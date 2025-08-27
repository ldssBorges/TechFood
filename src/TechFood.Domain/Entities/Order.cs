using TechFood.Common.Exceptions;
using TechFood.Common.Resources;
using TechFood.Domain.Enums;
using TechFood.Domain.Validations;

namespace TechFood.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    private Order() { }

    public Order(
        Guid? customerId = null)
    {
        CustomerId = customerId;
        CreatedAt = DateTime.Now;
        Status = OrderStatusType.Created;
    }

    public Order(Guid? customerId, DateTime createdAt, DateTime? finishedAt,
    OrderStatusType status, decimal amount, decimal discount,
    IEnumerable<OrderItem> items, IEnumerable<OrderHistory?> history, Guid? id = null)
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }

        CustomerId = customerId;
        CreatedAt = createdAt;
        FinishedAt = finishedAt;
        Status = status;
        Amount = amount;
        Discount = discount;
        _items = items.ToList();
        _historical = history?.ToList() ?? new List<OrderHistory>();
    }

    private readonly List<OrderItem> _items = [];

    private readonly List<OrderHistory> _historical = [];

    public Guid? CustomerId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public OrderStatusType Status { get; private set; }

    public decimal Amount { get; private set; }

    public decimal Discount { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public IReadOnlyCollection<OrderHistory> Historical => _historical.AsReadOnly();

    public void ApplyDiscount(decimal discount)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Exceptions.Order_CannotApplyDiscountToNonCreatedStatus);
        }

        CommonValidations.ThrowIsGreaterThanZero(discount, Exceptions.Order_DiscountCannotBeNegative);

        Discount = discount;

        CalculateAmount();
    }

    public void CreatePayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Exceptions.Order_CannotCreatePaymentToNonCreatedStatus);
        }

        UpdateStatus(OrderStatusType.WaitingPayment);
    }

    public void ConfirmPayment()
    {
        if (Status != OrderStatusType.WaitingPayment)
        {
            throw new DomainException(Exceptions.Order_CannotPayToNonWaitingPaymentStatus);
        }

        UpdateStatus(OrderStatusType.Paid);
    }

    public void RefusedPayment()
    {
        if (Status != OrderStatusType.WaitingPayment)
        {
            throw new DomainException(Exceptions.Order_CannotRefuseToNonWaitingPaymentStatus);
        }

        UpdateStatus(OrderStatusType.RefusedPayment);
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Exceptions.Order_CannotAddItemToNonCreatedStatus);
        }

        _items.Add(item);

        CalculateAmount();
    }

    public void RemoveItem(Guid itemId)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Exceptions.Order_CannotRemoveItemToNonCreatedStatus);
        }

        var item = _items.Find(i => i.Id == itemId);

        CommonValidations.ThrowObjectIsNull(item, Exceptions.Order_ItemNotFound);

        _items.Remove(item!);

        CalculateAmount();
    }

    private void CalculateAmount()
    {
        Amount = 0;

        foreach (var item in _items)
        {
            Amount += item.Quantity * item.UnitPrice;
        }

        Amount -= Discount;
    }

    public void FinishPreparation()
    {
        if (Status != OrderStatusType.InPreparation)
        {
            throw new DomainException(Exceptions.Order_CannotFinishToNonInPreparationStatus);
        }

        UpdateStatus(OrderStatusType.PreparationDone);
    }

    public void StartPreparation()
    {
        if (Status != OrderStatusType.Paid)
        {
            throw new DomainException(Exceptions.Order_CannotPrepareToNonPaidStatus);
        }

        UpdateStatus(OrderStatusType.InPreparation);
    }

    public void CancelPreparation()
    {
        if (Status == OrderStatusType.Cancelled)
        {
            //TODO: Adjust message
            throw new DomainException("Pedido ja foi cancelado");
        }

        UpdateStatus(OrderStatusType.Cancelled);
    }

    public void Finish()
    {
        FinishedAt = DateTime.Now;

        UpdateStatus(OrderStatusType.Finished);
    }

    private void UpdateStatus(OrderStatusType status)
    {
        Status = status;
        _historical.Add(new(status, null, Guid.Empty));
    }
}
