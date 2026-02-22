using System;
using System.Collections.Generic;

namespace PokemonPolymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // We create services and abstractions in `Main` to keep composition centralized and testable.
            var battleService = new BattleService();

            // We depend on the abstraction `Pokemon` so any new Pokemon can participate without changing the caller.
            var team = new List<Pokemon>
            {
                new Pikachu(),
                new Charmander(),
                new Bulbasaur(),
                new Squirtle(),
                new Eevee()
            };

            // We model behavior as a separate abstraction to follow the Single Responsibility Principle.
            IMove thunderbolt = new Thunderbolt();
            IMove ember = new Ember();
            IMove waterGun = new WaterGun();

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
            Console.WriteLine("=== DO: Use polymorphism with database tables ===");

            // We keep database creation in a dedicated type to isolate data concerns.
            var database = new PokemonDatabase();

            // We operate on table gateways through an interface to support new tables without changing callers.
            var tableGateways = new List<ITableGateway>
            {
                new PokemonTableGateway(database),
                new MoveTableGateway(database)
            };

            foreach (var tableGateway in tableGateways)
            {
                // We rely on polymorphism so each table can define its own load and display logic.
                tableGateway.Load();
                tableGateway.PrintRows();
            }

            Console.WriteLine();
            Console.WriteLine("=== DO: Use table data with a battle strategy ===");

            // We use repositories to transform raw rows into domain models.
            var pokemonRepository = new PokemonRepository(database);
            var moveRepository = new MoveRepository(database);

            // We keep randomness in one place to make behavior deterministic if seeded later.
            var random = new Random();

            // We select Pokemon and moves through strategies to show another polymorphism seam.
            IPokemonSelectionStrategy pokemonSelection = new RandomPokemonSelectionStrategy(pokemonRepository, random);
            IMoveSelectionStrategy moveSelection = new RandomMoveSelectionStrategy(moveRepository, random);
            IBattleStrategy battleStrategy = new WeaknessAwareBattleStrategy();

            // We use a leaderboard to track the winners across multiple battles.
            var leaderboard = new Leaderboard();

            // We start with a random attacker pulled from the table data.
            var attacker = pokemonSelection.Select();

            for (var round = 1; round <= 3; round++)
            {
                Console.WriteLine($"--- Battle {round} ---");

                // We ensure the defender is different to keep each battle meaningful.
                var defender = pokemonSelection.Select(attacker.Name);

                // We run turn-based combat so both sides get to attack.
                var result = battleService.ExecuteTurnBasedBattle(attacker, defender, battleStrategy, moveSelection);
                Console.WriteLine(result.Summary);

                // We track the winner so we can display a summary at the end.
                leaderboard.RecordWin(result.Winner);

                if (result.Winner.Name == attacker.Name)
                {
                    // We keep the attacker at the same health when it wins, per the requirement.
                    attacker = result.Winner;
                }
                else
                {
                    // We choose a new attacker when the current one loses.
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
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
