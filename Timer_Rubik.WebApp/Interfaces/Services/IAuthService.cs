﻿using Timer_Rubik.WebApp.DTO.Client;

namespace Timer_Rubik.WebApp.Interfaces.Services
{
    public interface IAuthService
    {
        APIResponseDTO<string> Login(LoginRequestDTO loginRequest);

        APIResponseDTO<string> Register(RegisterRequestDTO registerRequest);

        APIResponseDTO<string> Forgot(ForgotPasswordDTO forgotPassword);
    }
}
