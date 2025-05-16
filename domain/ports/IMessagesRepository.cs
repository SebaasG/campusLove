using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;

namespace campusLove.domain.ports
{
    public interface IMessagesRepository
    {
        // void saveMessage(Messages messages);
        List<Messages> getConversation(string user1, string user2);
    }
}