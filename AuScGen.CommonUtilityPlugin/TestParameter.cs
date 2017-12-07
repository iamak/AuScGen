// ***********************************************************************
// <copyright file="TestParameter.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>TestParameter class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMC.DataAccess;

namespace AuScGen.CommonUtilityPlugin
{
    /// <summary>
    ///     Class TestParameter
    /// </summary>
    public class TestParameter
    {
        /// <summary>
        /// Gets the parameter file.
        /// </summary>
        /// <value>
        /// The parameter file.
        /// </value>
        private static string ParamFile
        {
            get
            {
                return string.Format(@"{0}\{1}", ParameterFilePath, ParameterFileName);
            }
        }

        /// <summary>
        /// Gets or sets the parameter file path.
        /// </summary>
        /// <value>
        /// The parameter file path.
        /// </value>
        public static string ParameterFilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the parameter file.
        /// </summary>
        /// <value>
        /// The name of the parameter file.
        /// </value>
        public static string ParameterFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        public static string TestName { get; set; }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Parameter</returns>
        public static string GetParameter(string key)
        {
            StackTrace trace = new StackTrace();
            var testName = trace.GetFrames().Where(frame => frame.GetMethod().Name.StartsWith("TC", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().GetMethod().Name;
            ParameterFileName = string.Format("{0}.xml", trace.GetFrames().Where(frame => frame.GetMethod().Name.StartsWith("TC", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().GetMethod().ReflectedType.FullName.Split('.').LastOrDefault());
            return AuScGen.BaseSetings.GetParameter(ParamFile, testName, key);
        }

        /// <summary>
        /// Gets the excel parameter.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Parameter</returns>
        public static string GetExcelParameter(string key)
        {
            StackTrace trace = new StackTrace();
            var testName = trace.GetFrames().Where(frame => frame.GetMethod().Name.StartsWith("TC", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().GetMethod().Name;
            ParameterFileName = string.Format("{0}.xls", trace.GetFrames().Where(frame => frame.GetMethod().Name.StartsWith("TC", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().GetMethod().ReflectedType.FullName.Split('.').LastOrDefault());
            TestData data = new TestData(ParamFile);
            return data.GetTestData(testName).Where(oneDatum => oneDatum.Name.Equals(key)).FirstOrDefault().Data;
        }
    }
}
