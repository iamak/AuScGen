// ***********************************************************************
// <copyright file="ICalender.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ICalender Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
    /// <summary>
    /// ICalender
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface ICalender : IControl
    {
        /// <summary>
        /// Gets the calender header.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of the locator.</param>
        /// <returns>SeleniumWebControls.</returns>
        SeleniumWebControls GetCalenderHeader(string locator, LocatorType locatorType);
        /// <summary>
        /// Gets the month and year.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of the locator.</param>
        /// <param name="headerLocator">The header locator.</param>
        /// <param name="headerLocatorType">Type of the header locator.</param>
        /// <returns>SeleniumWebControls.</returns>
        SeleniumWebControls GetMonthAndYear(string locator, LocatorType locatorType, string headerLocator, LocatorType headerLocatorType);
    }
}
