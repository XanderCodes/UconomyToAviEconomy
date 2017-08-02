using com.aviadmini.rocketmod.AviEconomy;

// ReSharper disable InconsistentNaming
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
        /// <param name="playerId">RocketPlayer ID of the account owner</param>
        /// <returns></returns>
        public decimal GetBalance(string playerId)
        {
            decimal balance = Bank.GetBalance(playerId);
            Uconomy.Instance.OnBalanceChecked(playerId, balance);
            return balance;
        }

        /// <summary>
        /// Increasing balance to increaseBy (can be negative)
        /// </summary>
        /// <param name="playerId">RocketPlayer ID of the account owner</param>
        /// <param name="increaseBy">amount to change</param>
        /// <returns>the new balance</returns>
        public decimal IncreaseBalance(string playerId, decimal increaseBy)
        {
            Bank.PerformPayout(playerId, increaseBy, false);
            Uconomy.Instance.BalanceUpdated(playerId, increaseBy);
            return GetBalance(playerId);
        }
    }
}
