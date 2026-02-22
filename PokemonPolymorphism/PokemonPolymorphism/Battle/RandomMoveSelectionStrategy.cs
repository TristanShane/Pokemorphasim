using System;
using System.Linq;

namespace PokemonPolymorphism
{
    internal sealed class RandomMoveSelectionStrategy : IMoveSelectionStrategy
    {
        private readonly IMoveRepository moveRepository;
        private readonly Random random;

        public RandomMoveSelectionStrategy(IMoveRepository moveRepository, Random random)
        {
            // We inject dependencies to keep this strategy deterministic in tests.
            this.moveRepository = moveRepository;
            this.random = random;
        }

        public MoveProfile SelectMove(Pokemon pokemon)
        {
            // We filter by Pokemon rules so each Pokemon can only use its allowed moves.
            var moves = moveRepository.GetAll()
                .Where(move => pokemon.CanUseMove(move))
                .ToList();

            // We keep a fallback to avoid runtime errors if data is misconfigured.
            if (moves.Count == 0)
            {
                moves = moveRepository.GetAll().ToList();
            }

            // We choose a random move to keep the demo focused on polymorphism, not complex rules.
            return moves[random.Next(moves.Count)];
        }
    }
}
