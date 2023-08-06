using AutoMapper;

namespace Store.MVC.Profiles;
public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductVm>();
        CreateMap<ProductVm, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}
