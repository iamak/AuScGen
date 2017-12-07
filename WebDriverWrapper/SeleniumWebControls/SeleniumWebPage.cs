// ***********************************************************************
// <copyright file="SeleniumWebPage.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebPage class</summary>
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
    /// Class SeleniumWebPage.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.IPage" />
    public class SeleniumWebPage : SeleniumWebControls, IPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebPage"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebPage(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType,access)
        { }

    }
}
