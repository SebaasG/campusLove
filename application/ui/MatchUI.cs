using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.application.services;

namespace campusLove.application.ui
{
    public class MatchUI
    {
        private readonly MatchService _matchService;

        public MatchUI(MatchService matchService)
        {
            _matchService = matchService;
        }

        public void ShowUserMatches(string userDoc)
        {
            String currentUserDoc = _matchService.FindDoc(userDoc);


            if (string.IsNullOrEmpty(currentUserDoc))
            {
                Console.WriteLine("Error: No se encontró el documento del usuario.");
                return;
            } 
            

                while (true)
{
    var profile = _matchService.GetNextProfile(currentUserDoc);

    Console.WriteLine("\n🧡 Tus Matches:");
    if (profile == null)
    {
        Console.WriteLine("Aún no tienes matches. ¡Sigue interactuando!");
        break;
    }

    Console.WriteLine($"📌 Usuario: {profile.MatchedUserName} (Doc: {profile.MatchedUserDoc})");
    Console.WriteLine($"    ➤ Fecha del Match: {profile.CreatedAt:yyyy-MM-dd HH:mm}");

    Console.WriteLine("\nPresiona [Enter] para ver otro match o escribe 'salir' para terminar.");
    var input = Console.ReadLine();
    if (input?.ToLower() == "salir")
        break;
}
        }
    }
}