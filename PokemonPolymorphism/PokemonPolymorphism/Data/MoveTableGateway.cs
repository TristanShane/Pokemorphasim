using System;
using System.Data;

namespace PokemonPolymorphism
{
    internal sealed class MoveTableGateway : ITableGateway
    {
        private readonly PokemonDatabase database;

        public MoveTableGateway(PokemonDatabase database)
        {
            this.database = database;
        }

        public string TableName => database.MoveTable.TableName;

        public void Load()
        {
            database.MoveTable.Rows.Clear();
            database.MoveTable.Rows.Add(1, "Thunderbolt", "Electric", 90);
            database.MoveTable.Rows.Add(2, "Ember", "Fire", 40);
            database.MoveTable.Rows.Add(3, "Vine Whip", "Grass", 45);
            database.MoveTable.Rows.Add(4, "Water Gun", "Water", 40);
            database.MoveTable.Rows.Add(5, "Quick Attack", "Normal", 40);
        }

        public void PrintRows()
        {
            Console.WriteLine($"Table: {TableName}");
            foreach (DataRow row in database.MoveTable.Rows)
            {
                Console.WriteLine($"{row["Id"]}: {row["Name"]} ({row["Type"]}) Power {row["Power"]}");
            }
        }
    }
}
