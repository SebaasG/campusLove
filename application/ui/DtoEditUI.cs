using System;
using campusLove.application.services;

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
                Console.WriteLine("=== EDITAR PERFIL DE USUARIO ===");
                Console.WriteLine($"Usuario: {userDoc}");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Editar Género");
                Console.WriteLine("2. Editar Ciudad");
                Console.WriteLine("3. Editar Carrera");
                Console.WriteLine("4. Editar Frase");
                Console.WriteLine("5. Editar Email");
                Console.WriteLine("6. Activar/Desactivar Usuario");
                Console.WriteLine("0. Volver");
                Console.WriteLine("-------------------------------");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            EditGender(userDoc);
                            break;
                        case "2":
                            EditCity(userDoc);
                            break;
                        case "3":
                            EditCareer(userDoc);
                            break;
                        case "4":
                            EditPhrase(userDoc);
                            break;
                        case "5":
                            EditEmail(userDoc);
                            break;
                        case "6":
                            EditActive(userDoc);
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("❌ Opción no válida.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Ocurrió un error: {ex.Message}");
                    Pause();
                }
            }
        }

        private void EditGender(string userDoc)
        {
            Console.Write("Ingrese nuevo ID de género: ");
            if (int.TryParse(Console.ReadLine(), out int gender))
            {
                _editService.EditGender(userDoc, gender);
                Console.WriteLine("✅ Género actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("❌ Entrada inválida. Debe ser un número.");
            }
            Pause();
        }

        private void EditCity(string userDoc)
        {
            Console.Write("Ingrese nuevo ID de ciudad: ");
            if (int.TryParse(Console.ReadLine(), out int city))
            {
                _editService.EditCity(userDoc, city);
                Console.WriteLine("✅ Ciudad actualizada con éxito.");
            }
            else
            {
                Console.WriteLine("❌ Entrada inválida. Debe ser un número.");
            }
            Pause();
        }

        private void EditCareer(string userDoc)
        {
            Console.Write("Ingrese nuevo ID de carrera: ");
            if (int.TryParse(Console.ReadLine(), out int career))
            {
                _editService.EditCareer(userDoc, career);
                Console.WriteLine("✅ Carrera actualizada con éxito.");
            }
            else
            {
                Console.WriteLine("❌ Entrada inválida. Debe ser un número.");
            }
            Pause();
        }

        private void EditPhrase(string userDoc)
        {
            Console.Write("Ingrese nueva frase: ");
            string phrase = Console.ReadLine();
            _editService.EditPhrase(userDoc, phrase);
            Console.WriteLine("✅ Frase actualizada con éxito.");
            Pause();
        }

        private void EditEmail(string userDoc)
        {
            Console.Write("Ingrese nuevo email: ");
            string email = Console.ReadLine();
            _editService.EditEmail(userDoc, email);
            Console.WriteLine("✅ Email actualizado con éxito.");
            Pause();
        }

        private void EditActive(string userDoc)
        {
            Console.Write("¿Activar usuario? (s/n): ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "s" || input == "n")
            {
                bool isActive = input == "s";
                _editService.EditActive(userDoc, isActive);
                Console.WriteLine("✅ Estado actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("❌ Entrada inválida. Debe ingresar 's' o 'n'.");
            }
            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
