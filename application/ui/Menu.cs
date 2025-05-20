using System;
using System.Collections.Generic;
using campusLove.application.services;
using campusLove.domain.dto;
using campusLove.domain.ports;
using Spectre.Console;

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
                            AnsiConsole.MarkupLine("[green]✅ Login successful![/]");
                            _indexP.Show(Result);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]❌ Login failed. Please try again.[/]");
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                        break;
                }
            }
        }

        public void DisplayMainMenu()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(new Rule("[bold hotpink]💘 Campus Love App 💘[/]").RuleStyle("deeppink1"));

            var menu = new SelectionPrompt<string>()
                .Title("[bold]Welcome to the Campus Love Application![/]")
                .PageSize(3)
                .AddChoices("1. Register", "2. Login", "3. Exit");

            var choice = AnsiConsole.Prompt(menu);
            switch (choice)
            {
                case "1. Register":
                    register();
                    break;
                case "2. Login":
                    Result = _login.login();
                    if (Result != "404")
                    {
                        AnsiConsole.MarkupLine("[green]✅ Login successful![/]");
                        _indexP.Show(Result);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]❌ Login failed. Please try again.[/]");
                    }
                    break;
                case "3. Exit":
                    Environment.Exit(0);
                    break;
            }
        }

        private void register()
        {
            var user = new DtoRegisterUser();
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold aqua]📝 Register User[/]").RuleStyle("aqua"));

            user.doc = PedirEntrada("Enter your [bold]document[/]: ");
            user.name = PedirEntrada("Enter your [bold]name[/]: ");
            user.lastName = PedirEntrada("Enter your [bold]last name[/]: ");
            user.age = PedirEntradaInt("Enter your [bold]age[/]: ");
            
            AnsiConsole.Clear();
            var genders = _service.GetGenders();
            user.genderId = SeleccionarOpcionDesdeLista(genders, "Select your [bold]gender[/]:");
            AnsiConsole.Clear();

            var cities = _service.GetCities();
            user.cityId = SeleccionarOpcionDesdeLista(cities, "Select your [bold]city[/]:");
            AnsiConsole.Clear();

            var careers = _service.GetCareers();
            user.careerId = SeleccionarOpcionDesdeLista(careers, "Select your [bold]career[/]:");
            AnsiConsole.Clear();

            user.username = PedirEntrada("Enter your [bold]username[/]: ");
            user.password = PedirEntrada("Enter your [bold]password[/]: ");
            user.Phrase = PedirEntrada("Enter your [bold]phrase[/]: ");
            user.email = PedirEntrada("Enter your [bold]email[/]: ");

            _service.registerUser(user);

            AnsiConsole.MarkupLine("[green]🎉 Registration successful![/]");
            AnsiConsole.MarkupLine("[grey]Press any key to return to the main menu...[/]");
            Console.ReadKey();
        }

        private int SeleccionarOpcionDesdeLista(List<(int Id, string Name)> opciones, string mensaje)
        {
            var selection = new SelectionPrompt<string>()
                .Title(mensaje)
                .PageSize(10);

            var idLookup = new Dictionary<string, int>();
            foreach (var (id, name) in opciones)
            {
                string display = $"{name}";
                selection.AddChoice(display);
                idLookup[display] = id;
            }

            string choice = AnsiConsole.Prompt(selection);
            return idLookup[choice];
        }

        private string PedirEntrada(string mensaje)
        {
            return AnsiConsole.Ask<string>(mensaje);
        }

        private int PedirEntradaInt(string mensaje)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>(mensaje)
                    .Validate(input =>
                        input > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]You must enter a positive number[/]")
                    ));
        }
    }
}
