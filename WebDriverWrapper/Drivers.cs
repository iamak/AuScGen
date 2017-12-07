// ***********************************************************************
// <copyright file="Drivers.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Browser class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium;

namespace WebDriverWrapper
{
    /// <summary>
    ///     Browser Type
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// The firefox
        /// </summary>
        Firefox,

        /// <summary>
        /// The ie
        /// </summary>
        IE,

        /// <summary>
        /// The chrome
        /// </summary>
        Chrome,

        /// <summary>
        /// The HTML unit
        /// </summary>
        HTMLUnit

    }

    /// <summary>
    /// 
    /// </summary>
    public class Browser
    {
        #region Drivers

        /// <summary>
        /// Gets the firefox driver.
        /// </summary>
        /// <value>
        /// The firefox driver.
        /// </value>
        private IWebDriver FirefoxDriver
        {
            get
            {
                //string downloadDir = string.Format(@"{0}\DownloadedFiles",Directory.GetCurrentDirectory());

                if (!Directory.Exists(DownloadDirectory))
                {
                    Directory.CreateDirectory(DownloadDirectory);
                }

                FirefoxProfile firefoxProfile = new FirefoxProfile();
                firefoxProfile.SetPreference("browser.download.folderList", 2);
                firefoxProfile.SetPreference("browser.download.dir", DownloadDirectory);
                //firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false); 

                firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
                firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                firefoxProfile.SetPreference("pdfjs.disabled", true);
                firefoxProfile.SetPreference("plugin.scan.plid.all", false);

                if (null == BinaryPath)
                {
                    Console.WriteLine("null binary path");
                    return new FirefoxDriver(firefoxProfile);
                }
                else
                {
                    Console.WriteLine(BinaryPath);
                    return new FirefoxDriver(new FirefoxBinary(BinaryPath), firefoxProfile, TimeSpan.FromHours(2));
                }
            }
        }

        /// <summary>
        /// Gets the ie driver.
        /// </summary>
        /// <value>
        /// The ie driver.
        /// </value>
        private static IWebDriver IEDriver
        {
            get
            {
                return new InternetExplorerDriver(Config.DriverServerPath);
            }
        }

        /// <summary>
        /// Gets the chrome driver.
        /// </summary>
        /// <value>
        /// The chrome driver.
        /// </value>
        private static IWebDriver ChromeDriver
        {
            get
            {
                return new ChromeDriver(Config.DriverServerPath);
            }
        }

        /// <summary>
        /// Gets the HTML unit.
        /// </summary>
        /// <value>
        /// The HTML unit.
        /// </value>
        private IWebDriver HTMLUnit
        {
            get
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("java.exe", string.Format(@"-jar {0}\selenium-server-standalone-2.8.0.jar -port {1} -trustAllSSLCertificates", Config.NativeSeleniumDriver, Port));
                //startInfo.FileName = Config.NativeSeleniumDriver;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                startInfo.RedirectStandardOutput = true;

                //Start the process
                NativeSeleniumProcess = Process.Start(startInfo);

                //NativeSeleniumProcess = Process.Start(Config.NativeSeleniumDriver);

                while (!IsConnected()) ;
                return new RemoteWebDriver(new Uri(string.Format(@"http://127.0.0.1:{0}/wd/hub", Port)), DesiredCapabilities.HtmlUnit(), TimeSpan.FromHours(2));
            }
        }

        #endregion Drivers

        #region Private Properties

        /// <summary>
        /// Gets or sets the binary path.
        /// </summary>
        /// <value>
        /// The binary path.
        /// </value>
        public string BinaryPath { get; set; }

        #endregion Private Properties

        #region Internal Properties
        /// <summary>
        /// Gets the backed selenium.
        /// </summary>
        /// <value>
        /// The backed selenium.
        /// </value>
        internal WebDriverBackedSelenium BackedSelenium
        {
            get
            {
                WebDriverBackedSelenium driverBackedSelenium = new WebDriverBackedSelenium(BrowserHandle, Url);
                driverBackedSelenium.Start();
                return driverBackedSelenium;
            }
        }

        /// <summary>
        /// Gets or sets the browser handle.
        /// </summary>
        /// <value>
        /// The browser handle.
        /// </value>
        public IWebDriver BrowserHandle { get; set; }
        /// <summary>
        /// Gets or sets the type of the browser.
        /// </summary>
        /// <value>
        /// The type of the browser.
        /// </value>
        public BrowserType BrowserType { get; set; }

        #endregion Internal Properties

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        public Browser(BrowserType browserType)
        {
            GetBrowser(browserType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        /// <param name="binaryPath">The binary path.</param>
        public Browser(BrowserType browserType, string binaryPath)
        {
            BinaryPath = binaryPath;
            GetBrowser(browserType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        /// <param name="port">The port.</param>
        public Browser(BrowserType browserType, int port)
        {
            Port = port;
            GetBrowser(browserType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        /// <param name="binaryPath">The binary path.</param>
        /// <param name="downloadDirectory">The download directory.</param>
        public Browser(BrowserType browserType, string binaryPath, string downloadDirectory)
        {
            BinaryPath = binaryPath;
            DownloadDirectory = downloadDirectory;
            GetBrowser(browserType);
        }

        #endregion ctor

        #region Public Properties

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return BrowserHandle.Title;
            }
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public Uri Url
        {
            get
            {
                return new Uri(BrowserHandle.Url);
            }
        }

        /// <summary>
        /// Gets the current window handle.
        /// </summary>
        /// <value>
        /// The current window handle.
        /// </value>
        public string CurrentWindowHandle
        {
            get
            {
                return BrowserHandle.CurrentWindowHandle;
            }
        }

        /// <summary>
        /// Gets the window handles.
        /// </summary>
        /// <value>
        /// The window handles.
        /// </value>
        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return BrowserHandle.WindowHandles;
            }
        }

        /// <summary>
        /// Gets the native selenium process.
        /// </summary>
        /// <value>
        /// The native selenium process.
        /// </value>
        public Process NativeSeleniumProcess { get; private set; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; private set; }

        /// <summary>
        /// Gets or sets the download directory.
        /// </summary>
        /// <value>
        /// The download directory.
        /// </value>
        public string DownloadDirectory { get; set; }

        #endregion Public Properties

        #region Public  Methods

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        public void GetBrowser(BrowserType browserType)
        {
            //            ConfigureJava();
            //            
            //            Environment.SetEnvironmentVariable("JAVA_HOME",@"C:\Program Files\Java\jre7");
            //            Environment.SetEnvironmentVariable("JAVA",@"%JAVA_HOME%\bin\java.exe");
            //            Environment.SetEnvironmentVariable("JAVA_OPTS",@"%JAVA_TOOL_OPTONS% %_JAVA_OPTIONS%");
            //            Environment.SetEnvironmentVariable("JAVA_TOOL_OPTIONS",null);
            //            Environment.SetEnvironmentVariable("_JAVA_OPTIONS",null);

            BrowserType = browserType;

            if (browserType == BrowserType.Firefox)
            {
                BrowserHandle = FirefoxDriver;
            }

            if (browserType == BrowserType.IE)
            {
                BrowserHandle = IEDriver;
            }

            if (browserType == BrowserType.Chrome)
            {
                BrowserHandle = ChromeDriver;
            }

            if (browserType == BrowserType.HTMLUnit)
            {
                BrowserHandle = HTMLUnit;
            }
        }

        /// <summary>
        /// Switches the browser.
        /// </summary>
        /// <param name="browserTitle">The browser title.</param>
        public void SwitchBrowser(string browserTitle)
        {
            //string currentWindow = CurrentWindowHandle;

            IEnumerable<string> availableWindowHandles = BrowserHandle.WindowHandles;

            foreach (string availableWindowHandle in availableWindowHandles)
            {
                if (BrowserHandle.SwitchTo().Window(availableWindowHandle).Title.Contains(browserTitle))
                {
                    BrowserHandle.SwitchTo().Window(availableWindowHandle);
                }
            }
            //BrowserHandle = BrowserHandle.SwitchTo().Window(BrowserTitle);
        }

        /// <summary>
        /// Navigates the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Navigate(Uri url)
        {
            BrowserHandle.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Switches to frame.
        /// </summary>
        /// <param name="frameIndex">Index of the frame.</param>
        public void SwitchToFrame(int frameIndex)
        {
            BrowserHandle.SwitchTo().Frame(frameIndex);
        }

        //public void SwitchToFrame(IWebElement WebElement)
        //{
        //    BrowserHandle.SwitchTo().Frame(WebElement);
        //}

        /// <summary>
        /// Switches to frame.
        /// </summary>
        /// <param name="frameName">Name of the frame.</param>
        public void SwitchToFrame(string frameName)
        {
            BrowserHandle.SwitchTo().Frame(frameName);
        }

        //public void SwitchToWindow(string FrameName)
        //{
        //    BrowserHandle.SwitchTo().Frame(FrameName);
        //}

        /// <summary>
        /// Switches to default content.
        /// </summary>
        public void SwitchToDefaultContent()
        {
            BrowserHandle.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Maximizes this instance.
        /// </summary>
        public void Maximize()
        {
            BrowserHandle.Manage().Window.Maximize();
        }

        /// <summary>
        /// Deletes all cookies.
        /// </summary>
        public void DeleteAllCookies()
        {
            BrowserHandle.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Executes the java script.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public object ExecuteJavaScript(string javaScript)
        {
            return ((IJavaScriptExecutor)BrowserHandle).ExecuteScript(javaScript);
        }

        /// <summary>
        /// Takes the sreen shot.
        /// </summary>
        /// <returns></returns>
        public Bitmap TakeScreenshot()
        {
            var ms = new MemoryStream(((ITakesScreenshot)BrowserHandle).GetScreenshot().AsByteArray);
            return new Bitmap(ms);
        }

        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {
            BrowserHandle.Navigate().Back();
        }

        /// <summary>
        /// Goes the forward.
        /// </summary>
        public void GoForward()
        {
            BrowserHandle.Navigate().Forward();
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            BrowserHandle.Navigate().Refresh();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            BrowserHandle.Close();
        }

        /// <summary>
        /// Quits this instance.
        /// </summary>
        public void Quit()
        {
            BrowserHandle.Quit();
            BrowserHandle.Dispose();
        }

        /// <summary>
        /// Waits for page loaded.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public void WaitForPageLoaded(int timeout)
        {
            WebDriverBackedSelenium driverBackedSelenium = new WebDriverBackedSelenium(BrowserHandle, Url);

            driverBackedSelenium.Start();
            driverBackedSelenium.WaitForPageToLoad(timeout.ToString(CultureInfo.InvariantCulture));
        }

        #endregion Public  Methods

        /// <summary>
        /// Determines whether this instance is connected.
        /// </summary>
        /// <returns></returns>
        private bool IsConnected()
        {
            TcpClient client = new TcpClient();
			bool result = false;
            Console.WriteLine("Connecting.....");
            try
            {
                client.Connect("127.0.0.1", Port);
                Console.WriteLine("True");
                result = true;
            }
            catch
            {
                Console.WriteLine("False");				
                result = false;
				throw;
            }
			return result;
        }

        /// <summary>
        /// Wins the get handle.
        /// </summary>
        /// <param name="windowName">Name of the w.</param>
        /// <returns></returns>
        protected static internal IntPtr WindowGetHandle(string windowName)
        {
            IntPtr handleWindow = new IntPtr();

            foreach (Process processList in Process.GetProcesses())
            {
                if (processList.MainWindowTitle.Contains(windowName))
                {
                    handleWindow = processList.MainWindowHandle;
                }
            }

            return handleWindow;
        }

		/// <summary>
		/// Configures the java.
		/// </summary>
		protected static internal void ConfigureJava()
        {
            //Process.Start(Config.IEDriverServerPath + "java.bat");

            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";

            p.StartInfo.Arguments = @"/C " + Config.DriverServerPath + @"\java.bat";

            p.Start();

            p.WaitForExit();
        }
    }
}
