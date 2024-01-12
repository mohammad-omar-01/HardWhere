﻿using Application.DTOs.User;
using Domain.UserNS;
using System.Security.Cryptography;

namespace Application.Services.Authintication
{
    public interface IUserAuthicticateService
    {
        public bool IsUsernameAvaliable(string username);
        public bool IsEmailAvaliable(string email);
        public string? Login(UserSignInDTO user);
        public bool Logout(LogoutRequestDTO logoutRequest);
        public bool SignUp(UserSignUpDTO user);
    }
}