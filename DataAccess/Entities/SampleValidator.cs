// ***********************************************************************
// <copyright file="SampleValidator.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SampleValidator class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    /// <summary>
    ///     Class SampleValidator
    /// </summary>
    public class SampleValidator : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleValidator"/> class.
        /// </summary>
        /// <param name="column1">The column1.</param>
        /// <param name="column2">The column2.</param>
        /// <param name="column3">The column3.</param>
        public SampleValidator(string column1, string column2, string column3)
        {
            this.Column1 = column1;
            this.Column2 = column2;
            this.Column3 = column3;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleValidator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="salary">The salary.</param>
        public SampleValidator(string name, int salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        /// <summary>
        /// Gets or sets the column1.
        /// </summary>
        /// <value>
        /// The column1.
        /// </value>
        public string Column1 { get; set; }

        /// <summary>
        /// Gets or sets the column2.
        /// </summary>
        /// <value>
        /// The column2.
        /// </value>
        public string Column2 { get; set; }

        /// <summary>
        /// Gets or sets the column3.
        /// </summary>
        /// <value>
        /// The column3.
        /// </value>
        public string Column3 { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// The salary.
        /// </value>
        public int Salary { get; set; }
    }
}
