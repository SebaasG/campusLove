using System;
using System.Collections.Generic;
using campusLove.domain.entities;
using campusLove.domain.ports;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly ConexionSingleton _conn;

        public StatsRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

        public List<Stats> GetAll()
        {
            var statsList = new List<Stats>();

            var connec = _conn.ObtenerConexion();

            string query = "SELECT userId, likesReceived, likesGiven, matchesCount, lastLikeDate FROM Stats";

            using (var command = new MySqlCommand(query, connec))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var stats = new Stats
                        {
                            UserId = reader["userId"].ToString(),
                            LikesReceived = Convert.ToInt32(reader["likesReceived"]),
                            LikesGiven = Convert.ToInt32(reader["likesGiven"]),
                            MatchesCount = Convert.ToInt32(reader["matchesCount"]),
                            LastLikeDate = reader["lastLikeDate"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["lastLikeDate"])
                        };

                        statsList.Add(stats);
                    }
                }
            }

            return statsList;
        }
    }
}
