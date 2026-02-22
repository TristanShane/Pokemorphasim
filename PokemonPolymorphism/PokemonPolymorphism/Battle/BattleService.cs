using System;
using System.Text;

namespace PokemonPolymorphism
{
    // This service focuses on battle orchestration and not on Pokemon specifics (Single Responsibility).
    internal sealed class BattleService
    {
        public void PerformMove(Pokemon pokemon, IMove move)
        {
            // We use polymorphism so behavior is extended by adding types, not by changing this method.
            Console.WriteLine($"{pokemon.GetBattleCry()} {move.Execute(pokemon)}");
        }

        public void PerformMoveWithSwitch(Pokemon pokemon, IMove move)
        {
            // We show the anti-pattern: switch on concrete types forces edits for every new Pokemon.
            string battleCry;

            if (pokemon is Pikachu)
            {
                battleCry = "Pika pika!";
            }
            else if (pokemon is Charmander)
            {
                battleCry = "Char!";
            }
            else if (pokemon is Bulbasaur)
            {
                battleCry = "Bulba!";
            }
            else if (pokemon is Squirtle)
            {
                battleCry = "Squirtle!";
            }
            else if (pokemon is Eevee)
            {
                battleCry = "Vee!";
            }
            else
            {
                // We fall back to a generic cry to avoid runtime failure.
                battleCry = "...";
            }

            Console.WriteLine($"{battleCry} {move.Execute(pokemon)}");
        }

        public BattleResult ExecuteTurnBasedBattle(
            Pokemon attacker,
            Pokemon defender,
            IBattleStrategy battleStrategy,
            IMoveSelectionStrategy moveSelectionStrategy)
        {
            // We build a log so the caller can display the full battle narrative.
            var log = new StringBuilder();

            while (attacker.Health > 0 && defender.Health > 0)
            {
                var attackerMove = moveSelectionStrategy.SelectMove(attacker);
                var attackerResult = battleStrategy.ExecuteTurn(attacker, defender, attackerMove);
                log.AppendLine(attackerResult.Summary);

                if (defender.Health <= 0)
                {
                    break;
                }

                var defenderMove = moveSelectionStrategy.SelectMove(defender);
                var defenderResult = battleStrategy.ExecuteTurn(defender, attacker, defenderMove);
                log.AppendLine(defenderResult.Summary);
            }

            var winner = attacker.Health > 0 ? attacker : defender;
            var loser = attacker.Health > 0 ? defender : attacker;
            log.AppendLine($"{winner.Name} wins the battle!");

            return new BattleResult(winner, loser, log.ToString().TrimEnd());
        }
    }
}
