using HtmlAgilityPack;
using Recipe.Parser.Interfaces;
using Recipe.Parser.Models;
using System.Text.RegularExpressions;

namespace Recipe.Parser.Services
{
    public class IngredientsService : IIngredientsService
    {
        public List<string> ParseIngredients(List<string> documents)
        {
            var docElements = new List<string>();
            foreach (var document in documents)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(document);

                var nodes = doc.DocumentNode.Descendants();


                foreach (var node in nodes)
                {
                    foreach (var keyword in IngredientConfig.keywords)
                    {
                        var html = node.InnerText;
                        var match = Regex.Match(html, keyword);
                        if (match.Success)
                        {
                            var len = IngredientConfig.maxIngredientLength;
                            var startIndex = match.Index - len >= 0 ? match.Index - len : 0;
                            var matchLength = startIndex + len <= html.Length ? len : html.Length;
                            var section = html.Substring(startIndex, matchLength);
                            docElements.Add(section);
                        }
                    }
                }
            }

            return docElements;
        }
    }
}
