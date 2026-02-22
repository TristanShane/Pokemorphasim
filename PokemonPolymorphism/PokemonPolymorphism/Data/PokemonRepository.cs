using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;

namespace PokemonPolymorphism
{
    internal sealed class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDatabase database;

        public PokemonRepository(PokemonDatabase database)
        {
            // We inject the database to respect dependency inversion.
            this.database = database;
        }

        public IReadOnlyList<Pokemon> GetAll()
        {
            // We map rows into rich objects to centralize data transformation.
            var results = new List<Pokemon>();
            foreach (DataRow row in database.PokemonTable.Rows)
            {
                // We split allowed moves so the Pokemon instance can enforce its own move rules.
                var allowedMoves = ((string)row["AllowedMoves"])
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(move => move.Trim())
                    .ToList();

                results.Add(new DatabasePokemon(
                    (string)row["Name"],
                    (string)row["Type"],
                    (string)row["Weakness"],
                    (int)row["Health"],
                    allowedMoves));
            }

            return results;
        }
    }
}
