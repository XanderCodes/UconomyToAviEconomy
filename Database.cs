﻿using com.aviadmini.rocketmod.AviEconomy;

namespace fr34kyn01535.Uconomy
{
    public class DatabaseManager
    {
        internal DatabaseManager()
        {

        }

        /// <summary>
        /// returns the current balance of an account
        /// </summary>
        /// <param name="steamId"></param>
        /// <returns></returns>
        public decimal GetBalance(string id)
        {
            var balance = Bank.GetBalance(id);
            Uconomy.Instance.OnBalanceChecked(id, balance);
            return balance;
        }

        /// <summary>
        /// Increasing balance to increaseBy (can be negative)
        /// </summary>
        /// <param name="steamId">steamid of the accountowner</param>
        /// <param name="increaseBy">amount to change</param>
        /// <returns>the new balance</returns>
        public decimal IncreaseBalance(string steamId, decimal increaseBy)
        {
            Bank.PerformPayout(steamId, increaseBy, false);
            Uconomy.Instance.BalanceUpdated(steamId, increaseBy);
            return GetBalance(steamId);
        }
    }
}
