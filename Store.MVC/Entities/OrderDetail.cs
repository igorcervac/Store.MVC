using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.MVC.Entities;

public class OrderDetail : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrderId { get; set;}
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = default!;
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = default!;
    public int Count { get; set; }
    [DataType(DataType.Currency)]
    [Precision(18, 2)]
    public decimal Price { get; set; }
}
