using campusLove.domain.ports;
using campusLove.infrastructure.repositories;
using MySql.Data.MySqlClient;
using sgi_app.domain.factory;


namespace sgi_app.infrastructure.mysql
{
    public class mysqlDbFactory : IDbFactory
    {
        private readonly string _connectionString;

        public mysqlDbFactory(string connectionString)
        {
            _connectionString = connectionString;
            ObtenerConexion();
        }

        public IRegisterRepository ResgisterUserRepository()
        {
            return new registerRepository(ConexionSingleton.Instancia(_connectionString));
        }
        public IMessagesRepository MessagesRepository()
        {
            return new MessageRepository(ConexionSingleton.Instancia(_connectionString));
        }

        public ILoginRepository LoginUserRepository()
        {
            return new LoginRepository(ConexionSingleton.Instancia(_connectionString));
        }

        public IDtoEditRepository editRepository()
        {
            return new DtoEditRepository(ConexionSingleton.Instancia(_connectionString));
        }

         public IStatsRepository StatsRepository()
        {
            return new StatsRepository(ConexionSingleton.Instancia(_connectionString));
        }



        public MySqlConnection ObtenerConexion()
        {
            return ConexionSingleton.Instancia(_connectionString).ObtenerConexion();
        }

        public IProfileRepository ProfileRepository()
        {
            return new ProfileRepository(ConexionSingleton.Instancia(_connectionString));
        }

        public IMatchesRepository matchesRepository()
        {
            return new Matchreposotory(ConexionSingleton.Instancia(_connectionString));
        }
    }
}
