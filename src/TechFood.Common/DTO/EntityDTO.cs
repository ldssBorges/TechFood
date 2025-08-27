namespace TechFood.Common.DTO
{
    public class EntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsDeleted { get; set; }
    }
}
