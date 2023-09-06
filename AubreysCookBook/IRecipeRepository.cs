using AubreysCookBook.Models;

namespace AubreysCookBook
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetAllRecipes();
        public Recipe GetRecipe(int id);
        public void UpdateRecipe(Recipe recipe);
        public void InsertRecipe(Recipe recipeToInsert);
        public IEnumerable<Category> GetCategories();
        public Recipe AssignCategory();
        public void DeleteRecipe(Recipe recipe);
    }
}
