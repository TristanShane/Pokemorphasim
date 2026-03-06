using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We cache read-only data to avoid repeated allocations and database calls.
    internal sealed class PokemonRoster
    {
        private readonly IPokemonRepository repository;
        private IReadOnlyList<Pokemon> cached;

        public PokemonRoster(IPokemonRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<Pokemon> GetAll()
        {
            if (cached == null)
            {
                cached = repository.GetAll();
            }

            return cached;
        }
    }
}
