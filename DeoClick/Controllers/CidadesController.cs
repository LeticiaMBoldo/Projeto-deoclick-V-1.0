using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeoClick.Data;
using DeoClick.Models;
using Microsoft.AspNetCore.Hosting;

namespace DeoClick.Controllers
{
    public class CidadesController : Controller
    {
        private readonly DeoClickContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public CidadesController(DeoClickContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Cidades
        public async Task<IActionResult> Index()
        {
            var x = _context.Cidade.Include(a => a.Estados);
            return View(await x.ToListAsync());
        }

        // GET: Cidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade.Include(c => c.Estados)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            ViewData["idEstado"] = new SelectList(_context.Estados, "id", "nomeEstado");
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomeCidade,idEstado")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEstado"] = new SelectList(_context.Estados, "id", "nomeEstado", cidade.idEstado);
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }
            ViewData["idEstado"] = new SelectList(_context.Estados, "id", "nomeEstado", cidade.idEstado);
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomeCidade,idEstado")] Cidade cidade)
        {
            if (id != cidade.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidade.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEstado"] = new SelectList(_context.Estados, "id", "nomeEstado", cidade.idEstado);
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade.Include(c => c.Estados)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(int id)
        {
            return _context.Cidade.Any(e => e.id == id);
        }
    }
}
