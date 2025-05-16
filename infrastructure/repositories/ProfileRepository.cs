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

    public bool CheckIfMatchExists(string fromUser, string toUser)
    {
        var con = _conn.ObtenerConexion();
        string query = @"SELECT 1 FROM Interaction WHERE fromUser = @from AND toUser = @to AND typeId = 1";
        using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@from", fromUser);
        cmd.Parameters.AddWithValue("@to", toUser);
        using var reader = cmd.ExecuteReader();
        return reader.Read();
    }

    public void CreateMatch(string user1, string user2)
    {
        var con = _conn.ObtenerConexion();
        string query = @"INSERT INTO Matches (user1, user2) VALUES (@u1, @u2)";
        using var cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@u1", user1);
        cmd.Parameters.AddWithValue("@u2", user2);
        cmd.ExecuteNonQuery();
    }
    }
}