namespace PokemonPolymorphism
{
    // We keep these notes in code so the project can teach mapping strategies alongside polymorphism.
    internal static class InheritanceMappingNotes
    {
        /*
            TPH (Table-Per-Hierarchy):
            - All derived types share a single table with a discriminator column.
            - Pros: simplest schema, fewer joins, best read performance for polymorphic queries.
            - Cons: many nullable columns and weaker constraints for subtype-specific fields.

            TPT (Table-Per-Type):
            - Base fields live in one table, derived fields live in separate tables joined by key.
            - Pros: normalized schema and strong constraints per subtype.
            - Cons: joins for every query, slower reads, more complex migrations.

            TPC (Table-Per-Concrete):
            - Each concrete type has its own full table (duplicate base columns).
            - Pros: no joins for concrete queries and strong constraints per type.
            - Cons: duplicated columns and harder polymorphic queries (UNIONs).

            General guidance:
            - TPH is usually the cleanest and fastest for most apps because it keeps queries simple.
            - TPT is clean from a normalization perspective but often slower; use when subtype constraints matter most.
            - TPC can be clean for isolated concrete types but is heavier to maintain; use when types are rarely queried together.
        */
    }
}
