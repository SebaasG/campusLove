using System;
using campusLove.application.ui;
using campusLove.application.services;
using campusLove.domain.ports;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class IndexP
    {
        private readonly MessageUI _messagesUI;
        private readonly ProfileUI _profileui;
        private readonly MatchUI _MatchUI;
        private readonly DtoEditUI _Edit;
        private readonly StatsUI _Stats;

        public IndexP(MessageUI messageUI, ProfileUI profileui, MatchUI matchUI, DtoEditUI edit, StatsUI stats)
        {
            _messagesUI = messageUI;
            _profileui = profileui;
            _MatchUI = matchUI;
            _Edit = edit;
            _Stats = stats;
        }

        public void Show(string userName)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                AnsiConsole.Write(
                    new Panel($"[bold yellow]Bienvenido/a[/] [bold underline green]{userName}[/]")
                        .Border(BoxBorder.Rounded)
                        .Header("[bold blue]Campus Love[/]")
                        .Padding(1, 1, 1, 1)
                        .Expand()
                );

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]¿Qué deseas hacer?[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "1. Ver Perfiles",
                            "2. Matches",
                            "3. Mensajes",
                            "4. Tu Perfil",
                            "5. Estadísticas",
                            "6. Cerrar sesión",
                            "7. Salir"
                        }));

                switch (option)
                {
                    case "1. Ver Perfiles":
                        ProfileView(userName);
                        break;
                    case "2. Matches":
                        MatchUI(userName);
                        EsperarTecla();
                        break;
                    case "3. Mensajes":
                        _messagesUI.StartChat(userName);
                        EsperarTecla();
                        break;
                    case "4. Tu Perfil":
                        _Edit.Show(userName);
                        break;
                    case "5. Estadísticas":
                        _Stats.ShowAllStats();
                        EsperarTecla();
                        break;
                    case "6. Cerrar sesión":
                        AnsiConsole.MarkupLine("[grey]Cerrando sesión...[/]");
                        EsperarTecla();
                        running = false;
                        break;
                    case "7. Salir":
                        AnsiConsole.MarkupLine("[red]Saliendo de la aplicación...[/]");
                        Environment.Exit(0);
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Opción inválida. Intente de nuevo.[/]");
                        EsperarTecla();
                        break;
                }
            }
        }

        private void ProfileView(string userName)
        {
            _profileui.ViewProfiles(userName);
        }

        private void MatchUI(string userName)
        {
            _MatchUI.ShowUserMatches(userName);
        }

        private void EsperarTecla()
        {
            AnsiConsole.MarkupLine("[grey]Presiona una tecla para continuar...[/]");
            Console.ReadKey();
        }
    }
}
