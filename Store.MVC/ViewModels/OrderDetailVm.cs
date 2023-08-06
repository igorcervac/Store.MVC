namespace Store.MVC.ViewModels
{
    public class OrderDetailVm
    {
        public int OrderId { get; set;}
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
