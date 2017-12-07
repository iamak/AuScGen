// ***********************************************************************
// <copyright file="SeleniumWebCell.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebCell class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// Class SeleniumWebCell.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.IWebCell" />
    public class SeleniumWebCell : SeleniumWebControls, IWebCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebCell"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebCell(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }
       
    }
}
