﻿using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces.Repository
{
    public interface IAccountRepository
    {
        ICollection<Account> GetAccounts();

        Account GetAccount(Guid accountId);

        Account GetAccount(string email);

        Account GetAccountByFavorite(Guid favoriteId);

        Account GetAccountByScramble(Guid scrambleId);

        bool AccountExists(Guid accountId);

        bool RegisterAccount(Account account);

        bool Save();

        bool UpdateAccount(Guid accountId, Account account);

        bool UpdateAccount_User(Guid accountId, Account account);

        bool DeleteAccount(Account account);

        bool ChangePassword(Guid accountId, string newPassword);
    }
}
