using System.ComponentModel.DataAnnotations.Schema;

namespace TechFood.Common.DTO;

public class ProductDTO : EntityDTO
{

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Column("CategoryId")]
    public Guid CategoryDTOId { get; set; }

    public bool OutOfStock { get; set; }

    public string ImageFileName { get; set; } = null!;

    public decimal Price { get; set; }

}
