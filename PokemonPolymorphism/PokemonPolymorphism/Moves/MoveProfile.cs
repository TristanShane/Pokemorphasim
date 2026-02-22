namespace PokemonPolymorphism
{
    // We model moves as a data-driven record to map table rows to behavior.
    internal sealed class MoveProfile
    {
        public string Name { get; }
        public string Type { get; }
        public int Power { get; }

        public MoveProfile(string name, string type, int power)
        {
            Name = name;
            Type = type;
            Power = power;
        }
    }
}
