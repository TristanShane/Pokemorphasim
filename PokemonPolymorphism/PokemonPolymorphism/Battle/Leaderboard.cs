using System.Collections.Generic;
using System.Linq;

namespace PokemonPolymorphism
{
    // We track wins separately to keep battle logic focused on combat only.
    internal sealed class Leaderboard
    {
        private readonly Dictionary<string, int> winsByPokemon = new Dictionary<string, int>();

        public void RecordWin(Pokemon winner)
        {
            // We keep wins indexed by name to avoid coupling to object references.
            if (!winsByPokemon.ContainsKey(winner.Name))
            {
                winsByPokemon[winner.Name] = 0;
            }

            winsByPokemon[winner.Name]++;
        }

        public IReadOnlyList<LeaderboardEntry> GetStandings()
        {
            // We project into a read-only list so callers cannot mutate internal state.
            return winsByPokemon
                .Select(entry => new LeaderboardEntry(entry.Key, entry.Value))
                .OrderByDescending(entry => entry.Wins)
                .ToList();
        }
    }

    internal sealed class LeaderboardEntry
    {
        public string Name { get; }
        public int Wins { get; }

        public LeaderboardEntry(string name, int wins)
        {
            Name = name;
            Wins = wins;
        }
    }
}
