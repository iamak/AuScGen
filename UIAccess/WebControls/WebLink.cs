// ***********************************************************************
// <copyright file="WebLink.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebLink class</summary>
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
    /// WebLink
    /// </summary>
    public class WebLink : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebLink"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebLink(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.Link)
        { }

        /// <summary>
        /// Gets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        private ILink Link
        {
            get
            {
                return (ILink)ControlObject;
            }
        }
    }
}
