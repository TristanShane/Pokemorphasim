using System;
using System.Linq;

namespace PokemonPolymorphism
{
    internal sealed class RandomPokemonSelectionStrategy : IPokemonSelectionStrategy
    {
        private readonly PokemonRoster pokemonRoster;
        private readonly Random random;

        public RandomPokemonSelectionStrategy(PokemonRoster pokemonRoster, Random random)
        {
            // We inject dependencies to keep selection deterministic in tests.
            this.pokemonRoster = pokemonRoster;
            this.random = random;
        }

        public Pokemon Select(string excludedName = null)
        {
            // We filter out the excluded Pokemon to avoid mirror matches.
            var options = pokemonRoster.GetAll()
                .Where(pokemon => !string.Equals(pokemon.Name, excludedName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (options.Count == 0)
            {
                // We fail fast to avoid returning an invalid selection.
                throw new InvalidOperationException("No Pokemon available for selection.");
            }

            return options[random.Next(options.Count)];
        }
    }
}
