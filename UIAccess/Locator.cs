// ***********************************************************************
// <copyright file="Locator.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Locator class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;

namespace UIAccess
{
    /// <summary>
    /// Locator
    /// </summary>
    public class Locator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Locator"/> class.
        /// </summary>
        public Locator()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Locator"/> class.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of the locator.</param>
        public Locator(string locator, LocatorType locatorType)
        {
            ControlLocator = locator;
            this.LocatorType = locatorType;
        }
        /// <summary>
        /// Gets or sets the control locator.
        /// </summary>
        /// <value>
        /// The control locator.
        /// </value>
        public string ControlLocator { get; set; }
        /// <summary>
        /// Gets or sets the type of the locator.
        /// </summary>
        /// <value>
        /// The type of the locator.
        /// </value>
        public LocatorType LocatorType { get; set; }
    }
}
