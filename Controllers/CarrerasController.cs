
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionFacultadesCarrerasIdentity.Models;

public class CarrerasController : Controller
{
    private readonly GestionFacultadesCarrerasIdentityContext _context;

    public CarrerasController(
        GestionFacultadesCarrerasIdentityContext context)
    {
        _context = context;
    }

    // GET: Carreras
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var carreras = _context.Carrera.Include(c => c.Facultad);
        return View(await carreras.ToListAsync());
    }

    // GET: Carreras/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var carrera = await _context.Carrera
            .Include(c => c.Facultad)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (carrera == null)
            return NotFound();

        return View(carrera);
    }

    // GET: Carreras/Create
    [Authorize(Roles = "Coordinador")]
    public IActionResult Create()
    {
        ViewData["FacultadId"] = new SelectList(
            _context.Facultad, "Id", "Nombre");

        return View();
    }

    // POST: Carreras/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Create(
        [Bind("Id,Nombre,Codigo,DuracionSemestres,FacultadId")]
        Carrera carrera)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["FacultadId"] = new SelectList(
            _context.Facultad, "Id", "Nombre",
            carrera.FacultadId);

        return View(carrera);
    }

    // GET: Carreras/Edit/5
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var carrera = await _context.Carrera.FindAsync(id);

        if (carrera == null)
            return NotFound();

        ViewData["FacultadId"] = new SelectList(
            _context.Facultad, "Id", "Nombre",
            carrera.FacultadId);

        return View(carrera);
    }

    // POST: Carreras/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("Id,Nombre,Codigo,DuracionSemestres,FacultadId")]
        Carrera carrera)
    {
        if (id != carrera.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(carrera);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(carrera.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["FacultadId"] = new SelectList(
            _context.Facultad, "Id", "Nombre",
            carrera.FacultadId);

        return View(carrera);
    }

    // GET: Carreras/Delete/5
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var carrera = await _context.Carrera
            .Include(c => c.Facultad)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (carrera == null)
            return NotFound();

        return View(carrera);
    }

    // POST: Carreras/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Coordinador")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var carrera = await _context.Carrera.FindAsync(id);

        if (carrera != null)
        {
            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CarreraExists(int? id)
    {
        return _context.Carrera.Any(e => e.Id == id);
    }
}
