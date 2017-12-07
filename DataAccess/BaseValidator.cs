// ***********************************************************************
// <copyright file="BaseValidator.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>BaseValidator class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMC.DataAccess
{
    /// <summary>
    /// BaseValidator
    /// </summary>
    public class BaseValidator
    {
        /// <summary>
        /// The da
        /// </summary>
        private static DataAccess da;

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Gets the data access.
        /// </summary>
        /// <value>
        /// The data access.
        /// </value>
        protected static DataAccess DataAccess 
        { 
            get
            {
                return da;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValidator"/> class.
        /// </summary>
        public BaseValidator()
        { }

        /// <summary>
        /// Initializes the validator.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public static void InitializeValidator(string connectionString)
        {
            ConnectionString = connectionString;

            da = new DataAccess()
            {
                ConnectionString = connectionString,
                //ConectionString = "Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = " + dataFileName + ";" + "Extended Properties = 'Excel 8.0;HDR=Yes'",
                DataCategory = DataCategory.SQLDB
            };
        }
        
    }
}
