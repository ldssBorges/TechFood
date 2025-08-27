using System.Linq;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Mappers;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class OrderGateway : IOrderGateway
    {
        private readonly IUnitOfWorkDataSource _unitOfWork;
        private readonly IOrderDataSource _orderDataSource;

        public OrderGateway(IOrderDataSource orderDataSource,
                            IUnitOfWorkDataSource unitOfWork
                            )
        {
            _unitOfWork = unitOfWork;
            _orderDataSource = orderDataSource;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            var orderDTO = new OrderDTO()
            {
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                Discount = order.Discount,
                FinishedAt = order.FinishedAt,
                Historical = order.Historical.Select(h => new OrderHistoryDTO
                {
                    Id = h.Id,
                    OrderId = order.Id,
                    IsDeleted = h.IsDeleted,
                    CreatedAt = h.CreatedAt,
                    Status = (OrderStatusTypeDTO)h.Status
                }).ToList(),
                IsDeleted = order.IsDeleted,
                CreatedAt = order.CreatedAt,
                Status = (OrderStatusTypeDTO)order.Status,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            var result = await _orderDataSource.AddAsync(orderDTO);

            await _unitOfWork.CommitAsync();

            return result;
        }

        public async Task<List<Order>> GetAllDoneAndInPreparationAsync()
        {
            var orders = await _orderDataSource.GetAllDoneAndInPreparationAsync();

            var orderList = orders
    .Select(x => new Order(
        x.CustomerId,
        x.CreatedAt,
        null,
        (OrderStatusType)x.Status,
        x.Amount,
        x.Discount,
        (x.Items ?? new List<OrderItemDTO>()).Any()
            ? (x.Items ?? new List<OrderItemDTO>()).Select(orderItem =>
                new OrderItem(orderItem.ProductId, orderItem.UnitPrice, orderItem.Quantity, orderItem.Id))
            : new List<OrderItem>(),
        (x.Historical ?? new List<OrderHistoryDTO>()).Any()
            ? (x.Historical ?? new List<OrderHistoryDTO>()).Select(orderHistory =>
                new OrderHistory((OrderStatusType)orderHistory.Status, orderHistory.CreatedAt, orderHistory.Id))
            : new List<OrderHistory>(),
        x.Id
    ))
    .OrderByDescending(c => c.Status)
    .ThenBy(c => c.CreatedAt)
    .ToList();

            return orderList;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var orderDTO = await _orderDataSource.GetByIdAsync(id);

            if (orderDTO is null)
            {
                return null;
            }

            return OrderMapper.ToDomain(orderDTO);
        }

        public async Task UpdateAsync(Order order)
        {
            var orderDTO = new OrderDTO()
            {
                Id = order.Id,
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                Discount = order.Discount,
                FinishedAt = order.FinishedAt,
                Historical = order.Historical.Select(h => new OrderHistoryDTO
                {
                    Id = h.Id,
                    OrderId = order.Id,
                    IsDeleted = h.IsDeleted,
                    CreatedAt = h.CreatedAt,
                    Status = (OrderStatusTypeDTO)h.Status
                }).ToList(),
                IsDeleted = order.IsDeleted,
                CreatedAt = order.CreatedAt,
                Status = (OrderStatusTypeDTO)order.Status,
                Items = order.Items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _orderDataSource.UpdateAsync(orderDTO);

            await _unitOfWork.CommitAsync();
        }
    }
}
