using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.entities;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class ProfileService
    {
        private readonly IProfileRepository _repository;

        private readonly IMatchesRepository _matchesRepository;
        private readonly IEmailRepository _emailService;


        public ProfileService(IProfileRepository repository, IMatchesRepository matchesRepository, IEmailRepository emailService)
        {
            _emailService = emailService;
            _repository = repository;
            _matchesRepository = matchesRepository;
        }

        public Profiles GetNextProfile(string currentUserDoc)
        {
            return _repository.GetNextProfile(currentUserDoc);
        }

        public async void LikeProfile(string fromUser, string toUser)
        {
            _repository.RegisterInteraction(fromUser, toUser, 1);
            string email1 = getEmailByUsername(fromUser);
            string email2 = getEmailByUsername(toUser);
            Console.WriteLine(email1+ "este es el 1");
            Console.WriteLine(email2 + "este es el 2");
            Console.WriteLine("esperando una tecla");
            Console.ReadKey();

            bool email = ExistsMatchBetween(fromUser, toUser);
            Console.WriteLine("este es el resultado",email);
            Console.ReadKey();
            if (email == true)
            {
                var subject = "Match Found!";
                var body = $"Congratulations! You have a new match with {toUser}.";
                await _emailService.SendEmailAsync(email1, subject, body);
                await _emailService.SendEmailAsync(email2, subject, body);
                Console.WriteLine("enviando correos");
                Console.ReadKey();
            }
        }

        public void DislikeProfile(string fromUser, string toUser)
        {
            _repository.RegisterInteraction(fromUser, toUser, 2);
        }
        public string FindDoc(String username)
        {
            return _repository.GetDocByUsername(username);
        }
        public int GetCredits(string doc)
        {
            return _repository.GetCreditsByDoc(doc);
        }
        public void UpdateCredits(string doc, int credits)
        {
            _repository.UpdateCredits(doc, credits);
        }


        public bool ExistsMatchBetween(string userDoc1, string userDoc2)
        {
            return _matchesRepository.MatchExists(userDoc1, userDoc2);

        }
        
        public string getEmailByUsername(string username)
        {
            return _matchesRepository.getEmailByUsername(username);
        }

    }
}
