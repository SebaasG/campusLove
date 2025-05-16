using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;
using campusLove.domain.ports;
using sgi_app.infrastructure.mysql;

namespace campusLove.application.ui
{
    public class IndexP
    {   
        private readonly ProfileUI _profileui;

        public IndexP(ProfileUI profileui)
        {
            _profileui = profileui;
        }


        public void Show(string userName)
        {
            Console.Clear();
            Console.WriteLine("userName: " + userName);
            Console.WriteLine("===================================");
            Console.WriteLine("           Campus Love            ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. View Profiles");
            Console.WriteLine("2. Matches");
            Console.WriteLine("3. Messages");
            Console.WriteLine("4. Settings");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Exit");
            Console.WriteLine("===================================");

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
                    // Call method to view messages
                    break;
                case "4":
                    // Call method to view settings
                    break;
                case "5":
                    // Call method to logout
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        }

        public void ProfileView(string userName)
        {
            Console.WriteLine("HOla MUNDO");
            _profileui.ViewProfiles(userName);
        }
    }
}