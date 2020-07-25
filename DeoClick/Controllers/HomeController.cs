using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeoClick.Models;
using DeoClick.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace DeoClick.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeoClickContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, DeoClickContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var eventos = _context.Evento
                .Include(e => e.Clientes)
                .Include(e => e.Cidade).ToList();
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath; 
            return View(eventos);
        }


        public IActionResult Eventos()
        {
            var eventos = _context.Evento
                .Include(e => e.Clientes)
                .Include(e => e.Cidade).ToList();
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(eventos);
        }
        
        public IActionResult QuemSomos()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
