using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required, StringLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    public decimal Price { get; set; }
}
