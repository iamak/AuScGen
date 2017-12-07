// ***********************************************************************
// <copyright file="ControlAccess.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ControlAccess class</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace WebDriverWrapper
{
	/// <summary>
	/// LocatorType
	/// </summary>
	public enum LocatorType
    {
        /// <summary>
        /// The identifier
        /// </summary>
        Id,
        /// <summary>
        /// The name
        /// </summary>
        Name,
        /// <summary>
        /// The partial link text
        /// </summary>
        PartialLinkText,
        /// <summary>
        /// The CSS
        /// </summary>
        Css,
        /// <summary>
        /// The xpath
        /// </summary>
        Xpath,
        /// <summary>
        /// The tag name
        /// </summary>
        TagName,
        /// <summary>
        /// The link text
        /// </summary>
        LinkText,
        /// <summary>
        /// The class name
        /// </summary>
        ClassName
    }

    /// <summary>
    /// ControlType
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// The button
        /// </summary>
        Button,

        /// <summary>
        /// The edit box
        /// </summary>
        EditBox,

        /// <summary>
        /// The custom
        /// </summary>
        Custom,

        /// <summary>
        /// The calender
        /// </summary>
        Calender,

        /// <summary>
        /// The ComboBox
        /// </summary>
        ComboBox,

        /// <summary>
        /// The CheckBox
        /// </summary>
        CheckBox,

        /// <summary>
        /// The dialog
        /// </summary>
        Dialog,

        /// <summary>
        /// The frame
        /// </summary>
        Frame,

        /// <summary>
        /// The image
        /// </summary>
        Image,

        /// <summary>
        /// The label
        /// </summary>
        Label,

        /// <summary>
        /// The link
        /// </summary>
        Link,

        /// <summary>
        /// The ListBox
        /// </summary>
        ListBox,

        /// <summary>
        /// The page
        /// </summary>
        Page,

        /// <summary>
        /// The RadioButton
        /// </summary>
        RadioButton,

        /// <summary>
        /// The span area
        /// </summary>
        SpanArea,

        /// <summary>
        /// The web table
        /// </summary>
        WebTable,

        /// <summary>
        /// The web row
        /// </summary>
        WebRow,

        /// <summary>
        /// The web cell
        /// </summary>
        WebCell
    }

    /// <summary>
    /// ControlAccess
    /// </summary>
    public class ControlAccess : IControlAccess
    {
        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public Browser Browser { get; set; }

        /// <summary>
        /// Gets or sets the locator.
        /// </summary>
        /// <value>
        /// The locator.
        /// </value>
        public string Locator { get; set; }

        /// <summary>
        /// Gets or sets the type of the locator.
        /// </summary>
        /// <value>
        /// The type of the locator.
        /// </value>
        public LocatorType LocatorType { get; set; }

        /// <summary>
        /// Gets or sets the type of the control.
        /// </summary>
        /// <value>
        /// The type of the control.
        /// </value>
        public ControlType ControlType { get; set; }

        /// <summary>
        /// Gets or sets the type of the browser.
        /// </summary>
        /// <value>
        /// The type of the browser.
        /// </value>
        public BrowserType BrowserType { get; set; }

        /// <summary>
        /// a web element
        /// </summary>
        private IWebElement webElement;

        /// <summary>
        /// a web elements
        /// </summary>
        private ReadOnlyCollection<IWebElement> webElements;

        /// <summary>
        /// a web driver
        /// </summary>
        private IWebDriver webDriver;

        /// <summary>
        /// a locator
        /// </summary>
        private string locator;

        /// <summary>
        /// a locator type
        /// </summary>
        private LocatorType locatorType;

        /// <summary>
        /// a control type
        /// </summary>
        private ControlType controlType;

        /// <summary>
        /// a browser type
        /// </summary>
        private BrowserType browserType;

        /// <summary>
        /// Intializes the control access.
        /// </summary>
        public void IntializeControlAccess()
        {
            browserType = Browser.BrowserType;
            webDriver = Browser.BrowserHandle;
            locatorType = LocatorType;
            locator = Locator;
            controlType = ControlType;
        }

        /// <summary>
        /// Initializes the web element.
        /// </summary>
        internal void InitializeWebElement()
        {
            if (LocatorType == LocatorType.Id)
            {
                webElement = webDriver.FindElement(By.Id(locator));
            }

            if (LocatorType == LocatorType.Name)
            {
                webElement = webDriver.FindElement(By.Name(locator));
            }

            if (LocatorType == LocatorType.Css)
            {
                webElement = webDriver.FindElement(By.CssSelector(locator));
            }

            if (LocatorType == LocatorType.TagName)
            {
                webElement = webDriver.FindElement(By.TagName(locator));
            }

            if (LocatorType == LocatorType.Xpath)
            {
                webElement = webDriver.FindElement(By.XPath(locator));
            }

            if (LocatorType == LocatorType.LinkText)
            {
                webElement = webDriver.FindElement(By.LinkText(locator));
            }

            if (LocatorType == LocatorType.ClassName)
            {
                webElement = webDriver.FindElement(By.ClassName(locator));
            }
        }

        /// <summary>
        /// Initializes the web elements.
        /// </summary>
        private void InitializeWebElements()
        {
            if (LocatorType == LocatorType.Id)
            {
                webElements = webDriver.FindElements(By.Id(locator));
            }

            if (LocatorType == LocatorType.Name)
            {
                webElements = webDriver.FindElements(By.Name(locator));
            }

            if (LocatorType == LocatorType.Css)
            {
                webElements = webDriver.FindElements(By.CssSelector(locator));
            }

            if (LocatorType == LocatorType.TagName)
            {
                webElements = webDriver.FindElements(By.TagName(locator));
            }

            if (LocatorType == LocatorType.Xpath)
            {
                webElements = webDriver.FindElements(By.XPath(locator));
            }

            if (LocatorType == LocatorType.LinkText)
            {
                webElements = webDriver.FindElements(By.LinkText(locator));
            }

            if (LocatorType == LocatorType.ClassName)
            {
                webElements = webDriver.FindElements(By.ClassName(locator));
            }
        }

        /// <summary>
        /// Determines whether [is element present].
        /// </summary>
        /// <returns></returns>
        public bool IsElementPresent()
        {
            try
            {
                InitializeWebElement();
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public Actions Action
        {
            get
            {
                return new Actions(webDriver, webElement);
            }
        }

        /// <summary>
        /// Clicks at.
        /// </summary>
        public void ClickAt()
        {
            Browser.BackedSelenium.ClickAt(locator, "0,0");
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <returns>
        /// Get Control
        /// </returns>
        public IControl GetControl()
        {
            InitializeWebElement();
            return Utility.GetControlFromWebElement(webElement, controlType, this);
        }
		
        /// <summary>
        /// Gets or sets the get controls.
        /// </summary>
        /// <value>
        /// The get controls.
        /// </value>
		public ReadOnlyCollection<IControl> GetControls
		{
			get
			{
				InitializeWebElements();
				return Utility.GetControlsFromWebElements(webElements, controlType, this).AsReadOnly();
			}
		}

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of a locator.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <returns>
        /// list of IControl
        /// </returns>
        public IList<IControl> GetChildren(string locator, LocatorType locatorType, ControlType controlType)
        {
            return Utility.GetChildren(locator, locatorType, controlType, webElement, this);
        }
    }
}
