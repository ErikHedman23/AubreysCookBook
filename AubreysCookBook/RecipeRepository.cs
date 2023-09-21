using AubreysCookBook.Models;
using Dapper;
using System.Data;

namespace AubreysCookBook
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IDbConnection _conn;

        //This is private field is declared as a readonly because we are wanting to create a connection using IDbConnection interface which creates a connection using Dapper to connect C# to MYSQL.  We aren't declaring it anything because we don't wish to alter how the interface functions, and we don't want to be able to alter it unless it is within a class constructor.
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
            _conn.Execute("UPDATE recipes SET Name = @name, RecipeDescription = @recipedescription, Ingredients = @ingredients, ImagePath = @imagePath WHERE RecipeID = @id",
             new { name = recipe.Name, recipedescription = recipe.RecipeDescription, id = recipe.RecipeID, ingredients = recipe.Ingredients, imagePath = recipe.ImagePath });
        }
        public void InsertRecipe(Recipe recipeToInsert)
        {
            _conn.Execute("INSERT INTO recipes (NAME, RECIPEDESCRIPTION, CATEGORYID, INGREDIENTS, IMAGEPATH) VALUES (@name, @recipedescription, @categoryID, @ingredients, @imagePath);",
        new { name = recipeToInsert.Name, recipedescription = recipeToInsert.RecipeDescription, categoryID = recipeToInsert.CategoryID, ingredients = recipeToInsert.Ingredients, imagePath = recipeToInsert.ImagePath });
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
