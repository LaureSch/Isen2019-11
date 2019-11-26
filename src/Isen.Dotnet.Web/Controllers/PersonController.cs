using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly ApplicationDbContext _context;

        public PersonController(
            ILogger<PersonController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // https://localhost:5001/Person/Index
        [HttpGet] // facultatif car GET par défaut
        public IActionResult Index()
        {       
            _logger.LogInformation("Appel de /person/index");
            var persons = _context.PersonCollection.ToList();
            _logger.LogDebug($"Passage de {persons.Count} personnes à la vue");
            return View(persons);
        }
    }
}