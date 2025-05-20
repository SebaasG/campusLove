using System;
using campusLove.application.services;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class ProfileUI
    {
        private readonly ProfileService _service;

        public ProfileUI(ProfileService service)
        {
            _service = service;
        }

        public void ViewProfiles(string currentUsername)
        {
            string currentUserDoc = _service.FindDoc(currentUsername);

            if (string.IsNullOrEmpty(currentUserDoc))
            {
                AnsiConsole.MarkupLine("[red]Error: No se encontró el documento del usuario.[/]");
                return;
            }

            while (true)
            {
                var profile = _service.GetNextProfile(currentUserDoc);

                if (profile == null)
                {
                    AnsiConsole.MarkupLine("[yellow]No more profiles to view.[/]");
                    break;
                }

                var credits = _service.GetCredits(currentUserDoc);

                Console.Clear();
                var panel = new Panel(new Table()
                    .AddColumn("Campo")
                    .AddColumn("Valor")
                    .AddRow("Nombre", profile.name)
                    .AddRow("Edad", profile.age.ToString())
                    .AddRow("Género", profile.gender)
                    .AddRow("Ciudad", profile.city)
                    .AddRow("Frase", profile.phrase)
                    .AddRow("Créditos disponibles", credits.ToString()))
                {
                    Header = new PanelHeader($"Perfil para: [green]{currentUserDoc}[/]", Justify.Center),
                    Border = BoxBorder.Rounded,
                    Padding = new Padding(1, 1)
                };

                AnsiConsole.Write(panel);

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("¿Qué acción deseas realizar?")
                        .AddChoices(new[] { "Like", "Dislike", "Exit" }));

                if (credits <= 0)
                {
                    AnsiConsole.MarkupLine("[red]No tienes créditos suficientes para interactuar.[/]");
                    break;
                }
                else
                {
                    if (option == "Like")
                    {
                        _service.LikeProfile(currentUserDoc, profile.doc);
                        _service.UpdateCredits(currentUserDoc, credits - 1);
                        AnsiConsole.MarkupLine("[green]Like registrado.[/]");
                    }
                    else if (option == "Dislike")
                    {
                        _service.DislikeProfile(currentUserDoc, profile.doc);
                        _service.UpdateCredits(currentUserDoc, credits - 1);
                        AnsiConsole.MarkupLine("[green]Dislike registrado.[/]");
                    }
                    else // Exit
                    {
                        break;
                    }
                }

                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey(true);
            }
        }

        public void VerificarMatchEntre(string username1, string username2)
        {
            AnsiConsole.MarkupLine($"Verificando si hay match entre [blue]{username1}[/] y [blue]{username2}[/]...");

            if (string.IsNullOrEmpty(username1) || string.IsNullOrEmpty(username2))
            {
                AnsiConsole.MarkupLine("[red]Uno o ambos usuarios no existen.[/]");
                return;
            }

            bool existeMatch = _service.ExistsMatchBetween(username1, username2);

            if (existeMatch)
            {
                AnsiConsole.MarkupLine($"[green]✅ Ya existe un match entre {username1} y {username2}.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]❌ Aún no hay match entre {username1} y {username2}.[/]");
            }
        }
    }
}
