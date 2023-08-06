using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.MVC.Entities;

public class Order: IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        
    [StringLength(50)]
    [Required]
    public string FirstName { get; set; } = string.Empty;
        
    [StringLength(50)]
    [Required]
    public string LastName { get; set; } = string.Empty;
        
    [Required]
    [StringLength(100)]
    public string AddressLine { get; set; } = string.Empty;
        
    [Required]
    [StringLength(10, MinimumLength = 4)]
    public string ZipCode { get; set; } = string.Empty;
        
    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;
        
    [Required]
    [StringLength(20)]
    public string Region { get; set; } = string.Empty;
        
    [Required]
    [StringLength(50)]
    public string Country { get; set; } = string.Empty;
        
    [Required]
    [StringLength(25)]
    public string PhoneNumber { get; set; } = string.Empty;
        
    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    [Precision(18, 2)]
    public decimal Total { get; set; }
    public DateTime Date { get; set; }
}
