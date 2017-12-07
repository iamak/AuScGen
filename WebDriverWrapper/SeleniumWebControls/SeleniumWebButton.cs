// ***********************************************************************
// <copyright file="SeleniumWebButton.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebButton class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// Class SeleniumWebButton.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.IButton" />
    public class SeleniumWebButton : SeleniumWebControls, IButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebButton"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebButton(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType,access)
        { }

    }
}
