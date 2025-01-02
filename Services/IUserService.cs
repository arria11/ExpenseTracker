﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    public interface IUserService
    {
        Task SaveUserAsync(User user);

        Task<List<User>> LoadUserAsync();
    }
}
