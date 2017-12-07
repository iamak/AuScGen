using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ecolab.MigrationTest
{
   public  class TestParameters
   {
       private string fileName;
       private string testName;
       private string testParams;
       private static MigrationXmlParser parser;
       private static MigrationXmlParser MigrationParser
       {
           get
           {
               if (null == parser)
               {
                   parser = new MigrationXmlParser();
               }
               return parser;
           }
       }

       public TestParameters(string testParamsPath, string testcaseName)
       {
           testParams = testParamsPath;
           testName = testcaseName;
       }

       public string GetSourceQuery 
       { 
           get
           {
               return MigrationParser.SourceQuery(TestDataXMLNodeList);
           }
       }

       public string GetTargetQuery
       {
           get
           {
               return MigrationParser.TargetQuery(TestDataXMLNodeList);
           }
       }

       public string TestName 
       { 
           get
           {
               return testName;
           }         
       }

       public string FileName
       {
           get
           {
               return fileName;
           }
           set
           {
               fileName = value;
           }
       }

       public XmlNode TestDataXMLNodeList
        { 
            get
            {
                return MigrationParser.GetMigrateTestParams(testParams, TestName);
            }
        }
     
       public string SourceDBName
       {
           get
           {
               return MigrationParser.SourceDBName(TestDataXMLNodeList);
           }
       }
       public string SourceTableName
       {
           get
           {
               return MigrationParser.SourceTableName(TestDataXMLNodeList);
           }
       }
       public string TargetDBName
       {
           get
           {
               return MigrationParser.TargetDBName(TestDataXMLNodeList);
           }
       }
       public string TargetTableName
       {
           get
           {
               return MigrationParser.TargetTableName(TestDataXMLNodeList);
           }
       }
       public string SourceTableUniqueId 
       {
           get
           {
               return MigrationParser.SourceUniqueId(TestDataXMLNodeList);
           }
       }
       public string TargetTableUniqueId
       {
           get
           {
               return MigrationParser.TargetUniqueId(TestDataXMLNodeList);
           }
       }

       public ReadOnlyCollection<string> SourceFieldCollection
       {
           get
           {
               return MigrationParser.SourceTableFields(TestDataXMLNodeList);
           }
       }
       public ReadOnlyCollection<string> TargetFieldCollection
       {
           get
           {
               return MigrationParser.TargetTableFields(TestDataXMLNodeList);
           }
       }
    }
}
