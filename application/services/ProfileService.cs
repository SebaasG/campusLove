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

            bool email = ExistsMatchBetween(fromUser, toUser);
            if (email == true)
            {
                var subject = " ¡CAMPUSLOVE MATCH ENCONTRADO! ";
                var body = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; background-color: #fef6f9; padding: 20px; color: #333;'>
                        <div style='max-width: 600px; margin: auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0px 0px 15px rgba(0,0,0,0.1);'>
                            <h2 style='text-align: center; color: #e91e63;'>💘 ¡Felicitaciones!</h2>
                            <p style='font-size: 16px;'>🎉 Has hecho match con <strong>{toUser}</strong>.</p>
                            <p style='font-size: 16px;'>Ahora pueden empezar a conocerse mejor. ¡El amor está en el aire! 💌</p>
                            <hr style='margin: 20px 0;' />
                        </div>
                    </body>
                </html>";
                
                await _emailService.SendEmailAsync(email1, subject, body);
                await _emailService.SendEmailAsync(email2, subject, body);
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
