using System;
using System.Collections.Generic;
using campusLove.application.services;

namespace campusLove.application.ui
{
    public class MessageUI
    {
        private readonly MessageService _messageService;

        public MessageUI(MessageService messageService)
        {
            _messageService = messageService;
        }

        public void StartChat(string currentUser)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("           ChatsLove               ");
            Console.WriteLine("===================================");

            var chatUsers = _messageService.GetChatsForUser(currentUser);

            if (chatUsers.Count == 0)
            {
                Console.WriteLine("Aún no tienes conversaciones.");
            }
            else
            {
                Console.WriteLine("Conversaciones previas:");
                for (int i = 0; i < chatUsers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {chatUsers[i]}");
                }
            }

            Console.WriteLine("\n¿Con quién quieres hablar?");
            Console.Write("Escribe el número o el nombre de usuario: ");
            string input = Console.ReadLine();
            string toUser;

            if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= chatUsers.Count)
            {
                toUser = chatUsers[selectedIndex - 1];
            }
            else
            {
                toUser = input;
            }

            ShowConversation(currentUser, toUser);
            SendMessagePrompt(currentUser, toUser);
        }

        private void ShowConversation(string userName, string toUser)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine($"Chat entre {userName} y {toUser}");
            Console.WriteLine("===================================");

            var conversation = _messageService.GetConversation(userName, toUser);
            foreach (var msg in conversation)
            {
                Console.WriteLine($"{msg.fromUser}: {msg.content} ({msg.createdAt})");
            }

            Console.WriteLine("===================================");
        }

        private void SendMessagePrompt(string fromUser, string toUser)
        {
            Console.WriteLine("\nEscribe tu mensaje (o deja vacío para salir):");
            string message = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(message))
            {
                _messageService.SendMessage(fromUser, toUser, message);
                Console.WriteLine("Mensaje enviado.");
            }
            else
            {
                Console.WriteLine("No se envió ningún mensaje.");
            }
        }
    }
}
