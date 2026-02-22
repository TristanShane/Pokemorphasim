namespace PokemonPolymorphism
{
    // We abstract battle logic to make rule changes easy to swap and extend.
    internal interface IBattleStrategy
    {
        BattleActionResult ExecuteTurn(Pokemon attacker, Pokemon defender, MoveProfile move);
    }
}
