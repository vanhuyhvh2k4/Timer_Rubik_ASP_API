﻿namespace Timer_Rubik.WebApp.Models
{
    public class Scramble
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string? Solve { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Account Account { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public Category Category { get; set; }
    }
}
