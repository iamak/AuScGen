// ***********************************************************************
// <copyright file="IWebTable.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IWebTable Interface</summary>
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
    /// Interface IWebTable
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface IWebTable : IControl
    {
        /// <summary>
        /// Gets all rows.
        /// </summary>
        /// <returns>ReadOnlyCollection&lt;SeleniumWebRow&gt;.</returns>
        ReadOnlyCollection<SeleniumWebRow> GetAllRows();
    }
}