// ***********************************************************************
// <copyright file="SeleniumWebRadioButton.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebRadioButton class</summary>
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
    /// SeleniumWebRadioButton
    /// </summary>
    public class SeleniumWebRadioButton : SeleniumWebControls, IRadioButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebRadioButton"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebRadioButton(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        public void Check()
        {
            if (!this.IsChecked)
            {
                this.WebElement.Click();
            }
        }
    }
}
