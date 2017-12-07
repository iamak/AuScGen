// ***********************************************************************
// <copyright file="SeleniumWebDialog.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebDialog class</summary>
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
    /// Class Selenium WebDialog
    /// </summary>
    public class SeleniumWebDialog : SeleniumWebControls, IDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebDialog" /> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebDialog(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Accepts the dialog.
        /// </summary>
        public void AcceptDialog()
        {
            ControlAccess.Action.WebDriver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Cancels the dialog.
        /// </summary>
        public void CancelDialog()
        {
            ControlAccess.Action.WebDriver.SwitchTo().Alert().Dismiss();
        }

        /// <summary>
        /// Gets the dialog text.
        /// </summary>
        /// <returns></returns>
        public string GetDialogText()
        {
            return ControlAccess.Action.WebDriver.SwitchTo().Alert().Text;
        }

        /// <summary>
        /// Sends the text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SendText(string text)
        {
            ControlAccess.Action.WebDriver.SwitchTo().Alert().SendKeys(text);
        }
    }
}
