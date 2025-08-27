namespace TechFood.Domain.Enums;

public enum OrderStatusType
{
    Created,
    WaitingPayment,
    Paid,
    RefusedPayment,
    InPreparation,
    PreparationDone,
    Finished,
    Cancelled,
}
