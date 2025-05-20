using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class StatsService
    {
        private readonly IStatsRepository _statsRepository;

        public StatsService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public List<Stats> GetAllStats()
        {
            return _statsRepository.GetAll();
        }
    }
}
