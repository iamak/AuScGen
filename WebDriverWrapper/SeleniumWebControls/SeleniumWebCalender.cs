// ***********************************************************************
// <copyright file="SeleniumWebCalender.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebCalender class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// Selenium Web Calender class.
    /// </summary>
    public class SeleniumWebCalender : SeleniumWebControls, ICalender
    {
        /// <summary>
        /// The control access
        /// </summary>
        private ControlAccess controlAccess;
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebCalender"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebCalender(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        {
            this.controlAccess = access;
        }

        /// <summary>
        /// Gets the calender header.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of the locator.</param>
        /// <returns></returns>
        public SeleniumWebControls GetCalenderHeader(string locator, LocatorType locatorType)
        {
            switch (locatorType)
            {
                case LocatorType.ClassName:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.ClassName(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.Css:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.CssSelector(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.Id:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.Id(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.LinkText:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.LinkText(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.Name:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.Name(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.PartialLinkText:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.PartialLinkText(locator)), ControlType.Custom, this.controlAccess);

                case LocatorType.Xpath:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.XPath(locator)), ControlType.Custom, this.controlAccess);

                default:
                    return (SeleniumWebControls)Utility.GetControlFromWebElement(this.WebElement.FindElement(By.TagName(locator)), ControlType.Custom, this.controlAccess);
            }
        }

        /// <summary>
        /// Gets the month and year.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of the locator.</param>
        /// <param name="headerLocator">The header locator.</param>
        /// <param name="headerLocatorType">Type of the header locator.</param>
        /// <returns></returns>
        public SeleniumWebControls GetMonthAndYear(string locator, LocatorType locatorType, string headerLocator, LocatorType headerLocatorType)
        {

            return (SeleniumWebControls)Utility.GetControlFromWebElement(this.GetCalenderHeader(headerLocator, headerLocatorType).WebElement.FindElement(By.ClassName(locator)), ControlType.Custom, this.controlAccess);
        }
    }
}
