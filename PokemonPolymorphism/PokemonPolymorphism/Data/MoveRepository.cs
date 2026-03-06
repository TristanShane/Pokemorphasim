using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PokemonPolymorphism
{
    internal sealed class MoveRepository : IMoveRepository
    {
        private readonly PokemonContext context;

        public MoveRepository(PokemonContext context)
        {
            this.context = context;
        }

        public IReadOnlyList<MoveProfile> GetAll()
        {
            // We use AsNoTracking to avoid tracking overhead for read-only queries.
            return context.Moves
                .AsNoTracking()
                .Select(move => new MoveProfile(move.Name, move.Type, move.Power))
                .ToList();
        }
    }
}
