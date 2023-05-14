﻿using Minesweeper_350.Models;

namespace Minesweeper_350.Service
{
    public class SecurityService
    {
        UsersDAO usersDAO= new UsersDAO();

        public SecurityService()
        {
            //empty constructor
        }

        public bool ValidUser(UserModel user)
        {
            return usersDAO.FindByUsernameAndPassword(user);
        }

        public bool Register(UserModel user)
        {
            return usersDAO.RegisterUser(user);
        }

    }
}
