using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Threading;
using Ecolab.CommonUtilityPlugin;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;

namespace Ecolab.MigrationTest
{
    public class TestBase
    {
        private string parserPath;
        protected string TestParamsPath
        {
            get
            {
                return Directory.GetCurrentDirectory() + @"\TestData\";
            }
        }

        public TestBase(string testparams)
        {
            parserPath = string.Concat(TestParamsPath, testparams);
        }

        public TestBase()
        {

        }
        private static GetTestReport testDBReport;
        public static GetTestReport TestDBReport
        {
            get
            {
                if (null == testDBReport)
                {
                    testDBReport = new GetTestReport();
                }
                return testDBReport;
            }
        }
    }
}
