using TechFood.Domain.Entities;
using TechFood.Domain.Enums;

namespace TechFood.Application.Presenters
{
    public class OrderPresenter
    {
        public Guid? CustomerId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        public OrderStatusType Status { get; private set; }

        public decimal Amount { get; private set; }

        public decimal Discount { get; private set; }

        public static OrderPresenter Create(Order order)
        {
            return new OrderPresenter
            {
                CustomerId = order.CustomerId,
                CreatedAt = order.CreatedAt,
                FinishedAt = order.FinishedAt,
                Status = order.Status,
                Amount = order.Amount,
                Discount = order.Discount,
            };
        }
    }
}
