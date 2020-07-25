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
    public class EventosController : Controller
    {
        private readonly DeoClickContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EventosController(DeoClickContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var x = _context.Evento.Include(e => e.Clientes).Include(e => e.Cidade);
            return View(await x.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Cidade)
                .Include(e => e.Clientes)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente");
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,statusEvento,nomeEvento,endereco,bairro,numeroResidenia,cep,descricao,email,telefone,complemento,dataInicio,dataTermino,tipoEvento,valorEvento,FotoEvento,idCliente,idCidade")] Evento evento, IFormFile FotoEvento)
        {
            if (ModelState.IsValid)
            {

                if (FotoEvento != null)
                {
                    //especificar qual é a pasta que deve salvar o arquivo
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\eventoFotos");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + FotoEvento.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await FotoEvento.CopyToAsync(stream);
                    };
                    evento.FotoEvento = "/images/eventoFotos/" + nomeArquivo;

                }

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente", evento.idCliente);
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", evento.idCidade);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente", evento.idCliente);
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", evento.idCidade);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,statusEvento,nomeEvento,endereco,bairro,numeroResidenia,cep,descricao,email,telefone,complemento,dataInicio,dataTermino,tipoEvento,valorEvento,FotoEvento,idCliente,idCidade")] Evento evento, IFormFile NovaFoto)
        {
            if (id != evento.id)
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
                        string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\eventoFotos");
                        var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                        string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        {
                            await NovaFoto.CopyToAsync(stream);
                        };
                        evento.FotoEvento = "/images/eventoFotos/" + nomeArquivo;
                    }
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.id))
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
            ViewData["idCliente"] = new SelectList(_context.Clientes, "id", "nomeCliente", evento.idCliente);
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", evento.idCidade);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Clientes)
                .Include(e => e.Cidade)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.id == id);
        }
    }
}
