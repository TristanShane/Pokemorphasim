using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PokemonPolymorphism
{
    // We isolate migration and seed logic to keep startup clean.
    internal sealed class DatabaseInitializer
    {
        private readonly PokemonContext context;

        public DatabaseInitializer(PokemonContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            // We apply migrations so schema stays in sync with the model.
            context.Database.Migrate();

            if (context.Pokemon.Any())
            {
                return;
            }

            var moves = new List<MoveEntity>
            {
                new MoveEntity { Name = "Thunderbolt", Type = "Electric", Power = 90 },
                new MoveEntity { Name = "Ember", Type = "Fire", Power = 40 },
                new MoveEntity { Name = "Vine Whip", Type = "Grass", Power = 45 },
                new MoveEntity { Name = "Water Gun", Type = "Water", Power = 40 },
                new MoveEntity { Name = "Quick Attack", Type = "Normal", Power = 40 }
            };

            var pokemon = new List<PokemonEntity>
            {
                new PokemonEntity { Name = "Pikachu", Type = "Electric", Weakness = "Ground", Health = 100 },
                new PokemonEntity { Name = "Charmander", Type = "Fire", Weakness = "Water", Health = 100 },
                new PokemonEntity { Name = "Bulbasaur", Type = "Grass", Weakness = "Fire", Health = 100 },
                new PokemonEntity { Name = "Squirtle", Type = "Water", Weakness = "Electric", Health = 100 },
                new PokemonEntity { Name = "Eevee", Type = "Normal", Weakness = "Fighting", Health = 100 }
            };

            context.Moves.AddRange(moves);
            context.Pokemon.AddRange(pokemon);
            context.SaveChanges();

            LinkMoves("Pikachu", "Thunderbolt", "Quick Attack");
            LinkMoves("Charmander", "Ember");
            LinkMoves("Bulbasaur", "Vine Whip");
            LinkMoves("Squirtle", "Water Gun");
            LinkMoves("Eevee", "Quick Attack");

            context.SaveChanges();
        }

        private void LinkMoves(string pokemonName, params string[] moveNames)
        {
            var pokemon = context.Pokemon.Single(entity => entity.Name == pokemonName);
            var moves = context.Moves.Where(move => moveNames.Contains(move.Name)).ToList();

            foreach (var move in moves)
            {
                context.PokemonMoves.Add(new PokemonMoveEntity
                {
                    PokemonId = pokemon.Id,
                    MoveId = move.Id
                });
            }
        }
    }
}
