using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Ecolab.CommonUtilityPlugin;

namespace Ecolab.FunctionalTest.NonUITests
{
    public class ChemicalInjectionChartDataTest : TestBase
    {
        private string url = Config.TestSettings.Default.GetByFilterAPI;
        private string chartId = "2";

        [TestFixtureSetUp]
        protected void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Telerik.ActiveBrowser.Close();
        }

        [Test]
        public void TC01_TrendingDataCountWasher()
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("chartId", chartId);
            requestParams.Add("washerOrTunnelId", "1");
            requestParams.Add("compId", "1");
            requestParams.Add("parameter", "1");
            requestParams.Add("startDate", "09/23/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/23/2014 10:05:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            ValidateDataCount(dataFromService, 300);

            List<string> expectedSeries = new List<string>() { "Salt                          ", "Salt                           Standard"          
                                                             , "Dober Sour                    ", "Dober Sour                     Standard"
                                                             , "Dober Softener                ", "Dober Softener                 Standard"
                                                             ,"STAIN EX 1 4X500ML", "STAIN EX 1 4X500ML Standard"
                                                             ,"Liquid Soften-It              ","Liquid Soften-It               Standard"
                                                             ,"Alkalite II                   ","Alkalite II                    Standard"
                                                             ,"Print Builder                 ","Print Builder                  Standard"
                                                             ,"Hy-Sil                        ","Hy-Sil                         Standard"
                                                             ,"Formula Number","Step Number","Step Number Standard"};
            ValidateYAxisData(dataFromService, expectedSeries);
        }

        [Test]
        public void TC02_WasherDesiredValueTest()
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("chartId", chartId);
            requestParams.Add("washerOrTunnelId", "1");
            requestParams.Add("compId", "1");
            requestParams.Add("parameter", "1");
            requestParams.Add("startDate", "09/23/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/23/2014 10:05:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            DesiredValueTest(dataFromService, "Salt                           Standard", "5");
            DesiredValueTest(dataFromService, "Dober Sour                     Standard", "5");
            DesiredValueTest(dataFromService, "STAIN EX 1 4X500ML Standard", "3");
            DesiredValueTest(dataFromService, "Liquid Soften-It               Standard", "4");
            DesiredValueTest(dataFromService, "Alkalite II                    Standard", "5");
            DesiredValueTest(dataFromService, "Print Builder                  Standard", "7");
            DesiredValueTest(dataFromService, "Hy-Sil                         Standard", "6");
            DesiredValueTest(dataFromService, "Formula Number", "2");
            DesiredValueTest(dataFromService, "Step Number", "1");
            DesiredValueTest(dataFromService, "Step Number Standard", "60");
        }

        [Test]
        public void TC03_TrendingDataCountTunnel()
        {

            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("chartId", chartId);
            requestParams.Add("washerOrTunnelId", "2");
            requestParams.Add("compId", "1");
            requestParams.Add("parameter", "-1");
            requestParams.Add("startDate", "09/23/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/23/2014 10:01:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            ValidateDataCount(dataFromService, 60);

            List<string> expectedSeries = new List<string>() { "STAIN EX 2 4X500ML", "STAIN EX 2 4X500ML Standard"
                                                             , "Dober Softener                ", "Dober Softener                 Standard"
                                                             , "Alkalite II                   ","Alkalite II                    Standard"
                                                             ,"Hy-Sil                        ","Hy-Sil                         Standard"
                                                             ,"Citrasolve                    ","Citrasolve                     Standard"
                                                             ,"Fabristat                     ","Fabristat                      Standard"
                                                             ,"Formula Number","Transfer Signal","Weight","Weight Standard"};
            ValidateYAxisData(dataFromService, expectedSeries);
        }

        [Test]
        public void TC04_TunnelDesiredValueTest()
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("chartId", chartId);
            requestParams.Add("washerOrTunnelId", "2");
            requestParams.Add("compId", "1");
            requestParams.Add("parameter", "-1");
            requestParams.Add("startDate", "09/23/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/23/2014 10:01:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            DesiredValueTest(dataFromService, "STAIN EX 2 4X500ML Standard", "2");
            DesiredValueTest(dataFromService, "Dober Softener                 Standard", "7");
            DesiredValueTest(dataFromService, "Alkalite II                    Standard", "3");
            DesiredValueTest(dataFromService, "Hy-Sil                         Standard", "8");
            DesiredValueTest(dataFromService, "Citrasolve                     Standard", "8");
            DesiredValueTest(dataFromService, "Fabristat                      Standard", "5");
            DesiredValueTest(dataFromService, "Formula Number", "1");
            DesiredValueTest(dataFromService, "Transfer Signal", "1");
            DesiredValueTest(dataFromService, "Weight Standard", "60");
        }

        private void ValidateDataCount(List<ResponseDataItem> dataFromService, int numberofSeconds)
        {
            int numberOfData = (numberofSeconds / 5) + 1;

            foreach (ResponseDataItem data in dataFromService)
            {
                if (data.Data.Count != numberOfData)
                {
                    Assert.Fail("Total number of data for {0} is {1} in place of {2}", data.Name, data.Data.Count, numberOfData);
                }
            }
        }

        private void ValidateYAxisData(List<ResponseDataItem> dataFromService, List<string> dataSeries)
        {
            if (dataFromService.Count != dataSeries.Count)
            {
                Assert.Fail("Acctual count of data series does not match with expected Actual:{0}, Expected:{1}", dataFromService.Count, dataSeries.Count);
            }

            foreach (ResponseDataItem data in dataFromService)
            {
                if (!dataSeries.Contains(data.Name))
                {
                    Assert.Fail("Data series {0} is not expected", data.Name);
                }
            }
        }

        private void DesiredValueTest(List<ResponseDataItem> dataFromService, string seriesName, string desiredValue)
        {
            ResponseDataItem dataseriesName = dataFromService.Where(data => data.Name.Equals(seriesName)).FirstOrDefault();

            foreach (KeyValuePair<string, string> value in dataseriesName.Data)
            {
                if (!value.Value.Equals(desiredValue))
                {
                    Assert.Fail("Desired value for {0} item is {1} in place of {2}", seriesName, value.Value, desiredValue);
                }
            }
        }
    }
}
