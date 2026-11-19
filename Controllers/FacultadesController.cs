
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GestionFacultadesCarrerasIdentity.Models;

public class FacultadesController : Controller
{
    private readonly GestionFacultadesCarrerasIdentityContext _context;

    public FacultadesController(
        GestionFacultadesCarrerasIdentityContext context)
    {
        _context = context;
    }

    // GET: Facultades
    // Todos pueden consultar las facultades
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Facultad.ToListAsync());
    }

    // GET: Facultades/Details/5
    // Todos pueden consultar los detalles
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facultad = await _context.Facultad
            .FirstOrDefaultAsync(m => m.Id == id);

        if (facultad == null)
        {
            return NotFound();
        }

        return View(facultad);
    }

    // GET: Facultades/Create
    // Solo el Coordinador puede crear facultades
    [Authorize(Roles = "Coordinador")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Facultades/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Create(
        [Bind("Id,Nombre,Decano")] Facultad facultad)
    {
        if (ModelState.IsValid)
        {
            _context.Add(facultad);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(facultad);
    }

    // GET: Facultades/Edit/5
    // Solo el Coordinador puede editar facultades
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facultad = await _context.Facultad.FindAsync(id);

        if (facultad == null)
        {
            return NotFound();
        }

        return View(facultad);
    }

    // POST: Facultades/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("Id,Nombre,Decano")] Facultad facultad)
    {
        if (id != facultad.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(facultad);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultadExists(facultad.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(facultad);
    }

    // GET: Facultades/Delete/5
    // Solo el Coordinador puede eliminar facultades
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facultad = await _context.Facultad
            .FirstOrDefaultAsync(m => m.Id == id);

        if (facultad == null)
        {
            return NotFound();
        }

        return View(facultad);
    }

    // POST: Facultades/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var facultad = await _context.Facultad.FindAsync(id);

        if (facultad != null)
        {
            _context.Facultad.Remove(facultad);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Verificar si existe la facultad
    private bool FacultadExists(int? id)
    {
        return _context.Facultad.Any(e => e.Id == id);
    }
}
