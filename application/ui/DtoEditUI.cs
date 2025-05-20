using System;
using campusLove.application.services;
using Spectre.Console;

namespace campusLove.application.ui
{
    public class DtoEditUI
    {
        private readonly DtoEditService _editService;

        public DtoEditUI(DtoEditService editService)
        {
            _editService = editService;
        }

        public void Show(string userDoc)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                AnsiConsole.Write(
                    new Panel($"[bold yellow]Editar Perfil de Usuario[/]\n[grey]Usuario: {userDoc}[/]")
                        .Border(BoxBorder.Rounded)
                        .Header("[green]Menú de Edición[/]")
                        .Expand()
                        .Padding(1, 1));

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione una opción:")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "1. Editar Género",
                            "2. Editar Ciudad",
                            "3. Editar Carrera",
                            "4. Editar Frase",
                            "5. Editar Email",
                            "6. Activar/Desactivar Usuario",
                            "0. Volver"
                        }));

                try
                {
                    switch (option[0])
                    {
                        case '1':
                            EditGender(userDoc);
                            break;
                        case '2':
                            EditCity(userDoc);
                            break;
                        case '3':
                            EditCareer(userDoc);
                            break;
                        case '4':
                            EditPhrase(userDoc);
                            break;
                        case '5':
                            EditEmail(userDoc);
                            break;
                        case '6':
                            EditActive(userDoc);
                            break;
                        case '0':
                            running = false;
                            break;
                        default:
                            AnsiConsole.MarkupLine("[red]❌ Opción no válida.[/]");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"\n[red]❌ Ocurrió un error: {ex.Message}[/]");
                    Pause();
                }
            }
        }

        private void EditGender(string userDoc)
        {
            int? gender = PromptForInt("Ingrese nuevo ID de género:");

            if (gender.HasValue)
            {
                _editService.EditGender(userDoc, gender.Value);
                AnsiConsole.MarkupLine("[green]✅ Género actualizado con éxito.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]❌ Entrada inválida. Debe ser un número.[/]");
            }
            Pause();
        }

        private void EditCity(string userDoc)
        {
            int? city = PromptForInt("Ingrese nuevo ID de ciudad:");

            if (city.HasValue)
            {
                _editService.EditCity(userDoc, city.Value);
                AnsiConsole.MarkupLine("[green]✅ Ciudad actualizada con éxito.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]❌ Entrada inválida. Debe ser un número.[/]");
            }
            Pause();
        }

        private void EditCareer(string userDoc)
        {
            int? career = PromptForInt("Ingrese nuevo ID de carrera:");

            if (career.HasValue)
            {
                _editService.EditCareer(userDoc, career.Value);
                AnsiConsole.MarkupLine("[green]✅ Carrera actualizada con éxito.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]❌ Entrada inválida. Debe ser un número.[/]");
            }
            Pause();
        }

        private void EditPhrase(string userDoc)
        {
            var phrase = AnsiConsole.Ask<string>("Ingrese nueva frase:");
            _editService.EditPhrase(userDoc, phrase);
            AnsiConsole.MarkupLine("[green]✅ Frase actualizada con éxito.[/]");
            Pause();
        }

        private void EditEmail(string userDoc)
        {
            var email = AnsiConsole.Ask<string>("Ingrese nuevo email:");
            _editService.EditEmail(userDoc, email);
            AnsiConsole.MarkupLine("[green]✅ Email actualizado con éxito.[/]");
            Pause();
        }

        private void EditActive(string userDoc)
        {
            var isActive = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("¿Activar usuario?")
                    .AddChoices(new[] { "Sí", "No" }));

            _editService.EditActive(userDoc, isActive == "Sí");
            AnsiConsole.MarkupLine("[green]✅ Estado actualizado con éxito.[/]");
            Pause();
        }

        private int? PromptForInt(string message)
        {
            var input = AnsiConsole.Ask<string>(message);
            if (int.TryParse(input, out int value))
                return value;
            return null;
        }

        private void Pause()
        {
            AnsiConsole.MarkupLine("\n[grey]Presione cualquier tecla para continuar...[/]");
            Console.ReadKey(true);
        }
    }
}
