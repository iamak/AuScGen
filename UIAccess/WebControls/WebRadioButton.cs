// ***********************************************************************
// <copyright file="WebRadioButton.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebRadioButton class</summary>
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
    /// WebRadioButton
    /// </summary>
    public class WebRadioButton : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebRadioButton"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebRadioButton(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.RadioButton)
        { }

        /// <summary>
        /// Gets the RadioButton.
        /// </summary>
        /// <value>
        /// The RadioButton.
        /// </value>
        private IRadioButton RadioButton
        {
            get
            {
                return (IRadioButton)ControlObject;
            }
        }
    }
}
