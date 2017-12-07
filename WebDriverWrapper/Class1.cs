// ***********************************************************************
// <copyright file="Class1.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Class1 class</summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace WebDriver_Test
{
    /// <summary>
    /// Class1
    /// </summary>
    public class Class1
    {
        /// <summary>
        /// The driver
        /// </summary>
        private static IWebDriver driver;

        //private static ISelenium selenium;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public static void Setup()
        {
            IWebDriver driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), DesiredCapabilities.HtmlUnit());

            //driver = new FirefoxDriver();
            Selenium.WebDriverBackedSelenium s = new Selenium.WebDriverBackedSelenium(driver, new Uri(@"http://site4.way2sms.com/content/index.html"));
            s.Start();
            driver.Navigate().GoToUrl(new Uri("http://site4.way2sms.com/content/index.html"));

            WaitForLinkTextPresent("► click here to go to way2sms.com", 40);
            driver.FindElement(By.Id("username")).SendKeys("9916089888");
            driver.FindElement(By.Id("password")).SendKeys("suprwolf");
            driver.FindElement(By.Id("button")).Click();

            if (WaitIdPresent("quickclose1", 40000))
            {
                driver.FindElement(By.Id("quickclose1")).Click();
            }

            driver.FindElement(By.Id("quicksms")).Click();
            driver.SwitchTo().Frame("frame");
            s.WaitForPageToLoad("30000");
            driver.FindElement(By.Id("MobNo")).SendKeys("9916089888");
            WaitIdPresent("textArea", 400);
            driver.FindElement(By.Id("textArea")).SendKeys("test");
            driver.FindElement(By.Id("Send")).Submit();
            driver.FindElement(By.LinkText("Logout")).Clear();
        }

		/// <summary>
		/// Waits for link text present.
		/// </summary>
		/// <param name="locator">The locator.</param>
		/// <param name="maximumWaitSecond">The maximum wait second.</param>
		/// <returns></returns>
		public static bool WaitForLinkTextPresent(string locator, int maximumWaitSecond)
        {
            int theWaitTime;

            for (theWaitTime = 1; theWaitTime <= maximumWaitSecond; theWaitTime++)
            {
                try
                {
                    if (driver.FindElement(By.LinkText(locator)).Displayed) break;
                    Thread.Sleep(1000);
                }
                catch 
                {
					throw;
                }
            }

            if (theWaitTime > maximumWaitSecond)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

		/// <summary>
		/// Waits the identifier present.
		/// </summary>
		/// <param name="locator">The locator.</param>
		/// <param name="maximumWaitSecond">The maximum wait second.</param>
		/// <returns></returns>
		public static bool WaitIdPresent(string locator, int maximumWaitSecond)
        {
            int theWaitTime;

            for (theWaitTime = 1; theWaitTime <= maximumWaitSecond; theWaitTime++)
            {
                try
                {
                    if (driver.FindElement(By.Id(locator)).Displayed) break;
                    Thread.Sleep(1000);
                }
                catch 
                {
					throw;
                }
            }

            if (theWaitTime > maximumWaitSecond)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    //    public class ReusableRemoteWebDriver : RemoteWebDriver
    //    {
    //    	public ReusableRemoteWebDriver(Uri BaseUri, DesiredCapabilities BrowserCapabilites)
    //    		:base(BaseUri,BrowserCapabilites){}
    //    	
    ////    	ReusableRemoteWebDriver(String sessionId, URL remoteUrl) {
    ////		HttpCommandExecutor executor = new HttpCommandExecutor(remoteUrl);
    ////		setSessionId(sessionId);
    ////		setCommandExecutor(executor);
    ////}
    //    	
    //		public string Session { get; set; }
    //    	
    //    	public void StartSession(ICapabilities desiredCapabilities)
    //    	{
    //    		String sid = Session;
    //			if (sid != null) {
    //			  setSessionId(sid);
    //			  try {
    //			    getCurrentUrl();
    //			  } catch (WebDriverException e) {
    //			    // session is not valid
    //			    sid = null;
    //			  }
    //			}
    //			if (sid == null) {
    //			  base.StartSession(desiredCapabilities);
    //			  //saveSessionIdToSomeStorage(getSessionId().toString());
    //			}
    //
    //    	}
    //    	
    //    }
}