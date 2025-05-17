using System;
using System.Collections.Generic;
using campusLove.application.ui;

namespace campusLove.application.ui
{
    public class IndexP
    {
        private readonly MessageUI _messagesUI;

        public IndexP(MessageUI messageUI)
        {
            _messagesUI = messageUI;
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
                Console.WriteLine("4. Settings");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Exit");
                Console.WriteLine("===================================");
                Console.Write("Selecciona una opción: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // TODO: View profiles
                        break;
                    case "2":
                        // TODO: View matches
                        break;
                    case "3":
                        // Aquí simplemente llamamos al StartChat, que ya muestra todo el flujo de chats
                        _messagesUI.StartChat(userName);
                        Console.WriteLine("Presiona una tecla para volver...");
                        Console.ReadKey();
                        break;
                    case "4":
                        // TODO: Settings
                        break;
                    case "5":
                        Console.WriteLine("Cerrando sesión...");
                        return; // Regresa al menú de login
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presiona una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
