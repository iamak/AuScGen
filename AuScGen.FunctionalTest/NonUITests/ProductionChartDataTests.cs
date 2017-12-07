using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Ecolab.CommonUtilityPlugin;

namespace Ecolab.FunctionalTest.NonUITests
{
    public class ProductionChartDataTests : TestBase
    {
        private string url = Config.TestSettings.Default.GetByFilterAPI;
        private string chartId = "1";

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
            requestParams.Add("startDate", "09/22/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/22/2014 10:05:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            ValidateDataCount(dataFromService,300);

            List<string> expectedSeries = new List<string>(){"Customer","Formaula Number","Formaula Number Standard","Transfer Signal","Weight","Weight Standard"};
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
            requestParams.Add("startDate", "09/22/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/22/2014 10:05:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            DesiredValueTest(dataFromService, "Customer", "1");
            DesiredValueTest(dataFromService, "Formaula Number", "1");
            DesiredValueTest(dataFromService, "Formaula Number Standard", "1");
            DesiredValueTest(dataFromService, "Transfer Signal", "1");
            DesiredValueTest(dataFromService, "Weight Standard", "60");
        }

        [Test]
        public void TC03_TrendingDataCountTunnel()
        {
            
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("chartId", chartId);
            requestParams.Add("washerOrTunnelId", "2");
            requestParams.Add("compId", "1");
            requestParams.Add("parameter", "-1");
            requestParams.Add("startDate", "09/22/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/22/2014 10:01:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);

            ValidateDataCount(dataFromService, 60);

            List<string> expectedSeries = new List<string>() { "Temperature", "Temperature Standard", "Conductivity", "Conductivity Standard", "pH", "pH Standard"
                                                               ,"Customer","Formula Number","Transfer Signal","Weight","Weight Standard"};
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
            requestParams.Add("startDate", "09/22/2014 10:00:00 AM");
            requestParams.Add("endDate", "09/22/2014 10:01:00 AM");

            List<ResponseDataItem> dataFromService = ServiceAccess.getRequest(url, requestParams);
                        
            DesiredValueTest(dataFromService, "Temperature Standard", "60");
            DesiredValueTest(dataFromService, "Conductivity Standard", "2");
            DesiredValueTest(dataFromService, "pH Standard", "6");
            DesiredValueTest(dataFromService, "Customer", "1");
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

        private void ValidateYAxisData(List<ResponseDataItem> dataFromService,List<string> dataSeries)
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

            foreach(KeyValuePair<string,string> value in dataseriesName.Data)
            {
                if(!value.Value.Equals(desiredValue))
                {
                    Assert.Fail("Desired value for {0} item is {1} in place of {2}", seriesName,value.Value, desiredValue);
                }
            }
        }
           
    }
}
