namespace PokemonPolymorphism
{
    internal sealed class Eevee : Pokemon
    {
        public Eevee()
            : base("Eevee", "Normal", "Fighting", 100, new[] { "Quick Attack" })
        {
        }

        public override string GetBattleCry()
        {
            return "Vee!";
        }
    }
}
