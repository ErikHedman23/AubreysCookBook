﻿using AubreysCookBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AubreysCookBook.Controllers
{
    public class RecipeController : Controller
    {

        private readonly IRecipeRepository repo;

        public RecipeController(IRecipeRepository repo)
        {
            this.repo = repo;
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
            var searchResults = repo.SearchRecipe(searchString);
            return View(searchResults);
        }





    }
}