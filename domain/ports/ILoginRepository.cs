using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.ports
{
    public interface ILoginRepository
    {
        string login (string username, string password);
    }
}