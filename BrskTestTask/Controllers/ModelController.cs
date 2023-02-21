using BrskTestTask.Data;
using BrskTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrskTestTask.Controllers;

[Route("[controller]")]
public class ModelController : Controller
{
    private readonly ILogger<ModelController> _logger;
    private readonly AutoContext _context;

    public ModelController(ILogger<ModelController> logger,
        AutoContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var models = await _context.Models
            .Include(m => m.Brand)
            .OrderBy(m => m.Brand.Name)
            .ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _context.Models
            .Include(m => m.Brand)
            .Where(x => x.ModelId == id)
            .FirstOrDefaultAsync();
        if (model is not null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Create()
    {
        ViewData["Brands"] = new SelectList(await _context.Brands
            .ToListAsync(),
            nameof(Brand.BrandId),
            nameof(Brand.Name));
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Model model)
    {
        if (model is not null)
        {
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["Brands"] = new SelectList(await _context.Brands
            .ToListAsync(),
            nameof(Brand.BrandId),
            nameof(Brand.Name));
        var model = await _context.Models
            .Include(m => m.Brand)
            .Where(x => x.BrandId == id)
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
    public async Task<IActionResult> Edit(int id, Model model)
    {
        if (model is not null)
        {
            var record = await _context.Models
                .Include(m => m.Brand)
                .Where(x => x.BrandId == id)
                .FirstOrDefaultAsync();
            if (record is null)
            {
                return NotFound();
            }

            record.Name = model.Name;
            record.Active = model.Active;
            record.BrandId = model.BrandId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["Brands"] = new SelectList(await _context.Brands
            .ToListAsync(),
            nameof(Brand.Name));
        var model = await _context.Models
            .Include(m => m.Brand)
            .Where(x => x.ModelId == id)
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
    public async Task<IActionResult> Delete(int id, Model model)
    {
        if (model is not null)
        {
            var record = await _context.Models
                .Include(m => m.Brand)
                .Where(x => x.ModelId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (record is null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
