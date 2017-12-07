// ***********************************************************************
// <copyright file="SeleniumWebLabel.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebLabel class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// Class SeleniumWebLabel.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.ILabel" />
    public class SeleniumWebLabel : SeleniumWebControls, ILabel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebLabel"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebLabel(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType,access)
        { }
    }
}
