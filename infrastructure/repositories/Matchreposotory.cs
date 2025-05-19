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

    }
}