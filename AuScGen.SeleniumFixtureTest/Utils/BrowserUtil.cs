using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;

namespace AuScGen.SeleniumFixtureTest.Utils
{
    public class BrowserUtil
    {
        public static BrowserType GetBrowserTypeFromTestSettings
        {
            get
            {
                string browserType = Config.TestSettings.Default.Browser;

                switch(browserType)
                {
                    case "InternetExplorer":
                        return BrowserType.IE;

                    case "GoogleChrome":
                        return BrowserType.Chrome;

                    case "Firefox":
                        return BrowserType.Firefox;

                    default:
                        return BrowserType.IE;
                }
            }
        }

    }
}
