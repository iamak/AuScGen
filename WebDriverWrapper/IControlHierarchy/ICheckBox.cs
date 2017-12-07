// ***********************************************************************
// <copyright file="ICheckBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ICheckBox Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
    /// <summary>
    /// ICheckBox class.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface ICheckBox : IControl
    {
        /// <summary>
        /// Checks this instance.
        /// </summary>
        void Check();        
    }
}
