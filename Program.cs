using campusLove.application.services;
using campusLove.application.servicesa;
using campusLove.application.ui;
using Microsoft.Extensions.Configuration;
using sgi_app.domain.factory;
using sgi_app.infrastructure.mysql;

namespace CslApp
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            var smtpSection = configuration.GetSection("SmtpSettings");

            var smtpSettings = new campusLove.domain.dto.SmtpSettings
            {
                Server = smtpSection["Server"],
                Port = int.Parse(smtpSection["Port"]),
                Username = smtpSection["Username"],
                Password = smtpSection["Password"],
                FromName = smtpSection["FromName"],
                
            };

            var emailServiceRepository = new campusLove.infrastructure.repositories.EmailServiceRepository();


         
            IDbFactory factory = new mysqlDbFactory(connectionString);

            var registerService = new RegisterUser(factory.ResgisterUserRepository());
            var loginService = new LoginService(factory.LoginUserRepository());
            
            var profileService = new ProfileService(factory.ProfileRepository(), factory.matchesRepository(),emailServiceRepository);
            var messageService = new MessageService(factory.MessagesRepository());
            var matchService = new MatchService(factory.matchesRepository());
            

            var profileUI = new ProfileUI(profileService);
            var matchUI = new MatchUI(matchService);

            var indexP = new IndexP(new MessageUI(messageService), profileUI, matchUI);

            var uiMenu = new Menu(registerService, new LoginUI(loginService), indexP);
            uiMenu.ShowMenu();
        }
    }
}
