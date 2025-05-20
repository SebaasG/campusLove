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
        private readonly IndexP _indexP;

        public Menu(RegisterUser service, LoginUI login, IndexP indexP)
        {
            _service = service;
            _login = login;
            _indexP = indexP;
        }

        public string Result = "";

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
                        Result = _login.login();
                        if (Result != "404")
                        {
                            Console.WriteLine("Login successful!");
                            _indexP.Show(Result);
                        }
                        else
                        {
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
            Console.WriteLine("          Campus Love App          ");
            Console.WriteLine("===================================");
            Console.WriteLine("Welcome to the Campus Love Application!");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Please select an option: ");
        }

        private void register()
        {
            var user = new DtoRegisterUser();
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("          Register User            ");
            Console.WriteLine("===================================");

            user.doc = PedirEntrada("Enter your doc: ");
            user.name = PedirEntrada("Enter your name: ");
            user.lastName = PedirEntrada("Enter your last name: ");
            Console.Clear();
            user.age = PedirEntradaInt("Enter your age: ");

            // Obtener lista de géneros desde el servicio
            var genders = _service.GetGenders();
            user.genderId = SeleccionarOpcionDesdeLista(genders, "Select your gender:");
            Console.Clear();

            var cities = _service.GetCities();
            user.cityId = SeleccionarOpcionDesdeLista(cities, "Select your city:");
            Console.Clear();
            var careers = _service.GetCareers();
            user.careerId = SeleccionarOpcionDesdeLista(careers, "Select your career:");
            Console.Clear();
            user.username = PedirEntrada("Enter your username: ");
            user.password = PedirEntrada("Enter your password: ");
            user.Phrase = PedirEntrada("Enter your phrase: ");
            user.email = PedirEntrada("Enter your email: ");

            // Registrar usuario
            _service.registerUser(user);

            Console.WriteLine("Registration successful!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        private int SeleccionarOpcionDesdeLista(List<(int Id, string Name)> opciones, string mensaje)
        {
            Console.WriteLine(mensaje);
            for (int i = 0; i < opciones.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {opciones[i].Name}");
            }

            int seleccion;
            while (true)
            {
                Console.Write("Enter option number: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out seleccion) &&
                    seleccion > 0 &&
                    seleccion <= opciones.Count)
                {
                    return opciones[seleccion - 1].Id; // Retorna el Id real para guardar en DB
                }
                Console.WriteLine("Invalid option, please try again.");
            }
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