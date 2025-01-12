using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    public class AuthenticationStateService
    {
        private User authenticatedUser;

        public User GetAuthenticatedUser()
        {
            return authenticatedUser;
        }

        public void SetAuthenticatedUser(User user)
        {
            authenticatedUser = user;
        }

        public bool IsAuthenticated()
        {
            if (authenticatedUser != null)
            {
                return true;
            }
            return false;
        }

        public Task Logout()
        {
            authenticatedUser = null;

            return Task.CompletedTask;
        }
    }
}
