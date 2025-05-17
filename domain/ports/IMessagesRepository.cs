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
void saveMessage(Messages message);
List<Messages> getConversation(string user1, string user2);
List<string> getChatUsers(string username);
    }
}