// ***********************************************************************
// <copyright file="WebRow.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebRow class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;
using WebDriverWrapper.IControlHierarchy;

namespace UIAccess.WebControls
{
    /// <summary>
    /// WebRow
    /// </summary>
    public class WebRow : WebControl
    {
        /// <summary>
        /// The browser
        /// </summary>
        private Browser browser;
        /// <summary>
        /// The locator
        /// </summary>
        private Locator locator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebRow"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebRow(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.WebRow)
        {
            this.browser = browser;
            this.locator = locator;
        }
        /// <summary>
        /// Gets the web rows.
        /// </summary>
        /// <value>
        /// The web rows.
        /// </value>
        private IWebRow WebRows
        {
            get
            {
                return (IWebRow)ControlObject;
            }
        }
        /// <summary>
        /// Gets the get cells.
        /// </summary>
        /// <value>
        /// The get cells.
        /// </value>
        public ReadOnlyCollection<WebCell> GetCells
        {
            get
            {
                return Utility.GetWebControlsFromIControlList(this.WebRows.GetAllCells().Cast<IControl>().ToList(), this.browser, this.locator, ControlType.WebCell).Cast<WebCell>().ToList().AsReadOnly();
            }
        }
    }
}
