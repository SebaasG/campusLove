using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.domain.ports
{
    public class ProfileRepository : IProfileRepository

    {
        private readonly ConexionSingleton _conn;

        public ProfileRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

        public Profiles? GetNextProfile(string currentUserDoc)
        {
            var con = _conn.ObtenerConexion();
            string query = @"
            SELECT u.doc, u.name, u.age, p.phrase, g.name as gender, c.name as city
            FROM Users u
            JOIN Profiles p ON u.doc = p.userId
            JOIN Gender g ON u.genderId = g.id
            JOIN City c ON u.cityId = c.id
            WHERE u.doc != @currentUser
              AND u.doc NOT IN (
                  SELECT toUser FROM Interaction WHERE fromUser = @currentUser
              )
              AND p.isActive = TRUE
            LIMIT 1;
        ";

            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@currentUser", currentUserDoc);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Profiles
                {
                    doc = reader.GetString("doc"),
                    name = reader.GetString("name"),
                    age = reader.GetInt32("age"),
                    phrase = reader.GetString("phrase"),
                    gender = reader.GetString("gender"),
                    city = reader.GetString("city")
                };
            }

            return null;
        }

        public void RegisterInteraction(string fromUser, string toUser, int typeId)
        {
            var con = _conn.ObtenerConexion();
            string query = @"INSERT INTO Interaction (fromUser, toUser, typeId) VALUES (@from, @to, @type)";
            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@from", fromUser);
            cmd.Parameters.AddWithValue("@to", toUser);
            cmd.Parameters.AddWithValue("@type", typeId);
            cmd.ExecuteNonQuery();
        }

        // Ya no necesitas esto para crear matches, solo como utilidad opcional:
            public bool CheckIfMatchExists(string fromUser, string toUser)
            {
                var con = _conn.ObtenerConexion();
                string query = @"SELECT 1 FROM Matches 
                                    WHERE (user1 = @from AND user2 = @to) 
                                    OR (user1 = @to AND user2 = @from)";
                using var cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@from", fromUser);
                cmd.Parameters.AddWithValue("@to", toUser);
                using var reader = cmd.ExecuteReader();
                return reader.Read();
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