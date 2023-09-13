using AubreysCookBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AubreysCookBook.Controllers
{
    public class RecipeController : Controller
    {

        private readonly IRecipeRepository repo;
        //private readonly IWebHostEnvironment _env;

        public RecipeController(IRecipeRepository repo, IWebHostEnvironment env)
        {
            this.repo = repo;
            //_env = env;
        }
        public IActionResult Index()
        {
            var recipes = repo.GetAllRecipes();
            return View(recipes);
        }

        public IActionResult ViewRecipe(int id)
        {
            var recipe = repo.GetRecipe(id);
            return View(recipe);
        }

        public IActionResult UpdateRecipe(int id)
        {
            Recipe recipe = repo.GetRecipe(id);
            if (recipe == null)
            {
                return View("RecipeNotFound");
            }
            return View(recipe);
        }

        public IActionResult UpdateRecipeToDatabase(Recipe recipe)
        {
            repo.UpdateRecipe(recipe);

            return RedirectToAction("ViewRecipe", new { id = recipe.RecipeID });
        }

        public IActionResult InsertRecipe()
        {
            var recipe = repo.AssignCategory();
            return View(recipe);
        }

        public IActionResult InsertRecipeToDatabase(Recipe recipeToInsert)
        {
            repo.InsertRecipe(recipeToInsert);
            return RedirectToAction("Index");
        }

        //public IActionResult InsertImageIntoDatabase(Recipe imageToInsert, IFormFile imageFile)
        //{
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        // Get the file name and combine it with the "Images" folder path
        //        var fileName = Path.Combine(_env.WebRootPath, "Images", imageFile.FileName);

        //        // Save the uploaded file to the specified path
        //        using (var fileStream = new FileStream(fileName, FileMode.Create))
        //        {
        //            imageFile.CopyTo(fileStream);
        //        }

        //        // Save the file path (e.g., "/Images/image.jpg") to your database
        //        imageToInsert.ImagePath = $"/Images/{imageFile.FileName}";
        //    }

        //    // Save other recipe details to the database using recipeViewModel
        //    repo.InsertRecipe(imageToInsert);

        //    return RedirectToAction("Index");
        //}
        public IActionResult DeleteRecipe(Recipe recipe)
        {
            repo.DeleteRecipe(recipe);
            return RedirectToAction("Index");
        }

        //public IActionResult ViewRecipeByName(string request)
        //{
        //    var recipe = repo.GetRecipeByName(request);

        //    if (recipe == null)
        //    {
        //        return View("NoRecipesFound");
        //    }

        //    return View("ViewRecipe", recipe);
        //}

        //public IActionResult ViewRecipesByCategoryName(int categoryID)
        //{
        //    var recipes = repo.GetRecipesByCategory(categoryID);

        //    if (recipes == null || !recipes.Any())
        //    {
        //        return View("NoRecipesFound");
        //    }

        //    return View("Index", recipes);

        //}
        public IActionResult Search(string searchString) 
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return View("NoRecipesFound");
            }

            var searchResults = repo.SearchRecipe(searchString);

            if(!searchResults.Any())
            {
                return View("NoRecipesFound");
            }
            return View(searchResults);
        }





    }
}
