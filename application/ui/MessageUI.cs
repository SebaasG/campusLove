using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;

namespace campusLove.application.ui
{
    public class MessageUI
    {
        public void StartChat(MessageService chatService, string currentUser)
{
    Console.Write("¿Con quién quieres hablar? (Doc): ");
    string toUser = Console.ReadLine();

    Console.WriteLine("Escribe tu mensaje:");
    string message = Console.ReadLine();

    chatService.SendMessage(currentUser, toUser, message);
    Console.WriteLine("Mensaje enviado!");

    Console.WriteLine("\nConversación:");
    var conversation = chatService.GetConversation(currentUser, toUser);
    foreach (var msg in conversation)
    {
        Console.WriteLine($"{msg.fromUser}: {msg.content} ({msg.createdAt})");
    }
}

    }
}