// ***********************************************************************
// <copyright file="BaseSetings.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>BaseSetings class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AuScGen
{
    /// <summary>
    /// BaseSetings
    /// </summary>
    public class BaseSetings
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected static string GetValue(string xmlPath, string key)
        {
            XmlDocument xmlDoc = GetXmlDoc(xmlPath);
            XmlNodeList settingList = xmlDoc.SelectNodes("/TestSettings/TestSetting");
            foreach (XmlNode valueNode in settingList)
            {
                if (valueNode.FirstChild.Name.Equals(key))
                {
                    return valueNode.FirstChild.InnerText;
                }
            }
            //return xmlDoc.SelectNodes("/TestSettings/TestSetting");
            return null;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="testName">Name of the test.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>Parameter</returns>
        public static string GetParameter(string xmlPath, string testName, string parameterName)
        {
            XmlDocument xmlDoc = GetXmlDoc(xmlPath);
            XmlNodeList settingList = xmlDoc.SelectNodes("/TestParams/TestParam");
            
            foreach (XmlNode valueNode in settingList)
            {
                if (((XmlElement)valueNode).GetAttribute("Name").Equals(testName))
                {
                    return valueNode.ChildNodes.Cast<XmlElement>().Where(element => element.Name.Equals(parameterName)).FirstOrDefault().InnerText;
                }
            }
            //return xmlDoc.SelectNodes("/TestSettings/TestSetting");
            return null;
        }

        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="xmlPath">The XML path.</param>
        /// <returns></returns>
        protected static XmlDocument GetXmlDoc(string xmlPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(xmlPath);
            return XmlDoc;
        }    
    }
}
