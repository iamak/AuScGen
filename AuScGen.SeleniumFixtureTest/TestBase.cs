using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Framework;
using NUnit.Framework;
using UIAccess;

namespace AuScGen.SeleniumFixtureTest
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
                return AuScGen.SeleniumTestPage.Config.PageClassSettings.Default.MaxTimeoutValue;
            }            
        }

        
        private static WebDriverPlugin aWebDriver;
        public WebDriverPlugin WebDriver
        {
            get
            {
                if (null == aWebDriver)
                {
                    aWebDriver = CreatePlugin<WebDriverPlugin>();
                    aWebDriver.Browser = new WebDriverWrapper.Browser(Utils.BrowserUtil.GetBrowserTypeFromTestSettings);
                }
                return aWebDriver;
            }

            private set
            {
                aWebDriver = value;
            }
        }

        private TestExecutionUtil.TestExecute runner;
        public TestExecutionUtil.TestExecute Runner 
        { 
            get
            {
                if (null == runner)
                {
                    runner = new TestExecutionUtil.TestExecute();
                    runner.Print = new Utils.ScreenShot(WebDriver, Directory.GetCurrentDirectory() + @"\Reports");
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

        protected string AppUrl
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
            WebDriver.Browser.Maximize();
            WebDriver.Browser.Navigate(new Uri(AppUrl)); 
        }

        [TestFixtureTearDown]
        public virtual void TestFixtureTeardownBase()
        {
           WebDriver.Browser.Quit();
           WebDriver = null;
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
