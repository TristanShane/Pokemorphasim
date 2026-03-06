using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PokemonPolymorphism
{
    internal sealed class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext context;

        public PokemonRepository(PokemonContext context)
        {
            // We inject the context to respect dependency inversion.
            this.context = context;
        }

        public IReadOnlyList<Pokemon> GetAll()
        {
            // We use AsNoTracking to reduce change-tracking memory overhead for read-only data.
            return context.Pokemon
                .AsNoTracking()
                .Include(pokemon => pokemon.PokemonMoves)
                .ThenInclude(link => link.Move)
                .Select(pokemon => new DatabasePokemon(
                    pokemon.Name,
                    pokemon.Type,
                    pokemon.Weakness,
                    pokemon.Health,
                    pokemon.PokemonMoves.Select(link => link.Move.Name).ToList()))
                .ToList();
        }
    }
}
