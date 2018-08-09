using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Steamworks;
using System;

// ReSharper disable InconsistentNaming
namespace fr34kyn01535.Uconomy
{
    public class Uconomy : RocketPlugin<UconomyConfiguration>
    {
        public DatabaseManager Database;
        public static Uconomy Instance;

        protected override void Load()
        {
            Instance = this;
            Database = new DatabaseManager();
        }

        public delegate void PlayerBalanceUpdate(UnturnedPlayer player, decimal amt);
        public event PlayerBalanceUpdate OnBalanceUpdate;
        public delegate void PlayerBalanceCheck(UnturnedPlayer player, decimal balance);
        public event PlayerBalanceCheck OnBalanceCheck;
        public delegate void PlayerPay(UnturnedPlayer sender, string receiver, decimal amt);
        public event PlayerPay OnPlayerPay; // TODO intercept command from AviEconomy

        public override TranslationList DefaultTranslations => new TranslationList(){
            {"command_balance_show","Your current balance is: {0} {1}"},
            {"command_pay_invalid","Invalid arguments"},
            {"command_pay_error_pay_self","You cant pay yourself"},
            {"command_pay_error_invalid_amount","Invalid amount"},
            {"command_pay_error_cant_afford","Your balance does not allow this payment"},
            {"command_pay_error_player_not_found","Failed to find player"},
            {"command_pay_private","You paid {0} {1} {2}"},
            {"command_pay_console","You received a payment of {0} {1} "},
            {"command_pay_other_private","You received a payment of {0} {1} from {2}"},
        };

        internal void BalanceUpdated(string SteamID, decimal amt)
        {
            if (OnBalanceUpdate == null) { return; }
            UnturnedPlayer player = UnturnedPlayer.FromCSteamID(new CSteamID(Convert.ToUInt64(SteamID)));
            OnBalanceUpdate(player, amt);
        }

        internal void OnBalanceChecked(string SteamID, decimal balance)
        {
            if (OnBalanceCheck == null) { return; }
            UnturnedPlayer player = UnturnedPlayer.FromCSteamID(new CSteamID(Convert.ToUInt64(SteamID)));
            OnBalanceCheck(player, balance);
        }
    }
}
