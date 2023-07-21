﻿using BcryptNet = BCrypt.Net.BCrypt;
namespace Timer_Rubik.WebApp.Utils
{
    public class Password
    {
        public static string HashPassword(string password)
        {
            string hashedPassword = BcryptNet.HashPassword(password, BcryptNet.GenerateSalt(10));
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            bool isPasswordCorrect = BcryptNet.Verify(password, hashedPassword);
            return isPasswordCorrect;
        }

        public static string GenerateRandomPassword(int length)
        {
            string[] array = new string[] {"A", "B", "C", "D", "E", "F", "a", "b", "c", "e", "f", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string randomPassword = "";

            for (int dem = 0; dem < length; dem++)
            {
                var random = new Random();
                int randomNumber = random.Next(0, array.Length);
                randomPassword += array[randomNumber];
            }

            return randomPassword;

        }
    }
}
