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
            

        public IndexP(MessageUI messageUI, ProfileUI profileui)
        {
            _messagesUI = messageUI;
            _profileui = profileui;
        }
        public void Show(string userName)
        {
            while (true)
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
                Console.WriteLine("6. Exit");
                Console.WriteLine("===================================");
                Console.Write("Selecciona una opción: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ProfileView(userName);
                        break;
                    case "2":
                        // Call method to view matches
                        break;
                    case "3":
                        _messagesUI.StartChat(userName);
                        Console.WriteLine("Presiona una tecla para volver...");
                        Console.ReadKey();
                        break;
                    case "4":
                        
                        break;
                    case "5":
                        // Call method to view settings
                        break;
                    case "6":
                        Console.WriteLine("Saliendo...");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }
        }
        public void ProfileView(string userName)
        {
            _profileui.ViewProfiles(userName);
        }
    }
}
