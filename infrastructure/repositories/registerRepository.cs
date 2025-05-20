using System;
using System.Collections.Generic;
using campusLove.domain.dto;
using campusLove.domain.ports;
using campusLove.infrastructure.helpers;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class registerRepository : IRegisterRepository
    {
        private readonly ConexionSingleton _conn;

        public registerRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

        public void register(DtoRegisterUser user)
        {
            var connec = _conn.ObtenerConexion();

            string query = "CALL registerUser(@_Doc, @_Name, @_LastName, @_Age, @_GenderId, @_CityId, @_CareerId, @_UserName, @_Password, @_Phrase, @_Email);";

            using (var command = new MySqlCommand(query, connec))
            {
                string passwordHashed = PasswordHasher.HashPassword(user.password);
                command.Parameters.AddWithValue("@_Doc", user.doc);
                command.Parameters.AddWithValue("@_Name", user.name);
                command.Parameters.AddWithValue("@_LastName", user.lastName);
                command.Parameters.AddWithValue("@_Age", user.age);
                command.Parameters.AddWithValue("@_GenderId", user.genderId);
                command.Parameters.AddWithValue("@_CityId", user.cityId);
                command.Parameters.AddWithValue("@_CareerId", user.careerId);
                command.Parameters.AddWithValue("@_UserName", user.username);
                command.Parameters.AddWithValue("@_Password", passwordHashed);
                command.Parameters.AddWithValue("@_Phrase", user.Phrase);
                command.Parameters.AddWithValue("@_Email", user.email);

                command.ExecuteNonQuery();
            }
        }

        public List<(int Id, string Name)> GetGenders()
        {
            var genders = new List<(int Id, string Name)>();
            var connec = _conn.ObtenerConexion();
            string query = "SELECT id, name FROM Gender";

            using (var command = new MySqlCommand(query, connec))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    genders.Add((reader.GetInt32("id"), reader.GetString("name")));
                }
            }
            return genders;
        }

        public List<(int Id, string Name)> GetCities()
        {
            var cities = new List<(int Id, string Name)>();
            var connec = _conn.ObtenerConexion();
            string query = "SELECT id, name FROM City";

            using (var command = new MySqlCommand(query, connec))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cities.Add((reader.GetInt32("id"), reader.GetString("name")));
                }
            }
            return cities;
        }

        public List<(int Id, string Name)> GetCareers()
        {
            var careers = new List<(int Id, string Name)>();
            var connec = _conn.ObtenerConexion();
            string query = "SELECT id, name FROM Career";

            using (var command = new MySqlCommand(query, connec))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    careers.Add((reader.GetInt32("id"), reader.GetString("name")));
                }
            }
            return careers;
        }
    }
}
