using HtmlAgilityPack;
using Recipe.Parser.Interfaces;
using Recipe.Parser.Models;
using System.Text.RegularExpressions;
using System.Web;

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

        public List<Models.Recipe> ParseIngredientsNLP(List<string> urls)
        {
            var recipes = new List<Models.Recipe>();
            foreach (var url in urls)
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var docInnerHtmls = doc.DocumentNode.DescendantNodes().Select(s => s.InnerText);
                var docText = string.Join(string.Empty, docInnerHtmls);

                var recipe = new Models.Recipe();
                recipe.Ingredients = new List<string>();

                //Tokenizer
                //using var modelIn = new java.io.FileInputStream(GetModel("en-token.bin"));

                //var model = new opennlp.tools.tokenize.TokenizerModel(modelIn);
                //var tokenizer = new opennlp.tools.tokenize.TokenizerME(model);

                //var tokens = tokenizer.tokenize(doc.DocumentNode.InnerHtml);

                //foreach (var token in tokens)
                //    recipe.Ingredients.Add(token);


                //Sentences
                using var modelIn = new java.io.FileInputStream(GetModel("en-sent.bin"));

                var model = new opennlp.tools.sentdetect.SentenceModel(modelIn);
                var sentenceDetector = new opennlp.tools.sentdetect.SentenceDetectorME(model);

                var sentences = sentenceDetector.sentDetect(docText);

                foreach (var sentence in sentences)
                    recipe.Ingredients.Add(sentence);

                recipes.Add(recipe);
            }

            return recipes;
        }

        private const string DownloadsFolders = @"../nlp-models";
        private string GetModel(string fileName)
        {
            var asmFolder = Path.GetDirectoryName(GetType().Assembly.Location);
            var filePath = Path.GetFullPath(Path.Combine(asmFolder, DownloadsFolders, fileName));
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            return filePath;
        }

        public List<string> GetAllText(List<string> urls)
        {
            var docs = new List<string>();
            foreach (var url in urls)
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var docInnerHtmls = doc.DocumentNode.DescendantNodes().Select(s => s.InnerText);
                var docText = string.Join(string.Empty, docInnerHtmls);

                docs.Add(docText);
            }

            return docs;
        }

        public List<string> GetWPRMSites(List<string> urls)
        {
            var recipes = new List<string>();
            foreach (var url in urls)
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                foreach(var node in doc.DocumentNode.Descendants())
                {
                    var classList = node.Attributes["class"]?.Value;
                    if (classList != null && classList.Contains("wprm-recipe-name"))
                    {
                        var htmlArr = node.ParentNode.OuterHtml.Split("\n");
                        var html = string.Join(string.Empty, htmlArr);
                        recipes.Add(html);
                        break;
                    }
                }
            }

            return recipes;
        }
    }
}
