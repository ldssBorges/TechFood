using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO
{
    public class GetPreparationMonitorDTO
    {
        public Guid preparationId { get; set; }

        public Guid OrderId { get; set; }

        public int Number { get; set; }

        public PreparationStatusTypeDTO Status { get; set; }

        public IEnumerable<ProductResultDTO>? Products { get; set; }
    }

    public class ProductResultDTO
    {
        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
