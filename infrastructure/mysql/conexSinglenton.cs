using MySql.Data.MySqlClient;

namespace sgi_app.infrastructure.mysql
{
    public class ConexionSingleton
    {
        private static ConexionSingleton? _instancia;
        private readonly string _connectionString;
        private MySqlConnection? _conexion;

        private ConexionSingleton(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static ConexionSingleton Instancia(string connectionString)
        {
            _instancia ??= new ConexionSingleton(connectionString);
            return _instancia;
        }

        public MySqlConnection ObtenerConexion()
        {
            if (_conexion == null)
            {
                try
                {
                    
                    _conexion = new MySqlConnection(_connectionString);
                    Console.WriteLine("Conexión a la base de datos creada.");
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Conexión a la base de datos fffff.");
                    throw new Exception("Error al crear la conexión a la base de datos: " + ex.Message);
                }
         
            }

            if (_conexion.State != System.Data.ConnectionState.Open)
            {
                _conexion.Open();
            }

            return _conexion;
        }


    }
}
