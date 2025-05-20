using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.dto;

namespace campusLove.domain.ports
{
    public interface IRegisterRepository
    {
        public void register(DtoRegisterUser user);
        List<(int Id, string Name)> GetGenders();
        List<(int Id, string Name)> GetCities();
        List<(int Id, string Name)> GetCareers();

    }

}