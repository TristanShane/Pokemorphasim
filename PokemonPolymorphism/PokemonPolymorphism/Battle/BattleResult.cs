namespace PokemonPolymorphism
{
    // We represent the outcome of a battle so callers can display or persist results.
    internal sealed class BattleResult
    {
        public Pokemon Winner { get; }
        public Pokemon Loser { get; }
        public string Summary { get; }

        public BattleResult(Pokemon winner, Pokemon loser, string summary)
        {
            Winner = winner;
            Loser = loser;
            Summary = summary;
        }
    }
}
