using System;
using System.Linq;

namespace PokemonPolymorphism
{
    internal sealed class RandomPokemonSelectionStrategy : IPokemonSelectionStrategy
    {
        private readonly IPokemonRepository pokemonRepository;
        private readonly Random random;

        public RandomPokemonSelectionStrategy(IPokemonRepository pokemonRepository, Random random)
        {
            // We inject dependencies to keep selection deterministic in tests.
            this.pokemonRepository = pokemonRepository;
            this.random = random;
        }

        public Pokemon Select(string excludedName = null)
        {
            // We filter out the excluded Pokemon to avoid mirror matches.
            var options = pokemonRepository.GetAll()
                .Where(pokemon => !string.Equals(pokemon.Name, excludedName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return options[random.Next(options.Count)];
        }
    }
}
