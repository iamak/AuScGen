// ***********************************************************************
// <copyright file="SeleniumWebSpanArea.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebSpanArea class</summary>
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
    /// Class SeleniumWebSpanArea.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.SeleniumWebControls" />
    /// <seealso cref="WebDriverWrapper.IControlHierarchy.ISpanArea" />
    public class SeleniumWebSpanArea : SeleniumWebControls, ISpanArea
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebSpanArea"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebSpanArea(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType,access)
        { }

    }
}
