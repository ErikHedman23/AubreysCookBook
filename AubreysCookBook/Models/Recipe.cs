namespace AubreysCookBook.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string RecipeDescription { get; set; }
        public int CategoryID { get; set; }
        public string Ingredients { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
