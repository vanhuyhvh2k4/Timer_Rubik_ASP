﻿namespace Timer_Rubik.WebApp.Authorize.General.DTO
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Thumbnail { get; set; }
    }
}
