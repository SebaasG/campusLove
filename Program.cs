using campusLove.application.services;
using campusLove.application.ui;
using Microsoft.Extensions.Configuration;
using sgi_app.domain.factory;
using sgi_app.infrastructure.mysql;

namespace CslApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var dbSettings = configuration.GetSection("DatabaseSettings");
            
            var server = dbSettings["Server"];
            var port = dbSettings["Port"];
            var database = dbSettings["Database"];
            var user = dbSettings["User"];
            var password = dbSettings["Password"];

            var connectionString = $"Server={server};Port={port};Database={database};User={user};Password={password};";

            Console.WriteLine(connectionString);

            IDbFactory factory = new mysqlDbFactory(connectionString);

            var resgiterService =  new RegisterUser(factory.ResgisterUserRepository());

            var uiMenu = new Menu(resgiterService);
            uiMenu.ShowMenu();

        }
    }
}