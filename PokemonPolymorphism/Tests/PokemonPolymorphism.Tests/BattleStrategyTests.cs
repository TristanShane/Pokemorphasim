using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokemonPolymorphism.Tests
{
    [TestClass]
    public class BattleStrategyTests
    {
        [TestMethod]
        public void WeaknessAwareBattleStrategy_AppliesDoubleDamageOnWeakness()
        {
            var attacker = new Pikachu();
            var defender = new Squirtle();
            var move = new MoveProfile("Thunderbolt", "Electric", 90);
            var strategy = new WeaknessAwareBattleStrategy();

            strategy.ExecuteTurn(attacker, defender, move);

            Assert.AreEqual(0, defender.Health);
        }

        [TestMethod]
        public void PokemonRepository_LoadsAllowedMovesFromDatabase()
        {
            var databasePath = Path.Combine(Path.GetTempPath(), $"pokemon-tests-{Guid.NewGuid()}.db");

            try
            {
                var options = new DbContextOptionsBuilder<PokemonContext>()
                    .UseSqlite($"Data Source={databasePath}")
                    .Options;

                using (var context = new PokemonContext(options))
                {
                    var initializer = new DatabaseInitializer(context);
                    initializer.Initialize();

                    var repository = new PokemonRepository(context);
                    var pokemon = repository.GetAll();

                    Assert.IsTrue(pokemon.Any(entry => entry.Name == "Pikachu" && entry.AllowedMoveNames.Contains("Thunderbolt")));
                }
            }
            finally
            {
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
            }
        }
    }
}
