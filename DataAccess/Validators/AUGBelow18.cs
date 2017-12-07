// ***********************************************************************
// <copyright file="AUGBelow18.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>AUGBelow18 class</summary>
// ***********************************************************************
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Entities;

namespace EDMC.DataAccess.Validators
{
    /// <summary>
    /// AUGBelow18
    /// </summary>
    public class AUGBelow18 : BaseValidator
    {
        /// <summary>
        /// Samples the validation.
        /// </summary>
        /// <returns></returns>
        public static SampleValidator SampleValidation()
        {
            DataAccess.QueryString = SQL.Resource.SampleValidator;
            return DataAccess.GetData<SampleValidator>().FirstOrDefault();
        }

        /// <summary>
        /// Samples the salary validation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static SampleValidator SampleSalaryValidation(string name)
        {
            DataAccess.QueryString = SQL.Resource.SampleSalaryValidator;
            return DataAccess.GetData<SampleValidator, SqlCommand>(command =>
            {
                command.Parameters.AddWithValue("Name", name);
            }).FirstOrDefault();
        }
    }
}
