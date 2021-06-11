using Bogus;
using SoccerBet.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerBet.Test.Builders
{
    public class UserBuilder
    {
        Faker fake;

        public UserBuilder()
        {
            fake = new Faker();
        }

        public static UserBuilder New()
        {
            return new UserBuilder();
        }

        public RegisterUserViewModel CreateRegisterUser()
        {
            var registerUser = new RegisterUserViewModel()
            {
                Email = fake.Person.Email,
                Password = "teste123",
                ConfirmPassword = "teste123"
            };

            return registerUser;
        }

        public RegisterUserViewModel CreateRegisterUserWithNoConfirmPassword()
        {
            var registerUser = new RegisterUserViewModel()
            {
                Email = fake.Person.Email,
                Password = "teste123"
            };

            return registerUser;
        }

        public LoginUserViewModel CreateLoginUser()
        {
            var loginUserViewModel = new LoginUserViewModel()
            {
                Email = fake.Person.Email,
                Password = "teste123"
            };

            return loginUserViewModel;
        }

        public LoginUserViewModel CreateLoginUserWithEmptyPassword()
        {
            var loginUserViewModel = new LoginUserViewModel()
            {
                Email = fake.Person.Email
            };

            return loginUserViewModel;
        }

        public RegisterUserViewModel Build()
        {
            return CreateRegisterUser();
        }
    }
}
