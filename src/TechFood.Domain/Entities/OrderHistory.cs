using TechFood.Domain.Enums;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    private OrderHistory() { }

    public OrderHistory(
        OrderStatusType status,
        DateTime? dateTime = null,
        Guid? id = null)
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }
        Status = status;
        CreatedAt = dateTime ?? DateTime.Now;
    }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType Status { get; private set; }
}
