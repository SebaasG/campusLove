using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;
using campusLove.domain.entities;

namespace campusLove.application.ui
{
    public class LoginUI
    {
        private readonly LoginService _service;
           public LoginUI(LoginService loginService)
        {
            _service = loginService;
        }

        public bool login(){

            var credentials  = new Credentials();
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("            Login User           ");
            Console.WriteLine("===================================");

            credentials.username = PedirEntrada("Enter your User name: "); 
            credentials.password = PedirEntrada("Enter your Password: ");
    
            var result = _service.login(credentials.username, credentials.password);
            return result;
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();

        }

         private string PedirEntrada(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }
    }
}