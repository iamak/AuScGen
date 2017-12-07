// ***********************************************************************
// <copyright file="SeleniumWebLink.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebLink class</summary>
// ***********************************************************************
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
	/// <summary>
	/// Class SeleniumWebLink
	/// </summary>
    public class SeleniumWebLink : SeleniumWebControls, ILink
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="SeleniumWebLink"/> class.
		/// </summary>
		/// <param name="webElement">a web element.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <param name="access">The access.</param>
        internal SeleniumWebLink(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType,access)
        { }

    }
}
