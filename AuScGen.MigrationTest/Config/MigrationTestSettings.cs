using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecolab.MigrationTest.Config
{
    class MigrationTestSettings : BaseSetings
    {
        private static MigrationTestSettings defaultInstance = new MigrationTestSettings();
        private static string settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
        private static string settingsFile;
        public static MigrationTestSettings Default
        {
            get
            {
                settingsFile = Path.Combine(settingsFilePath, "MigrationTestSetting.xml");
                return defaultInstance;
            }
        }

        string conduitDBConnection = "Data Source=HYD-ECOLABDB\\SQLExpress;Initial Catalog=ConduitMigration;User ID=tcddev;Password=Agstcd@1";
        public string ConduitDBConnection
        {
            get
            {
                string value = GetValue(settingsFile, "ConduitDBConnection");
                if (null != value)
                {
                    conduitDBConnection = value;
                }
                return conduitDBConnection;
            }
        }

        string chemWatchDBConnection = "Provider=VFPOLEDB.1;Data Source=\\\\hyd-muqtharshai\\Chino;";
        public string ChemWatchDBConnection
        {
            get
            {
                string value = GetValue(settingsFile, "ChemWatchDBConnection");
                if (null != value)
                {
                    chemWatchDBConnection = value;
                }
                return chemWatchDBConnection;
            }
        }

      

    }
}
