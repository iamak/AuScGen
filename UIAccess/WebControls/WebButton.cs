// ***********************************************************************
// <copyright file="WebButton.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebButton class</summary>
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
    /// WebButton
    /// </summary>
    public class WebButton : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebButton"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebButton(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.Button)
        { }

        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <value>
        /// The button.
        /// </value>
        private IButton Button
        {
            get
            {
                return (IButton)ControlObject;
            }
        }

        /// <summary>
        /// Clicks this instance.
        /// </summary>
        public new void Click()
        {
            this.Button.Click();
        }
    }
}
