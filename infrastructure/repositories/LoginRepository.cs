using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.ports;
using campusLove.infrastructure.helpers;
using MySql.Data.MySqlClient;
using sgi_app.infrastructure.mysql;

namespace campusLove.infrastructure.repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly ConexionSingleton _conn;
        
        public LoginRepository(ConexionSingleton conn)
        {
            _conn = conn;
        }

    //     public bool login(string username, string password)
    //     {
    //         var connec = _conn.ObtenerConexion();
    //         var query = "CALL loginUser(@_UserName, @_Password);";
    //         using (var command = new MySqlCommand(query, connec))
    //         {
    //             Console.WriteLine("Executing stored procedure...");
                
    //             var passwordHashed = PasswordHasher.HashPassword(password);
    //             Console.WriteLine("pass" + passwordHashed);
    //             command.Parameters.AddWithValue("@_UserName", username);
    //             command.Parameters.AddWithValue("@_Password", passwordHashed);

    //         var reader = command.ExecuteReader();
    //         if (reader.Read())
    //         {
    //             var message = reader["message"].ToString();
    //             Console.WriteLine("message: " + message);
    //             if (message == "1")
    //             {
    //                 Console.WriteLine("Login successful!");
    //                 Console.WriteLine("Welcome " + username);
    //                 Console.WriteLine("Press any key to continue...");
    //                 Console.ReadKey();
    //                 return true;
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Invalid username or password.");
    //                 Console.WriteLine("Press any key to continue...");
    //                 Console.ReadKey();
    
    //                 return false;
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("No data returned from stored procedure.");

    //             return false;
    //         }
      
    //         }
    //     }
    // }
public string login(string username, string password)
{
        var connec = _conn.ObtenerConexion();
    
        var query = "CALL loginUser(@_UserName);";
        string noFound = "404";

        using (var command = new MySqlCommand(query, connec))
            {
                Console.WriteLine("Executing stored procedure...");

                command.Parameters.AddWithValue("@_UserName", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var storedHash = reader["password"].ToString();

                        if (PasswordHasher.VerifyPassword(password, storedHash))
                        {
                            Console.WriteLine("Login successful!");
                            Console.WriteLine("Welcome " + username);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            return username;
                        }
                        else
                        {
                            Console.WriteLine("Invalid password.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            return noFound;
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return noFound;
                    }
                }
            } 
    } 
}


    }
