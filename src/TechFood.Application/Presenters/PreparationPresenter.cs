using TechFood.Domain.Enums;

namespace TechFood.Application.Presenters
{
    public class PreparationPresenter
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Guid OrderId { get; set; }

        public PreparationPresenter(
            Guid id,
            PreparationStatusType status,
            DateTime createdAt,
            DateTime? startedAt,
            DateTime? finishedAt,
            Guid orderId)
        {
            Id = id;
            Status = status.ToString();
            CreatedAt = createdAt;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            OrderId = orderId;
        }
    }
}
