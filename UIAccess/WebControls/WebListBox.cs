// ***********************************************************************
// <copyright file="WebListBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebListBox class</summary>
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
    /// WebListBox
    /// </summary>
    public class WebListBox : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebListBox"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebListBox(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.ListBox)
        { }

        /// <summary>
        /// Gets the ListBox.
        /// </summary>
        /// <value>
        /// The ListBox.
        /// </value>
        private IListBox ListBox
        {
            get
            {
                return (IListBox)ControlObject;
            }
        }
    }
}
