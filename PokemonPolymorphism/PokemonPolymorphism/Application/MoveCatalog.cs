using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We cache move profiles to avoid repeated allocations during selection.
    internal sealed class MoveCatalog
    {
        private readonly IMoveRepository repository;
        private IReadOnlyList<MoveProfile> cached;

        public MoveCatalog(IMoveRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<MoveProfile> GetAll()
        {
            if (cached == null)
            {
                cached = repository.GetAll();
            }

            return cached;
        }
    }
}
