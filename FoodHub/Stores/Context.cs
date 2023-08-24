using System.Collections.ObjectModel;
using FoodHub.Models;

namespace FoodHub.Stores;

public sealed class DataStore
{
    public DataStore( )
    {
        Ingredients = GetIngredientsData( );
    }

    public ICollection<Ingredient> Ingredients { get; }

    /// <summary>
    /// Initial data store to test without database for Ingredients.
    /// </summary>
    private static ICollection<Ingredient> GetIngredientsData( )
    {
        return new Collection<Ingredient>
        {
            new( ) { Id = Guid.NewGuid( ), Name = "Fettuccine" },
            new( ) { Id = Guid.NewGuid( ), Name = "Courgette" },
            new( ) { Id = Guid.NewGuid( ), Name = "Tomato" },
        };
    }
}