using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers.Constants;
using WebApp.Helpers.Mappers;
using WebApp.Models.DTOs.Requests;
using WebApp.Models.ViewModels;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = AppConfig.DEFAULT_PAGE_SIZE)
    {
        var products = await _productService.GetProductsAsync(page, pageSize);
        return View(products.Select(EntityToDtoMapper.ToResponse));
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductAsync(id);
        if (product == null)
            return NotFound();

        return View(EntityToDtoMapper.ToResponse(product));
    }

    public IActionResult Create()
    {
        return View(new UpdateProductRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _productService.CreateProductAsync(request);
        TempData["SuccessMessage"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductAsync(id);
        if (product == null)
            return NotFound();

        var request = new UpdateProductRequest
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockLevel = product.StockLevel,
            Category = product.Category
        };

        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            await _productService.UpdateProductAsync(id, request);
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
