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
    public class FormaPagamentosController : Controller
    {
        private readonly DeoClickContext _context;

        public FormaPagamentosController(DeoClickContext context)
        {
            _context = context;
        }

        // GET: FormaPagamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormaPagamento.ToListAsync());
        }

        // GET: FormaPagamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento
                .FirstOrDefaultAsync(m => m.id == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormaPagamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,tipoPagamento")] FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formaPagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }
            return View(formaPagamento);
        }

        // POST: FormaPagamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,tipoPagamento")] FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formaPagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormaPagamentoExists(formaPagamento.id))
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
            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormaPagamento
                .FirstOrDefaultAsync(m => m.id == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // POST: FormaPagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formaPagamento = await _context.FormaPagamento.FindAsync(id);
            _context.FormaPagamento.Remove(formaPagamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormaPagamentoExists(int id)
        {
            return _context.FormaPagamento.Any(e => e.id == id);
        }
    }
}
