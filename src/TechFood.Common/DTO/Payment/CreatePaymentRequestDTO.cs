using System.ComponentModel.DataAnnotations;
using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO.Payment
{
    public class CreatePaymentRequestDTO
    {
        [Required]
        public Guid? OrderId { get; set; }

        public PaymentTypeDTO Type { get; set; }
    }
}
