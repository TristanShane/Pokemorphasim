using System.Collections.Generic;

namespace PokemonPolymorphism
{
    internal interface IMoveRepository
    {
        IReadOnlyList<MoveProfile> GetAll();
    }
}
