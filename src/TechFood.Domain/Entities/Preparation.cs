using TechFood.Domain.Enums;

namespace TechFood.Domain.Entities;

public class Preparation : Entity, IAggregateRoot
{
    public Preparation(Guid orderId, int number)
    {
        OrderId = orderId;
        Number = number;
        CreatedAt = DateTime.Now;
        Status = PreparationStatusType.Pending;
    }

    public Preparation(Guid id,
        Guid orderId,
        int number,
        DateTime createdAt,
        DateTime? startedAt,
        DateTime? finishedAt,
        PreparationStatusType status) : this(orderId, number)
    {
        SetId(id);
        CreatedAt = createdAt;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
        Status = status;
    }

    public Guid OrderId { get; private set; }

    public int Number { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? StartedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public PreparationStatusType Status { get; private set; }

    public void Start()
    {
        if (Status != PreparationStatusType.Pending)
        {
            throw new InvalidOperationException("Preparation can only be started if it is pending.");
        }

        StartedAt = DateTime.Now;
        Status = PreparationStatusType.InProgress;
    }

    public void Finish()
    {
        if (Status != PreparationStatusType.InProgress)
        {
            throw new InvalidOperationException("Preparation can only be finished if it is in progress.");
        }

        Status = PreparationStatusType.Done;
        FinishedAt = DateTime.Now;
    }

    public void Delivered()
    {
        if (Status != PreparationStatusType.Done)
        {
            throw new InvalidOperationException("Preparation can only be finished if it is in done.");
        }

        Status = PreparationStatusType.Finish;
    }

    public void Cancel()
    {
        if (Status == PreparationStatusType.Cancelled)
        {
            throw new InvalidOperationException("Preparation can only be already cancelled.");
        }

        Status = PreparationStatusType.Cancelled;
        FinishedAt = DateTime.Now;
    }
}
