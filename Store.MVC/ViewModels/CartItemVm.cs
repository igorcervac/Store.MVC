namespace Store.MVC.ViewModels;

public class CartItemVm 
{
    public string CartId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public ProductVm Product { get; set; } = default!;
    public int Count { get; set; }
}
