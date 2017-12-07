// ***********************************************************************
// <copyright file="SeleniumWebImage.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebImage class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// Class SeleniumWebImage.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.IImage" />
    public class SeleniumWebImage : SeleniumWebControls, IImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebImage"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebImage(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }
    }
}
