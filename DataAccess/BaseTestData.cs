// ***********************************************************************
// <copyright file="BaseTestData.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>BaseTestData class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMC.DataAccess
{
    /// <summary>
    /// BaseTestData
    /// </summary>
    public class BaseTestData
    {
        /// <summary>
        /// The da
        /// </summary>
        private static DataAccess da;

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
        /// Initializes a new instance of the <see cref="BaseTestData"/> class.
        /// </summary>
        /// <param name="dataFileName">Name of the data file.</param>
        public BaseTestData(string dataFileName)
        {
            da = new DataAccess()
            {
                ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;" + "Data Source = " + dataFileName + ";" + "Extended Properties = 'Excel 8.0;HDR=Yes'",
                //ConectionString = "Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = " + dataFileName + ";" + "Extended Properties = 'Excel 8.0;HDR=Yes'",
                DataCategory = DataCategory.MSExcel
            };
        }
    }
}
