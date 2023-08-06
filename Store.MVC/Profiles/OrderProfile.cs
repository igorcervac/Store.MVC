using AutoMapper;

namespace Store.MVC.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderVm, Order>();
        }
    }
}
