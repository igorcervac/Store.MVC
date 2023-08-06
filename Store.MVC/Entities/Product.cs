using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.MVC.Entities;

public class Product : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(256)]
    public string Name { get; set; } = string.Empty;
    [StringLength(1024)]
    public string? Description { get; set; }
    public int ProductTypeId { get; set; }
    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; } = default!;
    [DataType(DataType.Currency)]
    [Precision(18, 2)]
    public decimal Price { get; set; }
}
