namespace Recipe.Parser.Models
{
    public static class IngredientConfig
    {
        public const int maxIngredientLength = 60;
        public static List<string> keywords = new List<string>()
        {
            " tablespoon ",
            " tbsp ",
            " tsp ",
            " cup ",
            " quart ",
            " gallon ",
            " pint "
        };
    }
}
