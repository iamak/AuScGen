using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.FunctionalTest.Utils
{
    public static class TestExecution
    {
        public static ArtOfTest.WebAii.Core.BrowserType GetTelerikBrowser 
        { 
            get
            {
                ArtOfTest.WebAii.Core.BrowserType browserType = ArtOfTest.WebAii.Core.BrowserType.InternetExplorer;

                if(Config.TestSettings.Default.Browser.Equals("InternetExplorer"))
                {
                    browserType = ArtOfTest.WebAii.Core.BrowserType.InternetExplorer;
                }

                if (Config.TestSettings.Default.Browser.Equals("GoogleChrome"))
                {
                    browserType = ArtOfTest.WebAii.Core.BrowserType.Chrome;
                }

                if (Config.TestSettings.Default.Browser.Equals("Firefox"))
                {
                    browserType = ArtOfTest.WebAii.Core.BrowserType.FireFox;
                }

                return browserType;
            }
        }

        public static WebDriverWrapper.BrowserType GetWebDriverBrowser 
        { 
            get
            {
                WebDriverWrapper.BrowserType browserType = WebDriverWrapper.BrowserType.IE;

                if(Config.TestSettings.Default.Browser.Equals("InternetExplorer"))
                {
                    browserType = WebDriverWrapper.BrowserType.IE;
                }

                if (Config.TestSettings.Default.Browser.Equals("GoogleChrome"))
                {
                    browserType = WebDriverWrapper.BrowserType.Chrome;
                }

                if (Config.TestSettings.Default.Browser.Equals("Firefox"))
                {
                    browserType = WebDriverWrapper.BrowserType.Firefox;
                }

                return browserType;
            }

        }
    }
}
