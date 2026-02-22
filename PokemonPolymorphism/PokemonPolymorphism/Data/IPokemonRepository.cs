using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We use repositories to translate tables into domain objects without leaking DataRow to callers.
    internal interface IPokemonRepository
    {
        IReadOnlyList<Pokemon> GetAll();
    }
}
