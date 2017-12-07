// ***********************************************************************
// <copyright file="Actions.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Actions class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WebDriverWrapper
{
    /// <summary>
    /// Actions
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        /// <value>
        /// The web driver.
        /// </value>
        internal IWebDriver WebDriver { get; set; }
        /// <summary>
        /// Gets or sets the web element.
        /// </summary>
        /// <value>
        /// The web element.
        /// </value>
        internal IWebElement WebElement { get; set; }
        /// <summary>
        /// Gets the selenium actions.
        /// </summary>
        /// <value>
        /// The selenium actions.
        /// </value>
        private OpenQA.Selenium.Interactions.Actions SeleniumActions
        {
            get
            {
                return new OpenQA.Selenium.Interactions.Actions(WebDriver);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actions"/> class.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="control">The control.</param>
        public Actions(Browser browser, SeleniumWebControls control)
        {
            WebDriver = browser.BrowserHandle;
            WebElement = control.WebElement;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actions"/> class.
        /// </summary>
        /// <param name="webDriver">The webdriver.</param>
        /// <param name="webElement">The web element.</param>
        public Actions(IWebDriver webDriver, IWebElement webElement)
        {
            WebDriver = webDriver;
            WebElement = webElement;
        }

        /// <summary>
        /// Moves to element.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void MoveToElement(IControl webElement)
        {
            //Thread.Sleep(10000);
            SeleniumActions.MoveToElement(((SeleniumWebControls)webElement).WebElement).Build().Perform();
            //Thread.Sleep(10000);
            //element.Click().Build().Perform();
        }

        /// <summary>
        /// Moves to element.
        /// </summary>
        /// <param name="offsetX">The off set x.</param>
        /// <param name="offsetY">The off set y.</param>
        public void MoveToElement(int offsetX, int offsetY)
        {
            SeleniumActions.MoveToElement(WebElement, offsetX, offsetY).Build().Perform();
        }

        /// <summary>
        /// Drags the drop.
        /// </summary>
        /// <param name="target">The target.</param>
        public void DragDrop(IControl target)
        {
            SeleniumActions.DragAndDrop(WebElement, ((SeleniumWebControls)target).WebElement).Build().Perform();
        }

        /// <summary>
        /// Drags the drop to offset.
        /// </summary>
        /// <param name="offsetX">The off set x.</param>
        /// <param name="offsetY">The off set y.</param>
        public void DragDropToOffset(int offsetX, int offsetY)
        {
            SeleniumActions.DragAndDropToOffset(WebElement, offsetX, offsetY).Build().Perform();
        }

        /// <summary>
        /// Natives the select.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void NativeSelect(IControl webElement)
        {
            SeleniumActions.MoveToElement(((SeleniumWebControls)webElement).WebElement).Click().Build().Perform();
        }

        /// <summary>
        /// Sends the keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void SendKeys(string keys)
        {
            SeleniumActions.SendKeys(keys);

        }

        /// <summary>
        /// Sends the keys.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="keys">The keys.</param>
        public void SendKeys(IControl webElement, string keys)
        {
            SeleniumActions.SendKeys(((SeleniumWebControls)webElement).WebElement, keys);
        }

        /// <summary>
        /// Clicks the and hold.
        /// </summary>
        public void ClickAndHold()
        {
            SeleniumActions.ClickAndHold(WebElement).Build().Perform();
        }

        /// <summary>
        /// Clicks the and hold.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void ClickAndHold(IControl webElement)
        {
            SeleniumActions.ClickAndHold(((SeleniumWebControls)webElement).WebElement).Build().Perform();
        }

        /// <summary>
        /// Moves the by offset.
        /// </summary>
        /// <param name="offsetX">The x offset.</param>
        /// <param name="offsetY">The y offset.</param>
        public void MoveByOffset(int offsetX, int offsetY)
        {
            SeleniumActions.MoveByOffset(offsetX, offsetY).Build().Perform();
        }
    }
}
