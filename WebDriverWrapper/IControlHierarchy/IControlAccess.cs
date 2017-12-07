// ***********************************************************************
// <copyright file="IControlAccess.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IControlAccess Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDriverWrapper
{
    /// <summary>
    /// IControlAccess Interface.
    /// </summary>
    public interface IControlAccess
    {
        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <returns>Get Control</returns>
        IControl GetControl();

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of a locator.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <returns> list of IControl</returns>
        IList<IControl> GetChildren(string locator, LocatorType locatorType, ControlType controlType);
    }
}
