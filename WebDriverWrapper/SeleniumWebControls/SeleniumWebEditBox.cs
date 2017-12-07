// ***********************************************************************
// <copyright file="SeleniumWebEditBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebEditBox class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// SeleniumWebEditBox
    /// </summary>
    public class SeleniumWebEditBox : SeleniumWebControls, IEditBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebEditBox"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebEditBox(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }

        /// <summary>
        /// Works only for Id, Name, ClassName and TagName , for any other locator type default Id is used
        /// </summary>
        /// <param name="text">a text.</param>
        public void JSSendKeys(string text)
        {
            switch (ControlAccess.LocatorType)
            {
                case LocatorType.Id:
                    this.ExecuteJavaScript(this.ControlAccess.Browser, string.Format("document.getElementById('{0}').value='{1}'", this.ControlAccess.Locator, text));
                    break;

                case LocatorType.Name:
                    this.ExecuteJavaScript(this.ControlAccess.Browser, string.Format("document.getElementsByName('{0}')[0].value='{0}'", this.ControlAccess.Locator, text));
                    break;

                case LocatorType.ClassName:
                    this.ExecuteJavaScript(this.ControlAccess.Browser, string.Format("document.getElementsByClassName('{0}')[0].value='{0}'", this.ControlAccess.Locator, text));
                    break;

                case LocatorType.PartialLinkText:
                    break;
                case LocatorType.TagName:
                    this.ExecuteJavaScript(this.ControlAccess.Browser, string.Format("document.getElementsByTagName('{0}')[0].value='{0}'", this.ControlAccess.Locator, text));
                    break;

                default:
                    this.ExecuteJavaScript(this.ControlAccess.Browser, string.Format("document.getElementById('{0}').value='{0}'", this.ControlAccess.Locator, text));
                    break;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.WebElement.Clear();
            //aWebElement.SendKeys(Keys.Escape);
        }
    }
}
