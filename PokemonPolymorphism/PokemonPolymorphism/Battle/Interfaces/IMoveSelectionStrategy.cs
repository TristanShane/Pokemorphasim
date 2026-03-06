namespace PokemonPolymorphism
{
    // We abstract move selection to show polymorphism for battle decisions.
    internal interface IMoveSelectionStrategy
    {
        MoveProfile SelectMove(Pokemon pokemon);
    }
}
