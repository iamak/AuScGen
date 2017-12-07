// ***********************************************************************
// <copyright file="WebDialog.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebDialog class</summary>
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
    ///     Class WebDialog
    /// </summary>
    public class WebDialog : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDialog"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebDialog(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.Dialog)
        { }

        /// <summary>
        /// Gets the dialog.
        /// </summary>
        /// <value>
        /// The dialog.
        /// </value>
        private IDialog Dialog
        {
            get
            {
                return (IDialog)ControlObject;
            }
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        public void Test()
        {
            this.Dialog.GetTitle();
        }

        /// <summary>
        /// Accepts the dialog.
        /// </summary>
        public void AcceptDialog()
        {
            this.Dialog.AcceptDialog();
        }

        /// <summary>
        /// Cancels the dialog.
        /// </summary>
        public void CancelDialog()
        {
            this.Dialog.CancelDialog();
        }

		/// <summary>
		/// Gets the get dialog text.
		/// </summary>
		/// <value>
		/// The get dialog text.
		/// </value>
		public string GetDialogText
		{
			get
			{
				return this.Dialog.GetDialogText();
			}
		}

        /// <summary>
        /// Sends the text.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SendText(string value)
        {
            this.Dialog.SendText(value);
        }
    }
}