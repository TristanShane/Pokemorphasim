using System.Collections.Generic;
using System.Data;

namespace PokemonPolymorphism
{
    internal sealed class MoveRepository : IMoveRepository
    {
        private readonly PokemonDatabase database;

        public MoveRepository(PokemonDatabase database)
        {
            this.database = database;
        }

        public IReadOnlyList<MoveProfile> GetAll()
        {
            var results = new List<MoveProfile>();
            foreach (DataRow row in database.MoveTable.Rows)
            {
                results.Add(new MoveProfile(
                    (string)row["Name"],
                    (string)row["Type"],
                    (int)row["Power"]));
            }

            return results;
        }
    }
}
