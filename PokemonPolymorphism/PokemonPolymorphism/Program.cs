using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PokemonPolymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // We build the container once to centralize dependency management.
            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            using (var scope = serviceProvider.CreateScope())
            {
                // We run migrations and seeding at startup to keep persistence concerns isolated.
                var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                initializer.Initialize();

                var demoRunner = scope.ServiceProvider.GetRequiredService<DemoRunner>();
                demoRunner.Run();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // We register logging to aid diagnostics without coupling to a concrete logger.
            services.AddLogging(builder => builder.AddConsole());

            // We register EF Core with SQLite to ensure a real database is used.
            services.AddDbContext<PokemonContext>(options => options.UseSqlite("Data Source=pokemon.db"));

            // We register common services so higher-level code depends on abstractions.
            services.AddSingleton(new Random());
            services.AddSingleton<Leaderboard>();
            services.AddScoped<DatabaseInitializer>();
            services.AddScoped<DemoRunner>();
            services.AddScoped<BattleService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IMoveRepository, MoveRepository>();
            services.AddScoped<PokemonRoster>();
            services.AddScoped<MoveCatalog>();
            services.AddScoped<IPokemonSelectionStrategy, RandomPokemonSelectionStrategy>();
            services.AddScoped<IMoveSelectionStrategy, RandomMoveSelectionStrategy>();
            services.AddScoped<IBattleStrategy, WeaknessAwareBattleStrategy>();
        }
    }
}
