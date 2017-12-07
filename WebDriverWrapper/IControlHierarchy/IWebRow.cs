// ***********************************************************************
// <copyright file="IWebRow.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IWebRow Interface</summary>
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
    /// Interface IWebRow
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface IWebRow : IControl
    {
        /// <summary>
        /// Gets all cells.
        /// </summary>
        /// <returns>ReadOnlyCollection&lt;SeleniumWebCell&gt;.</returns>
        ReadOnlyCollection<SeleniumWebCell> GetAllCells();
    }
}
