using HtmlAgilityPack;
using Recipe.Parser.Models;

namespace Recipe.Parser.Interfaces
{
    public interface IIngredientsService
    {
        public List<Models.Recipe> ParseIngredients(List<string> urls);
        public List<Models.Recipe> ParseIngredientsNLP(List<string> urls);
        public List<string> GetAllText(List<string> urls);
        public List<string> GetWPRMSites(List<string> urls);
    }
}
