using TechFood.Common.DTO.Payment;

namespace TechFood.Application.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<QrCodePaymentResultDTO> GenerateQrCodePaymentAsync(QrCodePaymentRequestDTO request);
    }
}
