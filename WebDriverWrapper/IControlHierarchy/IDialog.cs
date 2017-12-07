// ***********************************************************************
// <copyright file="IDialog.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IDialog Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
    /// <summary>
    /// IDialog Interface.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface IDialog : IControl
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <returns></returns>
        string GetTitle();
        /// <summary>
        /// Accepts the dialog.
        /// </summary>
        void AcceptDialog();
        /// <summary>
        /// Cancels the dialog.
        /// </summary>
        void CancelDialog();
        /// <summary>
        /// Gets the dialog text.
        /// </summary>
        /// <returns></returns>
        string GetDialogText();
        /// <summary>
        /// Sends the text.
        /// </summary>
        /// <param name="text">The text.</param>
        void SendText(string text);
    }
}