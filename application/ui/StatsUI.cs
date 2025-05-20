using System;
using System.Collections.Generic;
using campusLove.application.services;
using campusLove.domain.entities;
using campusLove.domain.ports;

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
            var statsList = _statsService.GetAllStats();

            if (statsList == null || statsList.Count == 0)
            {
                Console.WriteLine("No hay estadísticas para mostrar.");
                return;
            }

            Console.WriteLine("\n📊 Estadísticas de usuarios:\n");

            foreach (var stat in statsList)
            {
                Console.WriteLine($"Usuario: {stat.UserId}");
                Console.WriteLine($"  Likes Recibidos: {stat.LikesReceived}");
                Console.WriteLine($"  Likes Dados: {stat.LikesGiven}");
                Console.WriteLine($"  Matches: {stat.MatchesCount}");
                Console.WriteLine($"  Último Like: {(stat.LastLikeDate.HasValue ? stat.LastLikeDate.Value.ToString("yyyy-MM-dd HH:mm") : "N/A")}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
