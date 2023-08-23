using FoodHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers;

[ApiController]
[Route( "api/ingredients" )]
public sealed class IngredientController : ControllerBase
{
    [HttpGet]
    public ActionResult<Ingredient> GetIngredients( )
    {
        return Ok( new Ingredient { Id = new Guid( ), Name = "Pasta" } );
    }
}