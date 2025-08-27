using TechFood.Common.DTO;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;

namespace TechFood.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order ToDomain(OrderDTO dto)
        {
            return new Order(
                dto.CustomerId,
                dto.CreatedAt,
                dto.FinishedAt,
                (OrderStatusType)dto.Status,
                dto.Amount,
                dto.Discount,
                dto.Items.Select(ToDomain),
                dto.Historical?.Select(ToDomain),
                dto.Id
            );
        }

        private static OrderItem ToDomain(OrderItemDTO dto)
        {
            return new OrderItem(dto.ProductId, dto.UnitPrice, dto.Quantity, dto.Id);
        }

        private static OrderHistory ToDomain(OrderHistoryDTO dto)
        {
            return new OrderHistory((OrderStatusType)(int)dto.Status, dto.CreatedAt, dto.Id);
        }
    }
}
