using System;
using System.Collections.Generic;
using campusLove.application.services;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class StatsUI
    {
        private readonly StatsService _statsService;

        public StatsUI(StatsService statsService)
        {
            _statsService = statsService;
        }

        public void ShowAllStats()
        {
            Console.Clear();

            var statsList = _statsService.GetAllStats();

            if (statsList == null || statsList.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay estadísticas para mostrar.[/]");
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey(true);
                return;
            }

            AnsiConsole.Write(
                new Panel("[bold purple]📊 Estadísticas de usuarios[/]")
                    .Border(BoxBorder.Rounded)
                    .Expand()
                    .Padding(1,1));

            var table = new Table()
                .Expand()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Usuario[/]")
                .AddColumn("[green]Likes Recibidos[/]")
                .AddColumn("[green]Likes Dados[/]")
                .AddColumn("[orange1]Matches[/]")
                .AddColumn("[cyan]Último Like[/]");

            foreach (var stat in statsList)
            {
                table.AddRow(
                    stat.UserId.ToString(),
                    stat.LikesReceived.ToString(),
                    stat.LikesGiven.ToString(),
                    stat.MatchesCount.ToString(),
                    stat.LastLikeDate.HasValue ? stat.LastLikeDate.Value.ToString("yyyy-MM-dd HH:mm") : "N/A"
                );
            }

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
            Console.ReadKey(true);
        }
    }
}
