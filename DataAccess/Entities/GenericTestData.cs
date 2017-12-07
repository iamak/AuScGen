// ***********************************************************************
// <copyright file="GenericTestData.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>GenericTestData class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    /// <summary>
    ///     Class GenericTestData.
    /// </summary>
    public class GenericTestData : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestData"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The data.</param>
        public GenericTestData(string name, string data)
        {
            this.Name = name;
            this.Data = data;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }
    }
}
