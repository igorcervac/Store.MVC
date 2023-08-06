using AutoMapper;

namespace Store.MVC.Profiles;
public class ProductTypeProfile: Profile
{
    public ProductTypeProfile()
    {
        CreateMap<ProductType, ProductTypeVm>();
    }
}
