// ***********************************************************************
// <copyright file="WebFrame.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebFrame class</summary>
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
    /// WebFrame
    /// </summary>
    public class WebFrame : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebFrame"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebFrame(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.Frame)
        { }

        /// <summary>
        /// Gets the frame.
        /// </summary>
        /// <value>
        /// The frame.
        /// </value>
        private IFrame Frame
        {
            get
            {
                return (IFrame)ControlObject;
            }
        }
    }
}
