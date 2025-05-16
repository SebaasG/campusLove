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

        // public void saveMessage(Messages messages)
        // {
        //     var connec = _conn.ObtenerConexion();
        //     var query = "INSERT INTO Messages (fromUser, toUser, content) VALUES (@from, @to, @content)";
        //     using (var command = new MySqlCommand(query, connec))
        //     {
        //         command.Parameters.AddWithValue("@from", messages.fromUser);
        //         command.Parameters.AddWithValue("@to", messages.toUser);
        //         command.Parameters.AddWithValue("@content", messages.content);

        //         command.ExecuteNonQuery();
        //     }
        // }

        public List<Messages> getConversation(string user1, string user2)
        {
            var messages = new List<Messages>();
            var connec = _conn.ObtenerConexion();

            string sql = @"
                SELECT * FROM Messages
                WHERE (fromUser = @user1 AND toUser = @user2)
                   OR (fromUser = @user2 AND toUser = @user1)
                ORDER BY createdAt";

            using var cmd = new MySqlCommand(sql, connec);
            cmd.Parameters.AddWithValue("@user1", user1);
            cmd.Parameters.AddWithValue("@user2", user2);

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
    }
}
