using Backend.DTOs.Importer;
using Backend.Services.CategoryManagement;
using Backend.Services.Importer;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers.Importer;

[Route("api/[controller]")]
[ApiController]
public class ImporterController : ControllerBase
{
    private readonly IImporterService _service;
    private readonly ICategoryService _categoryService;

    public ImporterController(IImporterService service, ICategoryService categoryService)
    {
        _service = service;
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Importer(ImporterDTO xlsx)
    {
        await _service.ProcessFileAsync(xlsx);
        return Ok(new { message = "User created successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> TestCategory()
    {
        var result = await _categoryService.GetAllService();
        return Ok(result);
    }
}