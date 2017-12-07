// ***********************************************************************
// <copyright file="TestData.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>TestData class</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Data.OleDb;
using DataAccess.Entities;

namespace EDMC.DataAccess
{
    /// <summary>
    /// TestData
    /// </summary>
    public class TestData : BaseTestData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestData"/> class.
        /// </summary>
        /// <param name="dataFile">The data file.</param>
        public TestData(string dataFile)
            : base(dataFile)
        { }

        /// <summary>
        /// Gets the test data.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <returns></returns>
        public IList<GenericTestData> GetTestData(string testName)
        {
            DataAccess.QueryString = SQL.Resource.GetTestData;
            return DataAccess.GetData<GenericTestData, OleDbCommand>(command =>
            {
                command.Parameters.AddWithValue("TestName", testName);
            });
        }
    }
}
