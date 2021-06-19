﻿using SoccerBet.App.Models;
using SoccerBet.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerBet.App.Interfaces
{
    public interface IAccountService
    {
        Task<UserToken> Login(LoginViewModel login);
    }
}
