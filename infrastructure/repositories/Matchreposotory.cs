using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class Matchreposotory : IMatchesRepository
    {
        private readonly ConexionSingleton _conn;
        public Matchreposotory(ConexionSingleton coon)
        {
            _conn = coon;
        }

        public List<MacthInfo> GetMatchesForUser(string currentUserDoc)
        {
            var con = _conn.ObtenerConexion();
            string query = @"
        SELECT 
            u.doc, u.name, m.createdAt
        FROM Matches m
        JOIN Users u ON 
            (m.user1 = u.doc AND m.user2 = @userDoc) OR
            (m.user2 = u.doc AND m.user1 = @userDoc)
        WHERE 
            u.doc != @userDoc";

            var matches = new List<MacthInfo>();

            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userDoc", currentUserDoc);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                matches.Add(new MacthInfo
                {
                    MatchedUserDoc = reader.GetString("doc"),
                    MatchedUserName = reader.GetString("name"),
                    CreatedAt = reader.GetDateTime("createdAt"),
                });
            }

            return matches;
        }

        public string GetDocByUsername(string username)
        {
            var connec = _conn.ObtenerConexion();
            var query = "SELECT docUser FROM Credentials WHERE username = @username LIMIT 1";

            using (var command = new MySqlCommand(query, connec))
            {
                command.Parameters.AddWithValue("@username", username);
                var result = command.ExecuteScalar();
                return result?.ToString();
            }
        }

        public bool MatchExists(string user1, string user2)
        {
            var con = _conn.ObtenerConexion();
            string query = @"
        SELECT COUNT(*) FROM Matches 
        WHERE (user1 = @user1 AND user2 = @user2) 
           OR (user1 = @user2 AND user2 = @user1)";

            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user1", user1);
            cmd.Parameters.AddWithValue("@user2", user2);

            var result = Convert.ToInt32(cmd.ExecuteScalar());
            return result > 0;
        }
        public string getEmailByUsername(string docUser)
        {
            var connec = _conn.ObtenerConexion();
            var query = @"SELECT p.email 
                        FROM Credentials c
                        JOIN Profiles p ON p.userId = c.docUser
                        WHERE c.docUser = @doc
                        LIMIT 1";

            using (var command = new MySqlCommand(query, connec))
            {
                command.Parameters.AddWithValue("@doc", docUser);
                var result = command.ExecuteScalar();
                return result?.ToString();
            }
        }


    }
}