using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We use a data-driven Pokemon type to represent rows loaded from the database.
    internal sealed class DatabasePokemon : Pokemon
    {
        public DatabasePokemon(string name, string type, string weakness, int health, IReadOnlyCollection<string> allowedMoveNames)
            : base(name, type, weakness, health, allowedMoveNames)
        {
        }

        public override string GetBattleCry()
        {
            // We keep the battle cry data-driven to avoid coupling to hard-coded types.
            return $"{Name}!";
        }
    }
}
