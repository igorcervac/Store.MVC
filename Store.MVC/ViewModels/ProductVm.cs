using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.MVC.ViewModels;
public class ProductVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ProductTypeId { get; set; }
    [DisplayName("Type")]
    public ProductTypeVm ProductType { get; set; } = default!;
    [DisplayFormat(DataFormatString ="{0:c}")]
    public decimal Price { get; set; }
}
