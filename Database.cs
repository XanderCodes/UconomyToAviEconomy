using System;

using com.aviadmini.rocketmod.AviEconomy;

// ReSharper disable InconsistentNaming
namespace fr34kyn01535.Uconomy {

    public class DatabaseManager {

        internal DatabaseManager() { }

        /// <summary>
        /// returns the current balance of an account
        /// </summary>
        /// <param name="playerId">RocketPlayer ID of the account owner</param>
        /// <returns></returns>
        public decimal GetBalance(string playerId) {
            decimal balance = Bank.GetBalance(playerId);
            Uconomy.Instance.OnBalanceChecked(playerId, balance);
            return balance;
        }

        /// <summary>
        /// Increasing balance to increaseBy (can be negative)
        /// </summary>
        /// <param name="playerId">Rocket player id of account owner</param>
        /// <param name="increaseBy">amount to change</param>
        /// <returns>the new balance</returns>
        public decimal IncreaseBalance(string playerId, decimal increaseBy) {

            if (increaseBy != 0) {
                Bank.PerformOperation(playerId, Bank.BANK_PLAYER_NAME_AND_ID, out decimal balance, out decimal _,
                    (BankAccount playerAcc, BankAccount bankAcc, out Transaction trans) => {
                        playerAcc.Balance += increaseBy;
                        bankAcc.Balance -= increaseBy;
                        trans = null;
                        return new Tuple<bool, decimal, decimal>(true, playerAcc.Balance, bankAcc.Balance);
                    });
                Uconomy.Instance.BalanceUpdated(playerId, increaseBy);
                return balance;
            }

            return GetBalance(playerId);
        }

    }

}