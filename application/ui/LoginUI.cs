using System;
using campusLove.application.services;
using campusLove.domain.entities;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class LoginUI
    {
        private readonly LoginService _service;

        public LoginUI(LoginService loginService)
        {
            _service = loginService;
        }

        public string login()
        {
            Console.Clear();

            // Panel con borde y título centrado
            AnsiConsole.Write(
                new Panel("[bold yellow]Login User[/]")
                    .Border(BoxBorder.Rounded)
                    .Header("[green]Por favor ingresa tus credenciales[/]")
                    .Expand()
                    .Padding(1, 1)
                    );

            var credentials = new Credentials();

            // Solicitar usuario con prompt estilizado
            credentials.username = AnsiConsole.Ask<string>("[bold]Enter your User name:[/]");

            // Solicitar contraseña con prompt oculto
            credentials.password = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter your Password:[/]")
                    .PromptStyle("red")
                    .Secret());

            var result = _service.login(credentials.username, credentials.password);
            return result;
        }
    }
}
