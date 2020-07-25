using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeoClick.Data;
using DeoClick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DeoClick.Controllers
{
    public class AnunciosController : Controller
    {
        private readonly DeoClickContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AnunciosController(DeoClickContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Anuncios
        public async Task<IActionResult> Index()
        {
            var x = _context.Anuncio.Include(a => a.Clientes);
            return View(await x.ToListAsync());
        }

        // GET: Anuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncio.Include(a => a.Clientes)
                .FirstOrDefaultAsync(m => m.id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;

            return View(anuncio);
        }

        // GET: Anuncios/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente");
            return View();
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomeAnuncio,tipoAnuncio,valorAnuncio,FotoAnuncio,idCliente")] Anuncio anuncio, IFormFile FotoAnuncio)
        {
            if (ModelState.IsValid)
            {
                if (FotoAnuncio != null)
                {
                    //especificar qual é a pasta que deve salvar o arquivo
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\anuncioFotos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + FotoAnuncio.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await FotoAnuncio.CopyToAsync(stream);
                    };
                    anuncio.FotoAnuncio = "/images/anuncioFotos/" + nomeArquivo;

                }
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "nomeCliente", anuncio.idCliente);
            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncio.FindAsync(id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente", anuncio.idCliente);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomeAnuncio,tipoAnuncio,valorAnuncio,FotoAnuncio,idCliente")] Anuncio anuncio, IFormFile NovaFoto)
        {
            if (id != anuncio.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (NovaFoto != null)
                    {
                        //especificar qual é a pasta que deve salvar o arquivo
                        string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\anuncioFotos");
                        var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                        string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        {
                            await NovaFoto.CopyToAsync(stream);
                        };
                        anuncio.FotoAnuncio = "/images/anuncioFotos/" + nomeArquivo;

                    }

                    _context.Update(anuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioExists(anuncio.id))
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
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "nomeCliente", anuncio.idCliente);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncio
                .Include(a => a.Clientes)
                .FirstOrDefaultAsync(m => m.id == id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncio = await _context.Anuncio.FindAsync(id);
            _context.Anuncio.Remove(anuncio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnuncioExists(int id)
        {
            return _context.Anuncio.Any(e => e.id == id);
        }
    }
}
