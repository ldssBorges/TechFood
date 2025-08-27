namespace TechFood.Common.DTO.Payment
{
    public record PaymentItemDTO(string Title, int Quantity, string Unit, decimal UnitPrice, decimal Amount);
}
