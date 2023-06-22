using RegisterAndLoginApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RegisterAndLoginApp.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();
       /* List<UserModel> knownUsers = new List<UserModel>();

        public SecurityService() 
        {
            knownUsers.Add(new UserModel { Id = 0, UserName = "dwightschrute", Password = "theoffice" });
            knownUsers.Add(new UserModel { Id = 1, UserName = "jimhalpert", Password = "thestud" });
            knownUsers.Add(new UserModel { Id = 2, UserName = "michaelscott", Password = "theboss" });
            knownUsers.Add(new UserModel { Id = 3, UserName = "pambeasley", Password = "thegirl" });
        }
       */
        public bool IsValid(UserModel user)
        {
            // return knownUsers.Any(x => x.UserName == user.UserName && x.Password == user.Password);
            return securityDAO.FindUserByNameAndPassword(user);
        }
    }
}
