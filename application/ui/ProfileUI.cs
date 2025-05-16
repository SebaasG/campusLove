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

        public void ViewProfiles(string currentUserDoc)
        {
            while (true)
            {
                var profile = _service.GetNextProfile(currentUserDoc);

                if (profile == null)
                {
                    Console.WriteLine("No more profiles to view.");
                    break;
                }

                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine($"Name: {profile.name}, Age: {profile.age}");
                Console.WriteLine($"Gender: {profile.gender}, City: {profile.city}");
                Console.WriteLine($"Phrase: {profile.phrase}");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Like");
                Console.WriteLine("2. Dislike");
                Console.WriteLine("3. Exit");
            }
        }
    }
}