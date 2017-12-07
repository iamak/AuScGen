// ***********************************************************************
// <copyright file="TreeViewModel.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TreeViewModel class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuScGen.Web.Models
{
    /// <summary>
    /// Tree View Model class.
    /// </summary>
    public class TreeViewModel
    {
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the class methods.
        /// </summary>
        /// <value>
        /// The class methods.
        /// </value>
        public List<MethodModel> ClassMethods { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked { get; set; }
    }
}