﻿namespace Timer_Rubik.WebApp.Models
{
    public class Solve
    {
        public Guid Id { get; set; }

        public string Answer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
