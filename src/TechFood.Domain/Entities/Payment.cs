using TechFood.Common.Exceptions;
using TechFood.Domain.Enums;

namespace TechFood.Domain.Entities;

public class Payment : Entity, IAggregateRoot
{
    private Payment() { }

    public Payment(
        Guid orderId,
        PaymentType type,
        decimal amount,
        Guid? id = null,
        PaymentStatusType status = PaymentStatusType.Pending)
    {
        if (id is not null)
        {
            base.SetId(id.Value);
        }

        OrderId = orderId;
        Type = type;
        Amount = amount;
        CreatedAt = DateTime.Now;
        Status = status;
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public PaymentType Type { get; private set; }

    public PaymentStatusType Status { get; private set; }

    public decimal Amount { get; private set; }
    public string QrCodeData { get; private set; }

    public int Number { get; private set; }

    public void Confirm()
    {
        if (PaidAt.HasValue)
        {
            throw new DomainException(Common.Resources.Exceptions.Payment_AlreadyPaid);
        }

        PaidAt = DateTime.Now;
        Status = PaymentStatusType.Approved;
    }

    public void Refused()
    {
        if (PaidAt.HasValue)
        {
            throw new DomainException(Common.Resources.Exceptions.Payment_AlreadyPaid);
        }

        Status = PaymentStatusType.Refused;
    }

    public void SetQRCodeData(string qrCodeData)
    {
        QrCodeData = qrCodeData;
    }

    public void SetOrderNumber(int number) => Number = number;
}
