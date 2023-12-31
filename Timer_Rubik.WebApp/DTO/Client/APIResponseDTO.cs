﻿namespace Timer_Rubik.WebApp.DTO.Client
{
    public class APIResponseDTO<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T? Data { get; set; }
    }
}
