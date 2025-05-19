using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.dto;
using campusLove.domain.ports;
using Org.BouncyCastle.Crypto.Generators;
using sgi_app.infrastructure.mysql;
using campusLove.infrastructure.helpers;
using MySql.Data.MySqlClient;

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
            
            string query = "CALL registerUser(@_Doc, @_Name, @_LastName, @_Age, @_GenderId, @_CityId, @_CareerId, @_UserName, @_Password, @_Phrase);";

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

                command.ExecuteNonQuery();
            }
        }
    }
}