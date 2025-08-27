using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO;

public class PreparationDTO : EntityDTO
{

    public Guid OrderId { get; set; }

    public int Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public PreparationStatusTypeDTO Status { get; set; }

}
