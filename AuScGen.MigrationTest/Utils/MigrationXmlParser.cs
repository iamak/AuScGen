using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using System.ComponentModel.Composition;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Controls;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;

namespace Ecolab.MigrationTest
{
    public class MigrationXmlParser
    {
        private XmlNodeList nodeList;

        public void ReadConfigDBConfig(string configXmlPath)
        {
            XmlNodeList nodeList = GetDBConfig(configXmlPath);
            string dbname;
            string connection;
            string dbCategory;
            foreach (XmlNode node in nodeList)
            {
                dbname = ((XmlElement)node).GetAttribute("Name");
                connection = node.SelectSingleNode("./Connection").InnerText;
                dbCategory = node.SelectSingleNode("./DBCatogory").InnerText;
               //dbConfigList.Add(new DBConfig(dbname, connection, dbCategory));
            }
        }

        public string SourceDBName(XmlNode node)
        {
            XmlNode sourceDBName = node.SelectSingleNode("./Source/DBName");
            return sourceDBName.InnerText;
        }
        public string SourceTableName(XmlNode node)
        {
            XmlNode sourceTableName = node.SelectSingleNode("./Source/TableName");
            return sourceTableName.InnerText;
        }
        public string SourceConnection(XmlNode node)
        {
            XmlNode sourceQuery = node.SelectSingleNode("./Source/ConnectionString");
            return sourceQuery.InnerText;
        }

        public string SourceQuery(XmlNode node)
        {
            XmlNode sourceQuery = node.SelectSingleNode("./Source/Query");
            return sourceQuery.InnerText;
        }

        public string SourceUniqueId(XmlNode node)
        {
            XmlNode sourceUniqueId = node.SelectSingleNode("./Source/UniqueId");
            return sourceUniqueId.InnerText;
        }

        public ReadOnlyCollection<string> SourceTableFields(XmlNode node)
        {
            XmlNode sourceColumns = node.SelectSingleNode("./Source/Fields");
            ReadOnlyCollection<string> sourceFieldsList =  sourceColumns.InnerText.Split(',').ToList().AsReadOnly();
            return sourceFieldsList;
        }

        public ReadOnlyCollection<string> TargetTableFields(XmlNode node)
        {
            XmlNode targetColumns = node.SelectSingleNode("./Target/Fields");
            ReadOnlyCollection<string> targetFieldsList = targetColumns.InnerText.Split(',').ToList().AsReadOnly();
            return targetFieldsList;
        }

        public string TargetQuery(XmlNode node)
        {
            XmlNode targetQuery = node.SelectSingleNode("./Target/Query");
            return targetQuery.InnerText;
        }

        public string TargetDBName(XmlNode node)
        {
            XmlNode targetDBName = node.SelectSingleNode("./Target/DBName");
            return targetDBName.InnerText;
        }

        public string TargetTableName(XmlNode node)
        {
            XmlNode targetTableName = node.SelectSingleNode("./Target/TableName");
            return targetTableName.InnerText;
        }

        public string TargetUniqueId(XmlNode node)
        {
            XmlNode targetUniqueId = node.SelectSingleNode("./Target/UniqueId");
            return targetUniqueId.InnerText;
        }

        public string TargetConnection(XmlNode node)
        {
            XmlNode sourceQuery = node.SelectSingleNode("./Target/ConnectionString");
            return sourceQuery.InnerText;
        }

        private XmlNodeList GetDBConfig(string XMLPath)
        {
            XmlDocument xmlDoc = GetXmlDoc(XMLPath);
            return xmlDoc.SelectNodes("/MigrationTest/Configuration/DBName");
        }

        public XmlNode GetMigrateTestParams_Copy(string XMLPath, string TestName)
        {
            XmlDocument xmlDoc = GetXmlDoc(XMLPath);
            string databasename = xmlDoc.DocumentElement.Name;

            foreach (XmlNode node in xmlDoc.SelectNodes("/" + databasename + "/*[starts-with(name(), 'SourceTableName')]"))
            {
                string tablename = node.Attributes["targetTable"].Value;
                string Columns = "";
                string Values = "";

                foreach (XmlNode field in node.SelectNodes("Field"))
                {
                    Columns += (!string.IsNullOrEmpty(Columns) ? ", " : "") + field.Attributes["targetField"].Value;
                    Values += (!string.IsNullOrEmpty(Values) ? ", " : "") + "'" + field.InnerText + "'";
                }
            }
            
            nodeList = xmlDoc.SelectNodes("/MigrationTest/MigrationTestStep");
            return GetNodes("MigrationTestStep", "TestName", TestName);
           
        }

        private XmlNode GetNodes_Copy(string nodeName, string attributeName, string attributeValue)
        {
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == nodeName && string.Equals(((XmlElement)node).GetAttribute(attributeName), attributeValue))
                {
                    return node;
                }
            }
            return null;
        }

        public XmlNode GetMigrateTestParams(string XMLPath, string TestName)
        {
            XmlDocument xmlDoc = GetXmlDoc(XMLPath);

            nodeList = xmlDoc.SelectNodes("/MigrationTest/MigrationTestStep");

            return GetNodes("MigrationTestStep", "TestName", TestName);
        }

        private XmlNode GetNodes(string nodeName, string attributeName, string attributeValue)
        {
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == nodeName && string.Equals(((XmlElement)node).GetAttribute(attributeName), attributeValue))
                {
                    return node;
                }
            }
            return null;
        }

        private XmlDocument GetXmlDoc(string XMLPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XMLPath);
            return XmlDoc;
        }

        public void GetXMLData(string myXmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(myXmlString); // suppose that myXmlString contains "<Names>...</Names>"
            XmlNodeList xnList = xml.SelectNodes("/Names/Name");
            foreach (XmlNode xn in xnList)
            {
                string firstName = xn["FirstName"].InnerText;
                string lastName = xn["LastName"].InnerText;
                Console.WriteLine("Name: {0} {1}", firstName, lastName);
            }
        }
    }
}

