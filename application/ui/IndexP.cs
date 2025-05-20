using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;
using campusLove.domain.ports;
using sgi_app.infrastructure.mysql;
using campusLove.application.ui;

namespace campusLove.application.ui
{
    public class IndexP
    {
        private readonly MessageUI _messagesUI;
        private readonly ProfileUI _profileui;

        private readonly MatchUI _MatchUI;

        private readonly DtoEditUI _Edit;

        private readonly StatsUI _Stats;

        public IndexP(MessageUI messageUI, ProfileUI profileui, MatchUI matchUI, DtoEditUI edit, StatsUI stats)
        {
            _messagesUI = messageUI;
            _profileui = profileui;
            _MatchUI = matchUI;
            _Edit = edit;
            _Stats = stats;
        }
       public void Show(string userName)
{
    bool running = true;

    while (running)
    {
        Console.Clear();
        Console.WriteLine("userName: " + userName);
        Console.WriteLine("===================================");
        Console.WriteLine("           Campus Love             ");
        Console.WriteLine("===================================");
        Console.WriteLine("1. View Profiles");
        Console.WriteLine("2. Matches");
        Console.WriteLine("3. Messages");
        Console.WriteLine("4. Your Profile");
        Console.WriteLine("5. Stats");
        Console.WriteLine("6. Logout");
        Console.WriteLine("7. Exit");
        Console.WriteLine("===================================");
        Console.Write("Selecciona una opción: ");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                ProfileView(userName);
                break;
            case "2":
                MatchUI(userName);
                Console.WriteLine("Presiona una tecla para volver...");
                Console.ReadKey();
                break;
            case "3":
                _messagesUI.StartChat(userName);
                Console.WriteLine("Presiona una tecla para volver...");
                Console.ReadKey();
                break;
            case "4":
                _Edit.Show(userName);
                break;
            case "5":
                _Stats.ShowAllStats();
                Console.WriteLine("Presiona una tecla para volver...");
                Console.ReadKey();
                break;
            case "6":
                Console.WriteLine("Cerrando sesión...");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                running = false; 
                break;
            case "7":
                Console.WriteLine("Saliendo...");
                Environment.Exit(0); 
                return;
            default:
                Console.WriteLine("Opción inválida. Intente de nuevo.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
}

        public void ProfileView(string userName)
        {
            _profileui.ViewProfiles(userName);
        }

        public void MatchUI(string userName)
        {
            _MatchUI.ShowUserMatches(userName);
        }
    }
}
