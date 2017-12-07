// ***********************************************************************
// <copyright file="TestCategory.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>TestCategoryAttribute class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;

namespace AuScGen
{
	/// <summary>
	/// Class TestType
	/// </summary>
	public enum TestType
    {
        /// <summary>
        /// The functional
        /// </summary>
        functional,
        /// <summary>
        /// The regression
        /// </summary>
        regression,
        /// <summary>
        /// The BVT
        /// </summary>
        bvt,
        /// <summary>
        /// The ondemand
        /// </summary>
        ondemand,
        /// <summary>
        /// The digital
        /// </summary>
        Digital,
        /// <summary>
        /// The MCP
        /// </summary>
        MCP,
        /// <summary>
        /// The SFP
        /// </summary>
        SFP,
        /// <summary>
        /// The argosy
        /// </summary>
        Argosy,
        /// <summary>
        /// The art institute
        /// </summary>
        ArtInstitute,
        /// <summary>
        /// The BMC
        /// </summary>
        BMC,
        /// <summary>
        /// The BMC below18
        /// </summary>
        BMCBelow18,
        /// <summary>
        /// The BMC above18
        /// </summary>
        BMCAbove18,
        /// <summary>
        /// The south
        /// </summary>
        South,
        /// <summary>
        /// The argosy online above18
        /// </summary>
        ArgosyOnlineAbove18,
        /// <summary>
        /// The argosy online below18
        /// </summary>
        ArgosyOnlineBelow18,
        /// <summary>
        /// The argosy ground below18
        /// </summary>
        ArgosyGroundBelow18,
        /// <summary>
        /// The argosy ground above18
        /// </summary>
        ArgosyGroundAbove18
    }
    /// <summary>
    ///     Class TestCategoryAttribute
    /// </summary>
    public class TestCategoryAttribute : CategoryAttribute
    {

        /// <summary>
        /// Gets the excluded file path.
        /// </summary>
        /// <value>
        /// The excluded file path.
        /// </value>
        private static string ExcludedFilePath
        {
            get
            {
                return string.Format(@"{0}\Config\ExcludedTest.xml", Directory.GetCurrentDirectory());
            }
        }

        /// <summary>
        /// The excluded test list
        /// </summary>
        private static List<XmlNode> excludedTestList;
        /// <summary>
        /// Gets the excluded test list.
        /// </summary>
        /// <value>
        /// The excluded test list.
        /// </value>
        private static List<XmlNode> ExcludedTestList
        {
            get
            {
                if (null == excludedTestList)
                {
                    excludedTestList = new List<XmlNode>();
                    foreach (XmlNode node in GetExcludedTests(ExcludedFilePath))
                    {
                        excludedTestList.Add(node);
                    }
                }

                return excludedTestList;

            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCategoryAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public TestCategoryAttribute(TestType type)
            : base(ConvertTestTypeToString(type))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCategoryAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="testName">Name of the test.</param>
        public TestCategoryAttribute(TestType type, string testName)
            : base(ConvertTestTypeToString(type, testName))
        { }

        /// <summary>
        /// Converts the test type to string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static string ConvertTestTypeToString(TestType type)
        {
            switch (type)
            {
                case TestType.functional:
                    return "Functional";

                case TestType.regression:
                    return "Regression";

                case TestType.bvt:
                    return "BVT";

                case TestType.ondemand:
                    return "OnDemand";

                case TestType.Digital:
                    return "Digital";

                case TestType.MCP:
                    return "MCP";

                case TestType.SFP:
                    return "SFP";

                case TestType.Argosy:
                    return "Argosy";

                case TestType.ArgosyOnlineAbove18:
                    return "ArgosyOnlineAbove18";

                case TestType.ArgosyOnlineBelow18:
                    return "ArgosyOnlineBelow18";

                case TestType.ArgosyGroundBelow18:
                    return "ArgosyGroundBelow18";

                case TestType.ArgosyGroundAbove18:
                    return "ArgosyGroundAbove18";

                case TestType.BMC:
                    return "BMC";

                case TestType.BMCBelow18:
                    return "BMCBelow18";

                case TestType.BMCAbove18:
                    return "BMCAbove18";

                case TestType.ArtInstitute:
                    return "ArtInstitute";

                case TestType.South:
                    return "South";

                default:
                    return "Regression";
            }
        }

        /// <summary>
        /// Converts the test type to string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="testName">Name of the test.</param>
        /// <returns></returns>
        private static string ConvertTestTypeToString(TestType type, string testName)
        {
            foreach (XmlNode excludedTestName in ExcludedTestList)
            {
                if (excludedTestName.SelectSingleNode("./TestName").InnerText.Equals(testName))
                {
                    if (null != excludedTestName.SelectSingleNode("./TestSuit"))
                    {
                        return excludedTestName.SelectSingleNode("./TestSuit").InnerText;
                    }
                    else
                    {
                        return "Excluded";
                    }

                }
            }

            return ConvertTestTypeToString(type);
        }

        /// <summary>
        /// Gets the excluded tests.
        /// </summary>
        /// <param name="XMLPath">The XML path.</param>
        /// <returns></returns>
        private static XmlNodeList GetExcludedTests(string XMLPath)
        {
            XmlDocument xmlDoc = GetXmlDoc(XMLPath);

            return xmlDoc.SelectNodes("/TestCases/Test");
        }

        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="XMLPath">The XML path.</param>
        /// <returns></returns>
        private static XmlDocument GetXmlDoc(string XMLPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XMLPath);
            return XmlDoc;
        }
    }
}
