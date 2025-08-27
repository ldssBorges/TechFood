namespace TechFood.Common.DTO;

public class OrderItemDTO : EntityDTO
{
    public Guid ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

}
