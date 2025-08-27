using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO;

public class OrderDTO : EntityDTO
{
    public Guid? CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public List<OrderItemDTO> Items { get; set; }

    public List<OrderHistoryDTO> Historical { get; set; }

}
