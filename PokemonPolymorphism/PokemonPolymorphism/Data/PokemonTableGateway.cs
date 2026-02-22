using System;
using System.Data;

namespace PokemonPolymorphism
{
    internal sealed class PokemonTableGateway : ITableGateway
    {
        private readonly PokemonDatabase database;

        public PokemonTableGateway(PokemonDatabase database)
        {
            // We inject the database dependency to make this class easy to test.
            this.database = database;
        }

        public string TableName => database.PokemonTable.TableName;

        public void Load()
        {
            // We clear and reload data to keep the method idempotent.
            database.PokemonTable.Rows.Clear();
            database.PokemonTable.Rows.Add(1, "Pikachu", "Electric", "Ground", 100, "Thunderbolt,Quick Attack");
            database.PokemonTable.Rows.Add(2, "Charmander", "Fire", "Water", 100, "Ember");
            database.PokemonTable.Rows.Add(3, "Bulbasaur", "Grass", "Fire", 100, "Vine Whip");
            database.PokemonTable.Rows.Add(4, "Squirtle", "Water", "Electric", 100, "Water Gun");
            database.PokemonTable.Rows.Add(5, "Eevee", "Normal", "Fighting", 100, "Quick Attack");
        }

        public void PrintRows()
        {
            Console.WriteLine($"Table: {TableName}");
            foreach (DataRow row in database.PokemonTable.Rows)
            {
                Console.WriteLine($"{row["Id"]}: {row["Name"]} ({row["Type"]}), Weakness {row["Weakness"]}, HP {row["Health"]}, Moves {row["AllowedMoves"]}");
            }
        }
    }
}
