using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //         public void StartChat(MessageService chatService, string currentUser)
        // {
        //     Console.Write("¿Con quién quieres hablar? (Doc): ");
        //     string toUser = Console.ReadLine();

        //     Console.WriteLine("Escribe tu mensaje:");
        //     string message = Console.ReadLine();

        //     // chatService.SendMessage(currentUser, toUser, message);
        //     // Console.WriteLine("Mensaje enviado!");

        //     Console.WriteLine("\nConversación:");
        //     var conversation = chatService.GetConversation(currentUser, toUser);
        //     foreach (var msg in conversation)
        //     {
        //         Console.WriteLine($"{msg.fromUser}: {msg.content} ({msg.createdAt})");
        //     }
        // }
        public void allChats(string userName, string toUser)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("           ChatsLove            ");
            Console.WriteLine("===================================");
            Console.WriteLine(_messageService.GetConversation(userName, toUser));
            Console.WriteLine("===================================");

            // Aquí puedes agregar la lógica para mostrar los chats disponibles
        }
        
    }
}