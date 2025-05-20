using System;
using System.Collections.Generic;
using campusLove.application.services;
using Spectre.Console;

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
            while (true)
            {
                Console.Clear();

                AnsiConsole.Write(
                    new Panel("[bold yellow]ChatsLove[/]")
                        .Border(BoxBorder.Rounded)
                        .Header("[green]Mensajes[/]")
                        .Expand()
                        .Padding(1, 1));

                var chatUsers = _messageService.GetChatsForUser(currentUser);

                if (chatUsers.Count == 0)
                {
                    AnsiConsole.MarkupLine("[grey]Aún no tienes conversaciones.[/]");
                    EsperarTecla();
                    return;
                }

                const string escribirOtroUsuario = "Escribir otro usuario";
                const string salirOpcion = "Salir";

                var choices = new List<string>(chatUsers)
        {
            escribirOtroUsuario,
            salirOpcion
        };

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("¿Con quién quieres hablar?")
                        .PageSize(10)
                        .AddChoices(choices)
                        .UseConverter(choice =>
                            choice == escribirOtroUsuario || choice == salirOpcion
                                ? $"[red]{choice}[/]"
                                : choice)
                );

                if (selection == salirOpcion)
                {
                    break;
                }

                string toUser;
                if (selection == escribirOtroUsuario)
                {
                    toUser = AnsiConsole.Ask<string>("Escribe el nombre de usuario:");
                }
                else
                {
                    toUser = selection;
                }

                ShowConversation(currentUser, toUser);

                if (!SendMessagePrompt(currentUser, toUser))
                {
                    break;
                }
            }
        }


        private void ShowConversation(string userName, string toUser)
        {
            Console.Clear();

            var conversationPanel = new Panel($"Chat entre [green]{userName}[/] y [green]{toUser}[/]")
                .Border(BoxBorder.Rounded)
                .Header("[bold yellow]Conversación[/]")
                .Expand()
                .Padding(1, 1);

            AnsiConsole.Write(conversationPanel);

            var conversation = _messageService.GetConversation(userName, toUser);

            if (conversation.Count == 0)
            {
                AnsiConsole.MarkupLine("[grey]No hay mensajes aún.[/]");
            }
            else
            {
                foreach (var msg in conversation)
                {
                    AnsiConsole.MarkupLine($"[bold blue]{msg.fromUser}[/]: {msg.content} [grey]({msg.createdAt:dd/MM/yyyy HH:mm})[/]");
                }
            }

            AnsiConsole.MarkupLine(new string('=', 40));
        }

        private bool SendMessagePrompt(string fromUser, string toUser)
        {
            var message = AnsiConsole.Ask<string>("\nEscribe tu mensaje (deja vacío para salir):");

            if (!string.IsNullOrWhiteSpace(message))
            {
                _messageService.SendMessage(fromUser, toUser, message);
                AnsiConsole.MarkupLine("[green]Mensaje enviado.[/]");
                EsperarTecla();
                return true; // Continuar chateando
            }
            else
            {
                AnsiConsole.MarkupLine("[grey]No se envió ningún mensaje. Saliendo del chat...[/]");
                EsperarTecla();
                return false; // Salir del chat
            }
        }


        private void EsperarTecla()
        {
            AnsiConsole.MarkupLine("[grey]Presiona una tecla para continuar...[/]");
            Console.ReadKey(true);
        }
    }
}
