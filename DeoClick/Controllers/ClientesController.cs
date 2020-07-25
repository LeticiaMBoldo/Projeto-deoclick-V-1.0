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
    public class ClientesController : Controller
    {
        private readonly DeoClickContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ClientesController(DeoClickContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var x = _context.Clientes.Include(cl => cl.Cidade);
            return View(await x.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.Include(cl => cl.Cidade)
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomeCliente,nomeEmpresa,cepEmpresa,endereco,bairro,complemento,numeroResidencial,telefoneContato,email,senha,caminhoImagemPerfil,idCidade")] Clientes clientes, IFormFile caminhoImagemPerfil)
        {
            if (ModelState.IsValid)
            {

                if(caminhoImagemPerfil != null)
                {
                    //especificar qual é a pasta que deve salvar o arquivo
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\avatarUsuario");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + caminhoImagemPerfil.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await caminhoImagemPerfil.CopyToAsync(stream);
                    };
                    clientes.caminhoImagemPerfil = "/images/avatarUsuario/" + nomeArquivo;

                }

                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", clientes.idCidade);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", clientes.idCidade);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomeCliente,nomeEmpresa,cepEmpresa,endereco,bairro,complemento,numeroResidencial,telefoneContato,email,senha,caminhoImagemPerfil,idCidade")] Clientes clientes, IFormFile NovaFoto)
        {
            if (id != clientes.id)
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
                        string pasta = Path.Combine(webHostEnvironment.WebRootPath, "images\\avatarUsuario");
                        var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                        string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        {
                            await NovaFoto.CopyToAsync(stream);
                        };
                        clientes.caminhoImagemPerfil = "/images/avatarUsuario/" + nomeArquivo;

                    }
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.id))
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
            ViewData["idCidade"] = new SelectList(_context.Cidade, "id", "nomeCidade", clientes.idCidade);
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .Include(c => c.Cidade)
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientes == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.id == id);
        }
    }
}
