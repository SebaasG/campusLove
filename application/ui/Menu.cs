using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;
using campusLove.domain.dto;
using campusLove.domain.ports;
using MySqlX.XDevAPI.Common;

namespace campusLove.application.ui
{
    public class Menu
    {
        private readonly RegisterUser _service;
        private readonly LoginUI _login;

        public Menu(RegisterUser service, LoginUI login)
        {
            _login = login;
            _service = service;
        }

     
        public void ShowMenu()
        {

            Console.Clear();
            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":

                        register();
                        break;
                    case "2":
                        var Result = _login.login();
                        if(Result == true){
                            Console.WriteLine("Login successful!");
                        }
                        else{
                            Console.WriteLine("Login failed. Please try again.");
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("          Campus Love App         ");
            Console.WriteLine("===================================");
            Console.WriteLine("Welcome to the Campus Love Application!");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Please select an option: ");
        }

        private void register(){

            var user  = new DtoRegisterUser();
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("          Register User           ");
            Console.WriteLine("===================================");

            user.doc = PedirEntrada("Enter your doc: "); 
            user.name = PedirEntrada("Enter your name: ");
            user.lastName = PedirEntrada("Enter your last name: ");
            user.age = PedirEntradaInt("Enter your age: ");
            user.genderId = PedirEntradaInt("Enter your gender ID: ");
            user.cityId = PedirEntradaInt("Enter your city ID: ");
            user.careerId = PedirEntradaInt("Enter your career ID: ");
            user.username = PedirEntrada("Enter your username: ");
            user.password = PedirEntrada("Enter your password: ");
          
         

            // Call the register method from the service
            _service.registerUser(user);

            Console.WriteLine("Registration successful!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

         private string PedirEntrada(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }

        private int PedirEntradaInt(string mensaje)
        {
            int result;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.WriteLine("Debe ingresar un número válido.");
                Console.Write(mensaje);
            }
            return result;
        }
    }
}