﻿using Timer_Rubik.WebApp.Authorize.User.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Repository
{
    public class FavoriteRepository_U : IFavoriteRepository_U
    {
        private readonly DataContext _context;

        public FavoriteRepository_U(DataContext context)
        {
            _context = context;
        }

        public bool CreateFavorite(Favorite favorite)
        {
            var newFavorite = new Favorite()
            {
                Id = favorite.Id,
                AccountId = favorite.AccountId,
                ScrambleId = favorite.ScrambleId,
                Time = favorite.Time,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Favorites.Add(newFavorite);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
