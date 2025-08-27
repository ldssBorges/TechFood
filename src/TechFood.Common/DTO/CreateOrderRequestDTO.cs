using System.ComponentModel.DataAnnotations;

namespace TechFood.Common.DTO;

public class CreateOrderRequestDTO
{
    [Required]
    public Guid CustomerId { get; set; }

    public string? CuponCode { get; set; }

    [Required]
    public List<Item> Items { get; set; } = [];

    public class Item
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
