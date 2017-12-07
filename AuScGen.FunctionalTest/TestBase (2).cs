using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Framework;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Core;
using NUnit.Framework;
using ThatcherTech.CommonUtilityPlugin;


namespace ThatcherTech.FunctionalTest
{
    public class TestBase : IDisposable
    {
        private static ContainerAccess container = new ContainerAccess();
        private bool disposed = false;

        public TestBase()
        {
            Console.WriteLine("Test base Constructor");
            //TestFixture();             
        }

        protected static string TestDataPath
        {
            get
            {
                return string.Format(@"{0}\TestData\", Directory.GetCurrentDirectory());
            }
        }

        private ThatcherTech.TelerikPlugin.TelerikPlugin telerik;
        public ThatcherTech.TelerikPlugin.TelerikFramework Telerik
        {
            get
            {
                if (null == telerik)
                {
                    telerik = CreatePlugin<ThatcherTech.TelerikPlugin.TelerikPlugin>();                    
                }
                return telerik.TelerikFramework;
            }
        }

        private static WebDriverPlugin aWebDriver;
        public static WebDriverPlugin WebDriver
        {
            get
            {
                if (null == aWebDriver)
                {
                    aWebDriver = CreatePlugin<WebDriverPlugin>();
                    aWebDriver.Browser = new WebDriver_Test.Browser(WebDriver_Test.BrowserType.Chrome);
                }
                return aWebDriver;
            }
        }

        private static ExcelReader myExcel;
        public static ExcelReader Excel
        {
            get
            {
                if (null == myExcel)
                {
                    myExcel = CreatePlugin<ThatcherTech.CommonUtilityPlugin.ExcelReader>();
                }
                return myExcel;
            }

        }

        private MouseKeyBoardSimulator keyBoardSimulator;
        public MouseKeyBoardSimulator KeyBoardSimulator
        {
            get
            {
                if (null == keyBoardSimulator)
                {
                    keyBoardSimulator = CreatePlugin<ThatcherTech.CommonUtilityPlugin.MouseKeyBoardSimulator>();
                }
                return keyBoardSimulator;
            }

        }

        private DataAccess dataAccess;
        public DataAccess DBValidation
        {
            get
            {
                dataAccess = CreatePlugin<ThatcherTech.CommonUtilityPlugin.DataAccess>();
                dataAccess.ConectionString = Config.TestSettings.Default.DBConnection;
                dataAccess.DataCategory = ThatcherTech.CommonUtilityPlugin.DataCategory.SQLDB;                
                return dataAccess;
            }

        }

        public HttpRestClient ServiceAccess
        {
            get
            {

                return CreatePlugin<ThatcherTech.CommonUtilityPlugin.HttpRestClient>();
            }

        }        

        private Page myPage;
        protected Page Page
        {
            get
            {
                if (null == myPage)
                {
                    myPage = new Page(this);
                }
                return myPage;
            }
        }

        private Utils.Steps steps;
        protected Utils.Steps Steps
        {
            get
            {
                if (null == steps)
                {
                    steps = new Utils.Steps(Page,this);
                }
                return steps;
            }
        }

        protected string TCDAppUrl
        {
            get
            {
                return Config.TestSettings.Default.Url;
            }
        }

        [TestFixtureSetUp]
        public virtual void TestFixtureSetupBase()
        {
            Console.WriteLine("Test Fixture Base");
            Telerik.Initialize(false, new TestContextWriteLine(Console.Out.WriteLine));
            Telerik.Manager.LaunchNewBrowser(Utils.TestExecution.GetTelerikBrowser,true);
            Telerik.Manager.ActiveBrowser.ClearCache(BrowserCacheType.Cookies);
            Telerik.Manager.ActiveBrowser.ClearCache(BrowserCacheType.History);
            Telerik.Manager.ActiveBrowser.Window.SetActive();
            Telerik.Manager.ActiveBrowser.Window.Maximize();
            ConfigureTelerik();           
        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTeardownBase()
        {
            Telerik.Shutdown();
            Telerik.CleanUp();
        }

        private void ConfigureTelerik()
        {
            if (Config.TestSettings.Default.UserViewMode)
            {
                Telerik.Manager.Settings.ExecutionDelay = 500;
                Telerik.Manager.Settings.AnnotationMode = AnnotationMode.All;
                Telerik.Manager.Settings.AnnotateExecution = true; 
                Telerik.Manager.Settings.LogLocation = string.Format(@"{0}\AnnotationLogs",Directory.GetCurrentDirectory());
                Telerik.Manager.Settings.LogAnnotations = true;
            }
        }
        private static T CreatePlugin<T>() where T : IContainerPlugin
        {
            return container.GetPlugin<T>();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }
        public void Dispose()
        {
            //TestFixtureTearDown();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
