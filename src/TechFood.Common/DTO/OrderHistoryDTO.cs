using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO;

public class OrderHistoryDTO : EntityDTO
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderStatusTypeDTO Status { get; set; }
}
