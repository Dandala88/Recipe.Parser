using HtmlAgilityPack;

namespace Recipe.Parser.Interfaces
{
    public interface IIngredientsService
    {
        public List<string> ParseIngredients(List<string> document);
    }
}
