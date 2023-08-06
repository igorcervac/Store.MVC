using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.MVC.Entities;

public class ProductType: IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(256)]
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set;} = new List<Product>();
}
