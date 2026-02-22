using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonPolymorphism
{
    // We use an abstract base class so all Pokemon share a consistent contract.
    internal abstract class Pokemon
    {
        // We keep name immutable to preserve a consistent identity.
        public string Name { get; }
        // We keep type immutable so the model reflects stable identity data.
        public string Type { get; }
        // We keep weakness immutable to avoid changing battle rules mid-fight.
        public string Weakness { get; }
        // We keep allowed moves on the Pokemon to enforce type-specific move rules.
        public IReadOnlyCollection<string> AllowedMoveNames { get; }
        // We keep health mutable because battles are expected to reduce it.
        public int Health { get; private set; }

        protected Pokemon(string name, string type, string weakness, int health, IReadOnlyCollection<string> allowedMoveNames)
        {
            Name = name;
            Type = type;
            Weakness = weakness;
            Health = health;
            // We store allowed move names to let selection strategies enforce rules.
            AllowedMoveNames = allowedMoveNames;
        }

        // We make this abstract to force each Pokemon to provide its own behavior.
        public abstract string GetBattleCry();

        public void ApplyDamage(int damage)
        {
            // We clamp health at zero to avoid negative hit points.
            Health = Math.Max(Health - damage, 0);
        }

        public bool CanUseMove(MoveProfile move)
        {
            // We centralize the rule so all strategies can rely on a single check.
            return AllowedMoveNames.Contains(move.Name);
        }
    }
}
