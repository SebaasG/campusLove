using System;
using System.Collections.Generic;
using System.Linq;
using campusLove.domain.ports;



namespace sgi_app.domain.factory
{
    public interface IDbFactory
    {
        IRegisterRepository ResgisterUserRepository();
        ILoginRepository LoginUserRepository();

        IMessagesRepository MessagesRepository();

        IProfileRepository ProfileRepository();
        
        IMatchesRepository matchesRepository();

    }
}