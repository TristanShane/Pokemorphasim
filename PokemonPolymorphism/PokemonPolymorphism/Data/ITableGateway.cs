namespace PokemonPolymorphism
{
    // We define a table gateway abstraction so each table can encapsulate its own logic (Open/Closed Principle).
    internal interface ITableGateway
    {
        string TableName { get; }
        void Load();
        void PrintRows();
    }
}
