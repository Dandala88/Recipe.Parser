using HtmlAgilityPack;
using Recipe.Parser.Models;

namespace Recipe.Parser.Interfaces
{
    public interface IIngredientsService
    {
        public List<Models.Recipe> ParseIngredients(List<string> urls);
    }
}
