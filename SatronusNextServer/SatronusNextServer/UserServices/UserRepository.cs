using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatronusNextServer.UserServices
{
    public class UserRepository : IUserRepository
    {
        private readonly List<CustomUser> users = new List<CustomUser>
        {
            new CustomUser
            {
                SubjectId = "00",
                UserName = "Rei",
                Password = "Rei",
                Email = "ayanamirei@gmail.com"
            },
            new CustomUser
            {
                SubjectId = "01",
                UserName = "Shinji",
                Password = "Shinji",
                Email = "ikarishinji@gmail.com"
            },
            new CustomUser
            {
                SubjectId = "02",
                UserName = "Asuka",
                Password = "Asuka",
                Email = "langleyasuka@gmail.com"
            }

        };
        public CustomUser FindBySubjectId(string subjectId)
        {
            return users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public CustomUser FindByUsername(string username)
        {
            return users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }
            return false;
        }
    }
}
