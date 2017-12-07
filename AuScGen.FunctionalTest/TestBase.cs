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
using UIAccess;

namespace AuScGen.FunctionalTest
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

        public static int Timeout 
        {
            get
            {
                return Pages.Config.PageClassSettings.Default.MaxTimeoutValue;
            }            
        }

        private TelerikPlugin.TelerikPlugin telerik;
        public TelerikPlugin.TelerikFramework Telerik
        {
            get
            {
                if (null == telerik)
                {
                    telerik = CreatePlugin<TelerikPlugin.TelerikPlugin>();                    
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
                    aWebDriver.Browser = new WebDriverWrapper.Browser(WebDriverWrapper.BrowserType.Chrome);
                }
                return aWebDriver;
            }
        }

        private TestExecutionUtil.TestExecute runner;
        public TestExecutionUtil.TestExecute Runner 
        { 
            get
            {
                if(null == runner)
                {
                    runner = new TestExecutionUtil.TestExecute();
                    runner.Print = new Utils.ScreenShot(Telerik,Directory.GetCurrentDirectory() + @"\Reports");
                }
                return runner;
            }
        }

        private static CommonUtilityPlugin.ExcelReader myExcel;
        public static CommonUtilityPlugin.ExcelReader Excel
        {
            get
            {
                if (null == myExcel)
                {
                    myExcel = CreatePlugin<CommonUtilityPlugin.ExcelReader>();
                }
                return myExcel;
            }

        }

        private CommonUtilityPlugin.MouseKeyBoardSimulator keyBoardSimulator;
        public CommonUtilityPlugin.MouseKeyBoardSimulator KeyBoardSimulator
        {
            get
            {
                if (null == keyBoardSimulator)
                {
                    keyBoardSimulator = CreatePlugin<CommonUtilityPlugin.MouseKeyBoardSimulator>();
                }
                return keyBoardSimulator;
            }

        }

        private CommonUtilityPlugin.DataAccess dataAccess;
        public CommonUtilityPlugin.DataAccess DBValidation
        {
            get
            {
                dataAccess = CreatePlugin<CommonUtilityPlugin.DataAccess>();
                dataAccess.ConnectionString = Config.TestSettings.Default.DBConnection;
                dataAccess.DataCategory = CommonUtilityPlugin.DataCategory.SQLDB;                
                return dataAccess;
            }

        }

        public CommonUtilityPlugin.HttpRestClient ServiceAccess
        {
            get
            {

                return CreatePlugin<CommonUtilityPlugin.HttpRestClient>();
            }

        }

        //private Pages.DialogManager dialogHandler;
        //public Pages.DialogManager DialogHandler 
        //{ 
        //    get
        //    {
        //        if(null == dialogHandler)
        //        {
        //            dialogHandler = new Pages.DialogManager(Telerik);
        //        }
        //        return dialogHandler;
        //    }
        //}

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

        protected string TCDAppUrl
        {
            get
            {
                return Config.TestSettings.Default.Url;
            }
        }

        //protected bool IsLoggedIn 
        //{ 
        //    get
        //    {
        //        return Page.LoginPage.IsLoggedIn;
        //    }
        //}

        [TestFixtureSetUp]
        public virtual void TestFixtureSetupBase()
        {
            Console.WriteLine("Test Fixture Base");
            Telerik.Initialize(false, new TestContextWriteLine(Console.Out.WriteLine));
            Telerik.Manager.LaunchNewBrowser(BrowserType.InternetExplorer);
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
            if(Config.TestSettings.Default.UserViewMode)
            {
                Telerik.Manager.Settings.ExecutionDelay = 500;
                Telerik.Manager.Settings.AnnotateExecution = true; 
            }
        }
        private static T CreatePlugin<T>() where T : IPlugin
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
