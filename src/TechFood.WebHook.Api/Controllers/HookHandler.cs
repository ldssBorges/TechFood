using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Service;
using TechFood.Common.DTO.Hook;
using TechFood.Infra.Payments.MercadoPago;

namespace TechFood.WebHook.Api.Controllers
{
    [ApiController()]
    [Route("v1/hook")]
    public class HookHandler : ControllerBase
    {
        private readonly IHookController _hookUseCase;

        private readonly string secretKey = "87bc7869ec224e19f3fa0e113836d0b243d655fb35969a026514c2cc586f5efa";

        public HookHandler(IUnitOfWorkDataSource unitOfWork,
                             IOrderDataSource orderDataSource,
                             IPreparationDataSource preparationDataSource,
                             IPaymentDataSource paymentDataSource,
                             IProductDataSource productDataSource,
                             IImageDataSource imageDataSource,
                             IOptions<MercadoPagoOptions> options,
                             IHttpClientFactory httpClientFactory,
                             IHttpContextAccessor httpContextAccessor,
                             IOrderNumberService orderNumberService)
        {
            var mercadoPagoPaymentService = new MercadoPagoPaymentService(options, httpClientFactory, httpContextAccessor);
            _hookUseCase = new HookController(unitOfWork,
                                                       orderDataSource,
                                                       preparationDataSource,
                                                       paymentDataSource,
                                                       productDataSource,
                                                       imageDataSource,
                                                       mercadoPagoPaymentService,
                                                       orderNumberService);
        }

        [HttpPost]
        public async Task<IActionResult> InvokeAsync(object serialObject)
        {
            var xSignature = HttpContext.Request.Headers["x-signature"].ToString();
            var xRequestId = HttpContext.Request.Headers["x-request-id"].ToString();

            var parts = xSignature.Split(',');

            string ts = null;
            string v1 = null;

            foreach (var part in parts)
            {
                var kv = part.Split('=');
                if (kv.Length == 2)
                {
                    if (kv[0].Trim() == "ts")
                        ts = kv[1].Trim();
                    else if (kv[0].Trim() == "v1")
                        v1 = kv[1].Trim();
                }
            }

            var dataId = HttpContext.Request.Query["data.id"].ToString();

            var manifest = $"id:{dataId};request-id:{xRequestId};ts:{ts};";

            var hash = ComputeHmacSha256(secretKey, manifest);

            var isValid = hash == v1;

            if (isValid == false)
                return NotFound();

            var request = JsonConvert.DeserializeObject<HookRequestDTO>(serialObject.ToString());

            await _hookUseCase.InvokeAsync(request);

            return Ok();
        }

        private static string ComputeHmacSha256(string key, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);

            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }
}
