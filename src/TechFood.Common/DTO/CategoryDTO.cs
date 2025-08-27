namespace TechFood.Common.DTO
{
    public class CategoryDTO : EntityDTO
    {
        public string Name { get; set; } = null!;

        public string ImageFileName { get; set; } = null!;

        public int SortOrder { get; set; }
    }
}
