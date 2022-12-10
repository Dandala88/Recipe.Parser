using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipe.Parser.Interfaces;
using Recipe.Parser.Models;

namespace Recipe.Parser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(ILogger<IngredientsController> logger, IIngredientsService ingredientsService)
        {
            _logger = logger;
            _ingredientsService = ingredientsService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Ingredients([FromBody] List<WebSnippet> snippets)
        {
            try
            {
                var urls = snippets.Select(s => s.Link).ToList();
                var ingredients = _ingredientsService.ParseIngredients(urls);
                return Ok(ingredients);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}