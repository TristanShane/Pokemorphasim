namespace PokemonPolymorphism
{
    // We introduce an interface for moves to follow Dependency Inversion (high-level code depends on abstraction).
    internal interface IMove
    {
        string Name { get; }
        string Execute(Pokemon pokemon);
    }
}
