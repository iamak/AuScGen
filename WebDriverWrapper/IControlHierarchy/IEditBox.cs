// ***********************************************************************
// <copyright file="IEditBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IEditBox Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
    /// <summary>
    /// IEditBox
    /// </summary>
    public interface IEditBox : IControl
    {
        /// <summary>
        /// Jses the send keys.
        /// </summary>
        /// <param name="text">a text.</param>
        void JSSendKeys(string text);
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}
