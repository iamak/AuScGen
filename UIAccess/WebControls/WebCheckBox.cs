// ***********************************************************************
// <copyright file="WebCheckBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebCheckBox class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;
using WebDriverWrapper.IControlHierarchy;

namespace UIAccess.WebControls
{
    /// <summary>
    /// WebCheckBox
    /// </summary>
    public class WebCheckBox : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebCheckBox"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebCheckBox(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.CheckBox)
        { }

        /// <summary>
        /// Gets the CheckBox.
        /// </summary>
        /// <value>
        /// The CheckBox.
        /// </value>
        private ICheckBox CheckBox
        {
            get
            {
                return (ICheckBox)ControlObject;
            }
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        public void Check()
        {
            this.CheckBox.Check();
        }
                
    }
}
