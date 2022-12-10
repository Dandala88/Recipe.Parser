using Microsoft.AspNetCore.Mvc;
using Recipe.Parser.Interfaces;

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
        public IActionResult Ingredients([FromBody] List<string> documents)
        {
            try
            {
                var ingredients = _ingredientsService.ParseIngredients(documents);
                return Ok(ingredients);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}