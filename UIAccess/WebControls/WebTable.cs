// ***********************************************************************
// <copyright file="WebTable.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebTable class</summary>
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
    /// WebTable
    /// </summary>
    public class WebTable : WebControl
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
        /// Initializes a new instance of the <see cref="WebTable"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebTable(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.WebTable)
        {
            this.browser = browser;
            this.locator = locator;
        }

        /// <summary>
        /// Gets the web tables.
        /// </summary>
        /// <value>
        /// The web tables.
        /// </value>
        private IWebTable WebTables
        {
            get
            {
                return (IWebTable)ControlObject;
            }
        }

        /// <summary>
        /// Gets the get rows.
        /// </summary>
        /// <value>
        /// The get rows.
        /// </value>
        public ReadOnlyCollection<WebRow> GetRows 
        { 
            get
            {
                return Utility.GetWebControlsFromIControlList(this.WebTables.GetAllRows().Cast<IControl>().ToList(), this.browser, this.locator, ControlType.WebRow).Cast<WebRow>().ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the row with value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public WebRow GetRowWithValue(string value)
        {
            foreach(WebRow row in this.GetRows)
            {
                if(row.Text.Equals(value))
                {
                    return row;
                }
            }
            return null;
        }
    }
}
