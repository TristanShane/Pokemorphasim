using System;
using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We keep demo orchestration in its own class to avoid bloating Program.cs.
    internal sealed class DemoRunner
    {
        private readonly BattleService battleService;
        private readonly PokemonRoster pokemonRoster;
        private readonly MoveCatalog moveCatalog;
        private readonly IPokemonSelectionStrategy pokemonSelection;
        private readonly IMoveSelectionStrategy moveSelection;
        private readonly IBattleStrategy battleStrategy;
        private readonly Leaderboard leaderboard;

        public DemoRunner(
            BattleService battleService,
            PokemonRoster pokemonRoster,
            MoveCatalog moveCatalog,
            IPokemonSelectionStrategy pokemonSelection,
            IMoveSelectionStrategy moveSelection,
            IBattleStrategy battleStrategy,
            Leaderboard leaderboard)
        {
            // We inject dependencies to keep the runner focused on orchestration only.
            this.battleService = battleService;
            this.pokemonRoster = pokemonRoster;
            this.moveCatalog = moveCatalog;
            this.pokemonSelection = pokemonSelection;
            this.moveSelection = moveSelection;
            this.battleStrategy = battleStrategy;
            this.leaderboard = leaderboard;
        }

        public void Run()
        {
            RunPolymorphismDemo();
            PrintDatabaseCatalog();
            RunBattleSimulation();
        }

        private void RunPolymorphismDemo()
        {
            var team = new List<Pokemon>
            {
                new Pikachu(),
                new Charmander(),
                new Bulbasaur(),
                new Squirtle(),
                new Eevee()
            };

            IMove thunderbolt = new Thunderbolt();
            IMove ember = new Ember();

            Console.WriteLine("=== DO: Use polymorphism (Open/Closed Principle) ===");
            foreach (var pokemon in team)
            {
                // We call a virtual member so the correct behavior is chosen by the runtime.
                battleService.PerformMove(pokemon, thunderbolt);
            }

            Console.WriteLine();
            Console.WriteLine("=== DON'T: Use type checks / switches (violates Open/Closed) ===");
            foreach (var pokemon in team)
            {
                // We show the anti-pattern where logic must be edited whenever a new Pokemon is added.
                battleService.PerformMoveWithSwitch(pokemon, ember);
            }

            Console.WriteLine();
        }

        private void PrintDatabaseCatalog()
        {
            Console.WriteLine("=== EF Core: Database catalog ===");

            foreach (var pokemon in pokemonRoster.GetAll())
            {
                Console.WriteLine($"{pokemon.Name} ({pokemon.Type}) Weakness {pokemon.Weakness}, HP {pokemon.Health}");
            }

            Console.WriteLine();
            foreach (var move in moveCatalog.GetAll())
            {
                Console.WriteLine($"{move.Name} ({move.Type}) Power {move.Power}");
            }

            Console.WriteLine();
        }

        private void RunBattleSimulation()
        {
            Console.WriteLine("=== Turn-based battles (EF Core data) ===");

            var attacker = pokemonSelection.Select();

            for (var round = 1; round <= 3; round++)
            {
                Console.WriteLine($"--- Battle {round} ---");

                // We ensure the defender is different to keep each battle meaningful.
                var defender = pokemonSelection.Select(attacker.Name);

                // We run turn-based combat so both sides get to attack.
                var result = battleService.ExecuteTurnBasedBattle(attacker, defender, battleStrategy, moveSelection);
                Console.WriteLine(result.Summary);

                leaderboard.RecordWin(result.Winner);

                if (result.Winner.Name == attacker.Name)
                {
                    // We keep the attacker at the same health when it wins, per the requirement.
                    attacker = result.Winner;
                }
                else
                {
                    attacker = pokemonSelection.Select();
                }

                Console.WriteLine();
            }

            Console.WriteLine("=== Leaderboard ===");
            foreach (var entry in leaderboard.GetStandings())
            {
                Console.WriteLine($"{entry.Name}: {entry.Wins} wins");
            }

            Console.WriteLine();
        }
    }
}
