using Peygir.Data.Properties;
using System;

namespace Peygir.Data
{
    public partial class PeygirDatabaseDataSet
    {
        public static string DefaultDatabaseFileName
        {
            get { return Settings.Default.DefaultDatabaseFileName; }
        }

        public static void ChangeDatabasePath(string databasePath)
        {
            if (databasePath == null)
            {
                throw new ArgumentNullException("databasePath");
            }

            string newConnectionString = Settings.Default.DefaultConnectionString;
            newConnectionString = newConnectionString.Replace(@"{DatabasePath}", databasePath);

            Settings.Default["PeygirDatabaseConnectionString"] = newConnectionString;

            return;
        }
    }
}
