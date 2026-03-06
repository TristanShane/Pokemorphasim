using System;
using System.Linq;

namespace PokemonPolymorphism
{
    internal sealed class RandomMoveSelectionStrategy : IMoveSelectionStrategy
    {
        private readonly MoveCatalog moveCatalog;
        private readonly Random random;

        public RandomMoveSelectionStrategy(MoveCatalog moveCatalog, Random random)
        {
            // We inject dependencies to keep this strategy deterministic in tests.
            this.moveCatalog = moveCatalog;
            this.random = random;
        }

        public MoveProfile SelectMove(Pokemon pokemon)
        {
            // We filter by Pokemon rules so each Pokemon can only use its allowed moves.
            var moves = moveCatalog.GetAll()
                .Where(move => pokemon.CanUseMove(move))
                .ToList();

            // We keep a fallback to avoid runtime errors if data is misconfigured.
            if (moves.Count == 0)
            {
                moves = moveCatalog.GetAll().ToList();
            }

            // We choose a random move to keep the demo focused on polymorphism, not complex rules.
            return moves[random.Next(moves.Count)];
        }
    }
}
