using AubreysCookBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AubreysCookBook.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository _recipeRepository;

        public HomeController(IRecipeRepository repo)
        {
            _recipeRepository = repo;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            //var recipe = _recipeRepository.GetRecipe()
            return View();
        }

        public IActionResult Privacy()
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