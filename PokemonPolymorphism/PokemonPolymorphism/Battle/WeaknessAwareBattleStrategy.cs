using System;

namespace PokemonPolymorphism
{
    internal sealed class WeaknessAwareBattleStrategy : IBattleStrategy
    {
        public BattleActionResult ExecuteTurn(Pokemon attacker, Pokemon defender, MoveProfile move)
        {
            // We apply a simple weakness multiplier to demonstrate rule customization.
            var isWeak = string.Equals(defender.Weakness, move.Type, StringComparison.OrdinalIgnoreCase);
            var multiplier = isWeak ? 2 : 1;
            var damage = move.Power * multiplier;

            // We update the defender's health to show that battle logic mutates state.
            defender.ApplyDamage(damage);

            var effectiveness = isWeak ? "It's super effective!" : "It's not very effective.";
            var summary = $"{attacker.GetBattleCry()} {attacker.Name} uses {move.Name}. {effectiveness} {defender.Name} has {defender.Health} HP left.";
            return new BattleActionResult(damage, summary);
        }
    }
}
