using System.Data;

namespace PokemonPolymorphism
{
    // We use a simple in-memory DataSet to model a database without external dependencies.
    internal sealed class PokemonDatabase
    {
        public DataSet Data { get; }

        public PokemonDatabase()
        {
            Data = new DataSet("PokemonDatabase");
            Data.Tables.Add(CreatePokemonTable());
            Data.Tables.Add(CreateMoveTable());
        }

        public DataTable PokemonTable => Data.Tables["Pokemon"];
        public DataTable MoveTable => Data.Tables["Move"];

        private static DataTable CreatePokemonTable()
        {
            var table = new DataTable("Pokemon");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Weakness", typeof(string));
            table.Columns.Add("Health", typeof(int));
            table.Columns.Add("AllowedMoves", typeof(string));
            return table;
        }

        private static DataTable CreateMoveTable()
        {
            var table = new DataTable("Move");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Power", typeof(int));
            return table;
        }
    }
}
