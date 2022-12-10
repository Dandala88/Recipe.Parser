using HtmlAgilityPack;
using Recipe.Parser.Interfaces;
using Recipe.Parser.Models;
using System.Text.RegularExpressions;

namespace Recipe.Parser.Services
{
    public class IngredientsService : IIngredientsService
    {
        public List<Models.Recipe> ParseIngredients(List<string> urls)
        {
            var recipes = new List<Models.Recipe>();
            foreach (var url in urls)
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var nodes = doc.DocumentNode.Descendants();

                var recipe = new Models.Recipe();
                recipe.Ingredients = new List<string>();

                foreach (var node in nodes)
                {
                    foreach (var keyword in IngredientConfig.keywords)
                    {
                        var html = node.InnerText;
                        var match = Regex.Match(html.ToLower(), keyword);
                        if (match.Success)
                        {
                            var len = IngredientConfig.maxIngredientLength;
                            var startIndex = match.Index - len >= 0 ? match.Index - len : 0;
                            var matchLength = startIndex + len <= html.Length ? len : html.Length;
                            var section = html.Substring(startIndex, matchLength);
                            if(!recipe.Ingredients.Contains(section))
                                recipe.Ingredients.Add(section);
                        }
                    }
                }

                recipes.Add(recipe);
            }

            return recipes;
        }
    }
}
