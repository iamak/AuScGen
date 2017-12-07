// ***********************************************************************
// <copyright file="WebCell.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebCell class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;

namespace UIAccess.WebControls
{
    /// <summary>
    /// Class WebCell.
    /// </summary>
    /// <seealso cref="UIAccess.WebControls.WebControl" />
    public class WebCell : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebCell"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebCell(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.WebCell)
        { }

    }
}
