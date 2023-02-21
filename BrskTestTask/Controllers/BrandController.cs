using BrskTestTask.Data;
using BrskTestTask.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrskTestTask.Controllers;

[Route("[controller]")]
public class BrandController : Controller
{
    private readonly ILogger<BrandController> _logger;
    private readonly AutoContext _context;

    public BrandController(ILogger<BrandController> logger,
        AutoContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var models = await _context.Brands.ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _context.Brands
            .Where(b => b.BrandId == id)
            .FirstOrDefaultAsync();
        if (model is not null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand model)
    {
        if (model is not null)
        {
            _context.Brands.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _context.Brands
            .Where(b => b.BrandId == id)
            .FirstOrDefaultAsync();
        if (model is not null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Brand model)
    {
        if (model is not null)
        {
            var record = await _context.Brands
                .Where(b => b.BrandId == id)
                .FirstOrDefaultAsync();
            if (record is null)
            {
                return NotFound();
            }

            record.Name = model.Name;
            record.Active = model.Active;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _context.Brands
            .Where(b => b.BrandId == id)
            .FirstOrDefaultAsync();
        if (model is not null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Brand model)
    {
        if (model is not null)
        {
            var record = await _context.Brands
                .Where(b => b.BrandId == id)
                .FirstOrDefaultAsync();
            if (record is null)
            {
                return NotFound();
            }

            _context.Brands.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
