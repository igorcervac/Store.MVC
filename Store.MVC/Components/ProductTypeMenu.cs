using Store.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Store.MVC.Components;

public class ProductTypeMenu: ViewComponent
{
    private readonly IReadOnlyRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductTypeMenu(IReadOnlyRepository<ProductType> productTypeRepository, IMapper mapper)
    {
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var productTypes = await _productTypeRepository.GetAllAsync();
        var productTypeVms = _mapper.Map<List<ProductTypeVm>>(productTypes);
        return View(productTypeVms);

    }
}
