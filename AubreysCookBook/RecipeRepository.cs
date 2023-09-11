using AubreysCookBook.Models;
using Dapper;
using System.Data;

namespace AubreysCookBook
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDbConnection _conn;

        public RecipeRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _conn.Query<Recipe>("SELECT * FROM RECIPES;");
        }


        public Recipe GetRecipe(int id)
        {
            return _conn.QuerySingle<Recipe>("SELECT * FROM RECIPES WHERE RECIPEID = @id", new { id = id });
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _conn.Execute("UPDATE recipes SET Name = @name, RecipeDescription = @recipedescription, Ingredients = @ingredients WHERE RecipeID = @id",
             new { name = recipe.Name, recipedescription = recipe.RecipeDescription, id = recipe.RecipeID, ingredients = recipe.Ingredients });
        }
        public void InsertRecipe(Recipe recipeToInsert)
        {
            _conn.Execute("INSERT INTO recipes (NAME, RECIPEDESCRIPTION, CATEGORYID, INGREDIENTS) VALUES (@name, @recipedescription, @categoryID, @ingredients);",
        new { name = recipeToInsert.Name, recipedescription = recipeToInsert.RecipeDescription, categoryID = recipeToInsert.CategoryID, ingredients = recipeToInsert.Ingredients });
        }
        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM categories;");
        }

        public Recipe AssignCategory()
        {
            var categoryList = GetCategories();
            var recipe = new Recipe();
            recipe.Categories = categoryList;
            return recipe;
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _conn.Execute("DELETE FROM Recipes WHERE RecipeID = @id;", new { id = recipe.RecipeID });
        }

        public IEnumerable<Recipe> SearchRecipe(string searchString)
        {
            return _conn.Query<Recipe>("SELECT * FROM RECIPES WHERE NAME LIKE @name;", 
                new {name = "%" + searchString + "%"});
        }

        //public Recipe GetRecipeByName(string name)
        ////{
        ////    return _conn.QuerySingle<Recipe>("SELECT * FROM RECIPES WHERE NAME = @name;", new { name = name });
        ////}

        ////public IEnumerable<Recipe> GetRecipesByCategory(int categoryID)
        ////{
        ////    return _conn.Query<Recipe>("SELECT * FROM RECIPES WHERE CATEGORYID = (SELECT CATEGORYID FROM CATEGORIES WHERE CATEGORYID = @categoryID);", new { categoryID = categoryID });
        ////}
    }
}
