namespace PokemonPolymorphism
{
    // We abstract Pokemon selection to keep randomization logic separate from orchestration.
    internal interface IPokemonSelectionStrategy
    {
        Pokemon Select(string excludedName = null);
    }
}
