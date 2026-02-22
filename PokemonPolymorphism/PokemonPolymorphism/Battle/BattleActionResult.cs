namespace PokemonPolymorphism
{
    // We represent a single attack so the battle service can build a full log.
    internal sealed class BattleActionResult
    {
        public int Damage { get; }
        public string Summary { get; }

        public BattleActionResult(int damage, string summary)
        {
            Damage = damage;
            Summary = summary;
        }
    }
}
