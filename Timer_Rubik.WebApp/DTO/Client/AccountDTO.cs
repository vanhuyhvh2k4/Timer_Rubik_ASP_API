﻿namespace Timer_Rubik.WebApp.DTO.Client
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterRequestDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Thumbnail { get; set; }
    }

    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
    }

    public class GetAccountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }
    }

    public class UpdateAccountDTO
    {
        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Password { get; set; }
    }
}
