using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeoClick.Data;
using DeoClick.Models;

namespace DeoClick.Controllers
{
    public class PagamentoAnunciosController : Controller
    {
        private readonly DeoClickContext _context;

        public PagamentoAnunciosController(DeoClickContext context)
        {
            _context = context;
        }

        // GET: PagamentoAnuncios
        public async Task<IActionResult> Index()
        {
            var x = _context.PagamentoAnuncio.Include(p => p.FormaPagamento);
            return View(await x.ToListAsync());
        }

        // GET: PagamentoAnuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamentoAnuncio = await _context.PagamentoAnuncio.Include(p => p.FormaPagamento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pagamentoAnuncio == null)
            {
                return NotFound();
            }

            return View(pagamentoAnuncio);
        }

        // GET: PagamentoAnuncios/Create
        public IActionResult Create()
        {
            ViewData["idFormaPagamento"] = new SelectList(_context.FormaPagamento, "id", "tipoPagamento");
            return View();
        }

        // POST: PagamentoAnuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,idFormaPagamento")] PagamentoAnuncio pagamentoAnuncio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamentoAnuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idFormaPagamento"] = new SelectList(_context.FormaPagamento, "id", "tipoPagamento", pagamentoAnuncio.idFormaPagamento);
            return View(pagamentoAnuncio);
        }

        // GET: PagamentoAnuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamentoAnuncio = await _context.PagamentoAnuncio.FindAsync(id);
            if (pagamentoAnuncio == null)
            {
                return NotFound();
            }
            ViewData["idFormaPagamento"] = new SelectList(_context.FormaPagamento, "id", "tipoPagamento", pagamentoAnuncio.idFormaPagamento);
            return View(pagamentoAnuncio);
        }

        // POST: PagamentoAnuncios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,idFormaPagamento")] PagamentoAnuncio pagamentoAnuncio)
        {
            if (id != pagamentoAnuncio.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamentoAnuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoAnuncioExists(pagamentoAnuncio.id))
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
            ViewData["idFormaPagamento"] = new SelectList(_context.FormaPagamento, "id", "tipoPagamento", pagamentoAnuncio.idFormaPagamento);
            return View(pagamentoAnuncio);
        }

        // GET: PagamentoAnuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamentoAnuncio = await _context.PagamentoAnuncio
                .Include(p => p.FormaPagamento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (pagamentoAnuncio == null)
            {
                return NotFound();
            }

            return View(pagamentoAnuncio);
        }

        // POST: PagamentoAnuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pagamentoAnuncio = await _context.PagamentoAnuncio.FindAsync(id);
            _context.PagamentoAnuncio.Remove(pagamentoAnuncio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoAnuncioExists(int id)
        {
            return _context.PagamentoAnuncio.Any(e => e.id == id);
        }
    }
}
