using System;
using System.Collections.Generic;
using campusLove.application.services;
using campusLove.application.servicesa;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class MatchUI
    {
        private readonly MatchService _matchService;

        public MatchUI(MatchService matchService)
        {
            _matchService = matchService;
        }

        public void ShowUserMatches(string userDoc)
        {
            Console.Clear();

            string currentUserDoc = _matchService.FindDoc(userDoc);

            if (string.IsNullOrEmpty(currentUserDoc))
            {
                AnsiConsole.MarkupLine("[red]Error: No se encontró el documento del usuario.[/]");
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey(true);
                return;
            }

            var profiles = _matchService.GetUserMatches(currentUserDoc);

            AnsiConsole.Write(
                new Panel("[bold orange1]🧡 Tus Matches[/]")
                    .Border(BoxBorder.Rounded)
                    .Expand()
                    .Padding(1, 1));

            if (profiles == null || profiles.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Aún no tienes matches. ¡Sigue interactuando![/]");
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey(true);
                return;
            }

            int index = 0;
            while (index < profiles.Count)
            {
                var profile = profiles[index];

                var profilePanel = new Panel(
                    $"[bold blue]Usuario:[/] {profile.MatchedUserName}  \n" +
                    $"[bold blue]Documento:[/] {profile.MatchedUserDoc}  \n" +
                    $"[bold blue]Fecha del Match:[/] {profile.CreatedAt:yyyy-MM-dd HH:mm}")
                    .Border(BoxBorder.Double)
                    .Padding(1, 1)
                    .Expand();

                AnsiConsole.Write(profilePanel);

                var input = AnsiConsole.Prompt(
    new TextPrompt<string>("[grey]Presiona [[Enter]] para ver otro match o escribe 'salir' para terminar:[/]")
        .AllowEmpty());

                if (!string.IsNullOrEmpty(input) && input.Trim().ToLower() == "salir")
                    break;

                index++;
                Console.Clear();

                // Re-print the header panel after clear
                AnsiConsole.Write(
                    new Panel("[bold orange1]🧡 Tus Matches[/]")
                        .Border(BoxBorder.Rounded)
                        .Expand()
                        .Padding(1, 1));
            }
        }
    }
}
