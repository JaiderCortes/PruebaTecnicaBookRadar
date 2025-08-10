using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PruebaTecnicaBookRadar.Data;
using PruebaTecnicaBookRadar.Models;

namespace PruebaTecnicaBookRadar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<LibroViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string autor)
        {
            if (string.IsNullOrEmpty(autor) || string.IsNullOrWhiteSpace(autor))
            {
                TempData["Error"] = "Por favor, ingrese un nombre de autor para la búsqueda.";
                return RedirectToAction("Index");
            }

            string url = $"https://openlibrary.org/search.json?author={Uri.EscapeDataString(autor)}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var libros = json["docs"]
                .Select(l => new LibroViewModel
                {
                    Titulo = l["title"]?.ToString(),
                    AnioPublicacion = l["first_publish_year"]?.ToObject<int?>(),
                    Editorial = l["publisher"]?.FirstOrDefault()?.ToString(),
                })
                .ToList();

            foreach (var libro in libros)
            {
                _context.HistorialBusquedas.Add(new HistorialBusqueda
                {
                    Autor = autor.ToUpper(),
                    Titulo = libro.Titulo,
                    AnioPublicacion = libro.AnioPublicacion,
                    Editorial = libro.Editorial,
                    FechaConsulta = DateTime.Now
                });
            }
            await _context.SaveChangesAsync();

            return View(libros);
        }

        public IActionResult Historial()
        {
            var historial = _context.HistorialBusquedas
                .OrderByDescending(h => h.FechaConsulta)
                .ToList();
            return View(historial);
        }
    }
}
