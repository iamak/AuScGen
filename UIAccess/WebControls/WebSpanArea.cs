// ***********************************************************************
// <copyright file="WebSpanArea.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebSpanArea class</summary>
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
    /// WebSpanArea
    /// </summary>
    public class WebSpanArea : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSpanArea"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebSpanArea(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.SpanArea)
        { }

        /// <summary>
        /// Gets the span area.
        /// </summary>
        /// <value>
        /// The span area.
        /// </value>
        private ISpanArea SpanArea
        {
            get
            {
                return (ISpanArea)ControlObject;
            }
        }
    }
}
