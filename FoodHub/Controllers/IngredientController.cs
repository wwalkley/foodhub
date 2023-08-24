using FoodHub.Models;
using FoodHub.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers;

[ApiController]
[Route( "api/ingredients" )]
public sealed class IngredientController : ControllerBase
{
    private readonly DataStore _dataStore;
    private readonly ILogger<IngredientController> _logger;

    public IngredientController( DataStore dataStore, ILogger<IngredientController> logger )
    {
        _dataStore = dataStore;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<Ingredient> GetIngredients( )
    {
        try
        {
            return Ok( _dataStore.Ingredients );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Returns a singular <see cref="Ingredient"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific Ingredient</param>
    [HttpGet( "{id}", Name = "GetIngredient" )]
    public ActionResult<Ingredient> GetIngredient( Guid id )
    {
        try
        {
            var ingredient = _dataStore.Ingredients.FirstOrDefault( c => c.Id == id );

            if ( ingredient != default )
            {
                return Ok( ingredient );
            }

            LogNotFound( id );
            return NotFound( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Creates an <see cref="Ingredient"/>
    /// </summary>
    [HttpPost( "" )]
    public ActionResult<Ingredient> Create( Ingredient request )
    {
        try
        {
            var ingredient = MapIngredient( request );

            _dataStore.Ingredients.Add( ingredient );

            _logger.LogInformation( "Created Ingredient with id \'{IngredientId}\'", ingredient.Id );
            return CreatedAtRoute( "GetIngredient", new { ingredient.Id }, ingredient );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Updates a <see cref="Ingredient"/>
    /// </summary>
    [HttpPut( "{id}" )]
    public ActionResult<Ingredient> Update( Guid id, Ingredient request )
    {
        try
        {
            var ingredient = _dataStore.Ingredients.FirstOrDefault( i => i.Id == id );

            if ( ingredient == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            ingredient.Name = request.Name;

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Patches an <see cref="Ingredient"/>
    /// </summary>
    [HttpPatch( "{id}" )]
    public ActionResult<Ingredient> PartialUpdate( Guid id, JsonPatchDocument<Ingredient> patchDocument )
    {
        try
        {
            var ingredient = _dataStore.Ingredients.FirstOrDefault( c => c.Id == id );

            if ( ingredient == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            patchDocument.ApplyTo( ingredient );

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Deletes a <see cref="Ingredient"/>
    /// </summary>
    [HttpDelete( "{id}" )]
    public ActionResult Delete( Guid id )
    {
        try
        {
            var ingredient = _dataStore.Ingredients.FirstOrDefault( c => c.Id == id );

            if ( ingredient == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            _dataStore.Ingredients.Remove( ingredient );

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    private static Ingredient MapIngredient( Ingredient request )
    {
        return new Ingredient
        {
            Id = new Guid( ),
            Name = request.Name
        };
    }

    private void LogNotFound( Guid id )
    {
        _logger.LogInformation( "Ingredient with Id \'{Id}\' was not found", id );
    }

    private void LogError( Exception exception )
    {
        _logger.LogCritical( "Exception occured when executing function: {Exception}", exception );
    }
}