using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;
public class ProductController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IWebHostEnvironment _enviroment;

    public ProductController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment enviroment)
    {
        _categoryService = categoryService;
        _productService = productService;
        _enviroment = enviroment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null)
        {
            return NotFound();
        }

        var wwwroot = _enviroment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
        var exists = System.IO.File.Exists(image);

        ViewBag.ImageExist = exists;

        return View(productDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null)
        {
            return NotFound();
        }

        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);
        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var productDto = await _productService.GetByIdAsync(id);

        if (productDto is null)
        {
            return NotFound();
        }

        return View(productDto);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
