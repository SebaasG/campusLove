using System;
using campusLove.domain.ports;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class DtoEditRepository : IDtoEditRepository
    {
        private readonly ConexionSingleton _conn;

        public DtoEditRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

        public void editGender(string username, int newGender)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateUserField("genderId", newGender, doc);
        }

        public void editCity(string username, int newCity)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateUserField("cityId", newCity, doc);
        }

        public void editCareer(string username, int newCareer)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateUserField("careerId", newCareer, doc);
        }

        public void editPhrase(string username, string newPhrase)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateProfileField("phrase", newPhrase, doc);
        }

        public void editEmail(string username, string newEmail)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateProfileField("email", newEmail, doc);
        }

        public void editActive(string username, bool newStatus)
        {
            var doc = GetDocByUsername(username);
            if (doc == null) throw new Exception("Usuario no encontrado.");
            UpdateProfileField("isActive", newStatus, doc);
        }

        // ======================
        // MÉTODOS PRIVADOS
        // ======================

        private void UpdateUserField(string fieldName, object newValue, string userDoc)
        {
            var connection = _conn.ObtenerConexion();

            string query = $"UPDATE Users SET {fieldName} = @value WHERE doc = @doc";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@value", newValue);
            command.Parameters.AddWithValue("@doc", userDoc);

            command.ExecuteNonQuery();
        }

        private void UpdateProfileField(string fieldName, object newValue, string userDoc)
        {
            var connection = _conn.ObtenerConexion();

            string query = $"UPDATE Profiles SET {fieldName} = @value WHERE userId = @doc";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@value", newValue);
            command.Parameters.AddWithValue("@doc", userDoc);

            command.ExecuteNonQuery();
        }

        private string? GetDocByUsername(string username)
        {
            var connection = _conn.ObtenerConexion();

            const string query = "SELECT docUser FROM Credentials WHERE username = @username LIMIT 1";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            var result = command.ExecuteScalar();
            return result?.ToString();
        }
    }
}
