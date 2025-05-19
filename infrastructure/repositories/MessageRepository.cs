using System;
using System.Collections.Generic;
using campusLove.domain.entities;
using campusLove.domain.ports;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class MessageRepository : IMessagesRepository
    {
        private readonly ConexionSingleton _conn;

        public MessageRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

        private string GetDocByUsername(string username, MySqlConnection connec)
        {
            string doc = null;
            string sql = "SELECT docUser FROM Credentials WHERE username = @username LIMIT 1";
            using var cmd = new MySqlCommand(sql, connec);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                doc = reader.GetString("docUser");
            }
            reader.Close();
            return doc;
        }

        public void saveMessage(Messages message)
        {
            var connec = _conn.ObtenerConexion();

            var fromDoc = GetDocByUsername(message.fromUser, connec);
            var toDoc = GetDocByUsername(message.toUser, connec);

            if (fromDoc == null || toDoc == null)
            {
                throw new Exception("Usuario no encontrado para fromUser o toUser");
            }

            var query = "INSERT INTO Messages (fromUser, toUser, content, createdAt) VALUES (@from, @to, @content, @createdAt)";
            using (var command = new MySqlCommand(query, connec))
            {
                command.Parameters.AddWithValue("@from", fromDoc);
                command.Parameters.AddWithValue("@to", toDoc);
                command.Parameters.AddWithValue("@content", message.content);
                command.Parameters.AddWithValue("@createdAt", message.createdAt);

                command.ExecuteNonQuery();
            }
        }

        public List<Messages> getConversation(string username1, string username2)
        {
            var messages = new List<Messages>();
            var connec = _conn.ObtenerConexion();

            string sql = @"
                SELECT m.id, m.fromUser, m.toUser, m.content, m.createdAt
                FROM Messages m
                JOIN Credentials c1 ON m.fromUser = c1.docUser
                JOIN Credentials c2 ON m.toUser = c2.docUser
                WHERE 
                    (c1.username = @user1 AND c2.username = @user2)
                    OR 
                    (c1.username = @user2 AND c2.username = @user1)
                ORDER BY m.createdAt";

            using var cmd = new MySqlCommand(sql, connec);
            cmd.Parameters.AddWithValue("@user1", username1);
            cmd.Parameters.AddWithValue("@user2", username2);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                messages.Add(new Messages
                {
                    Id = reader.GetInt32("id"),
                    fromUser = reader.GetString("fromUser"),
                    toUser = reader.GetString("toUser"),
                    content = reader.GetString("content"),
                    createdAt = reader.GetDateTime("createdAt")
                });
            }

            return messages;
        }

        public List<string> getChatUsers(string username)
        {
            var users = new HashSet<string>(); // Evita duplicados
            var connec = _conn.ObtenerConexion();

            string sql = @"
        SELECT 
            uMatch.username AS matchedUsername
        FROM
            Credentials AS c
        JOIN Matches AS m
            ON c.docUser = m.user1 OR c.docUser = m.user2
        JOIN Users AS u
            ON u.doc = c.docUser
        JOIN Credentials AS uMatch
            ON (
                (m.user1 = uMatch.docUser AND m.user2 = c.docUser)
                OR
                (m.user2 = uMatch.docUser AND m.user1 = c.docUser)
            )
        WHERE 
            c.username = @username
            AND uMatch.username != @username;";

            using var cmd = new MySqlCommand(sql, connec);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(reader.GetString("matchedUsername"));
            }

            return new List<string>(users);
        }

    }

}
