using System;
using System.Xml.Serialization;
using Rocket.API;

namespace fr34kyn01535.Uconomy
{
    [Serializable]
    public class UconomyConfiguration : IRocketPluginConfiguration
    {
        public string DatabaseAddress;
        public string DatabaseUsername;
        public string DatabasePassword;
        public string DatabaseName;
        public string DatabaseTableName;
        public int DatabasePort;

        public decimal InitialBalance;
        public string MoneyName;

        public void LoadDefaults()
        {
            InitialBalance = 30;
            MoneyName = "Credits";

            DatabaseAddress = "localhost";
            DatabaseUsername = "root";
            DatabasePassword = "";
            DatabaseName = "unturned";
            DatabaseTableName = "Unturned";
            DatabasePort = 3306;
        }
    }
}
