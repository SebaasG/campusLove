using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;

namespace campusLove.application.ui
{
    public class ProfileUI
    {
        private readonly ProfileService _service;

        public ProfileUI(ProfileService service)
        {
            _service = service;
        }

        public void ViewProfiles(string currentUsername)
        {

            String currentUserDoc = _service.FindDoc(currentUsername);

            if (string.IsNullOrEmpty(currentUserDoc))
            {
                Console.WriteLine("Error: No se encontró el documento del usuario.");
                return;
            }

            while (true)
            {


                var profile = _service.GetNextProfile(currentUserDoc);

                if (profile == null)
                {
                    Console.WriteLine("No more profiles to view.");
                    break;
                }
                var credits = _service.GetCredits(currentUserDoc);

                Console.Clear();
                Console.WriteLine("username: " + currentUserDoc);
                Console.WriteLine("===================================");
                Console.WriteLine($"Name: {profile.name}, Age: {profile.age}");
                Console.WriteLine($"Gender: {profile.gender}, City: {profile.city}");
                Console.WriteLine($"Phrase: {profile.phrase}");
                Console.WriteLine("===================================");
                Console.WriteLine($"Tienes {credits} créditos.");
                Console.WriteLine("1. Like");
                Console.WriteLine("2. Dislike");
                Console.WriteLine("3. Exit");

                var option = Console.ReadLine();



                if (credits <= 0)
                {
                    Console.WriteLine("No tienes créditos suficientes para interactuar.");
                    break;
                }
                else if (credits > 0)
                {
                    if (option == "1")
                    {
                        Console.WriteLine("Esta enrnado aqui");
                        _service.LikeProfile(currentUserDoc, profile.doc);
                        Console.WriteLine("Like registrado.");
                        Console.ReadKey();
                        _service.UpdateCredits(currentUserDoc, credits - 1);
                    }
                    else if (option == "2")
                    {
                        _service.DislikeProfile(currentUserDoc, profile.doc);
                        _service.UpdateCredits(currentUserDoc, credits - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    

public void VerificarMatchEntre(string username1, string username2)
        {
            Console.WriteLine($"Verificando si hay match entre {username1} y {username2}...");


            if (string.IsNullOrEmpty(username1) || string.IsNullOrEmpty(username2))
            {
                Console.WriteLine("Uno o ambos usuarios no existen.");
                return;
            }

            bool existeMatch = _service.ExistsMatchBetween(username1, username2);

            if (existeMatch)
            {
                Console.WriteLine($"✅ Ya existe un match entre {username1} y {username2}.");
            }
            else
            {
                Console.WriteLine($"❌ Aún no hay match entre {username1} y {username2}.");
            }
        }
            }


        }
    
