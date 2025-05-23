using System;
using System.Collections.Generic;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class MessageService
    {
        private readonly IMessagesRepository messageRepo;

        public MessageService(IMessagesRepository messageRepo)
        {
            this.messageRepo = messageRepo;
        }

        public void SendMessage(string from, string to, string content)
        {
            var message = new Messages
            {
                fromUser = from,
                toUser = to,
                content = content,
                createdAt = DateTime.Now
            };
            messageRepo.saveMessage(message);
        }

        public List<Messages> GetConversation(string user1, string user2)
        {
            return messageRepo.getConversation(user1, user2);
        }

        public List<string> GetChatsForUser(string username)
        {
            return messageRepo.getChatUsers(username);
        }
    }
}
