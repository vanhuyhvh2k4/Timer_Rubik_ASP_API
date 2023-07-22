﻿namespace Timer_Rubik.WebApp.Interfaces.Utils
{
    public interface IEmailService
    {
        void SendEmail(string toAddress, string subject, string body);

        bool EmailValid(string email);
    }
}