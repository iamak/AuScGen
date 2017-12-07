// ***********************************************************************
// <copyright file="WebEditBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebEditBox class</summary>
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
    /// WebEditBox
    /// </summary>
    public class WebEditBox : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebEditBox"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebEditBox(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.EditBox)
        { }

        /// <summary>
        /// Gets the edit box.
        /// </summary>
        /// <value>
        /// The edit box.
        /// </value>
        private IEditBox EditBox
        {
            get
            {
                return (IEditBox)ControlObject;
            }
        }

        /// <summary>
        /// Works only for Id, Name, ClassName and TagName , for any other locator type default Id is used
        /// </summary>
        /// <param name="text">The text.</param>
        public void JSSendKeys(string text)
        {
            this.EditBox.JSSendKeys(text);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.EditBox.Clear();
        }
    }
}
