// ***********************************************************************
// <copyright file="Class2.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Class2 class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace WebDriver_Test
{
    /// <summary>
    /// Class2
    /// </summary>
    public class Class2
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        public static void Main()
        {
            SearchTheWeb(new FirefoxDriver(), new Uri("http://www.google.com"));
            SearchTheWeb(new InternetExplorerDriver(), new Uri("http://www.google.com"));
            SearchTheWeb(new ChromeDriver(), new Uri("http://www.google.com"));

            System.Console.Out.WriteLine("Press ENTER to exit");
            System.Console.In.ReadLine();
        }

        /// <summary>
        /// Searches the web.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="url">The URL.</param>
        public static void SearchTheWeb(IWebDriver driver, Uri url)
        {
            // And now use this to visit Google
            //driver.get("http://www.google.com");
            driver.Navigate().GoToUrl(url);
            // Find the text input element by its name
            IWebElement element = driver.FindElement(By.Name("q"));

            // Enter something to search for
            element.SendKeys("Cheese!");

            // Now submit the form. WebDriver will find
            // the form for us from the element
            element.Submit();

            // Check the title of the page
            System.Console.Out.WriteLine(
              "Page title is: " + driver.Title);

            driver.Quit();
        }
    }
}