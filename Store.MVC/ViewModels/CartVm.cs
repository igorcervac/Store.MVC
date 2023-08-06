namespace Store.MVC.ViewModels
{
    public class CartVm
    {
        public List<CartItemVm> Items { get; set; } = new List<CartItemVm>();
        public decimal TotalCost { get; set; }
        public decimal Count { get; set; }
    }
}
