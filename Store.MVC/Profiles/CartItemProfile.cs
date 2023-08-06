using AutoMapper;

namespace Store.MVC.Profiles
{
    public class CartItemProfile: Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemVm>();
            CreateMap<CartItem, OrderDetail>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
