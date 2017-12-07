using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using System.Threading;

namespace Ecolab.FunctionalTest
{

    public class MainMenuTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            //base.TestFixtureSetupBase();
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestAdmin", "test");
        }

        //[TestFixtureTearDown]
        //public void TestFixtureTearDown()
        //{
        //    Console.WriteLine("Test Fixture Teardown overriden");
        //    //base.TestFixtureTearDown();
        //}

        /// <summary>
        /// Test case 18624: Verify availability of SETUP fromTop menu in the Homepage
        /// Test case 18643: Verify SETUP displays list with two options
        /// </summary>
        [TestCategory(TestType.bvt, "TC01_VerySetupMenuItem")]
        [TestCategory(TestType.regression, "TC01_VerySetupMenuItem")]
        [TestCategory(TestType.functional, "TC01_VerySetupMenuItem")]
        [Test]
        public void TC01_VerySetupMenuItem()
        {
            Assert.True(Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Setup"));

            Assert.True(Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Plant Setup"));

            Assert.True(Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Controller Setup"));

            Assert.True(Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Washer Groups"));

            Assert.True(Page.LoginPage.TopMainMenu.MenuItemsList.LastOrDefault().Contains("Storage Tanks"));
        }

        /// <summary>
        /// Test case 18644: Verify Plant setup and Controller setup
        /// </summary>
        [TestCategory(TestType.bvt, "TC02_VerifyPlantSetup")]
        [TestCategory(TestType.regression, "TC02_VerifyPlantSetup")]
        [TestCategory(TestType.functional, "TC02_VerifyPlantSetup")]
        [Test]
        public void TC02_VerifyPlantSetup()
        {
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();
                        
            Thread.Sleep(3000);
            Assert.True(Page.PlantSetupPage.BreadCrumbDetailsList().Contains("Plant Setup"));

            Assert.True(string.Equals(Page.PlantSetupPage.ActiveTabItem, "General"));
            
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();

            Thread.Sleep(3000);
            Assert.True(Page.PlantSetupPage.BreadCrumbDetailsList().Contains("Controller Setup"));

            Page.LoginPage.TopMainMenu.NavigateToWasherGroupsPage();

            Thread.Sleep(3000);
            Assert.True(Page.PlantSetupPage.BreadCrumbDetailsList().Contains("Washer Groups"));

            Page.LoginPage.TopMainMenu.NavigateToStorageTanksPage();    

            Thread.Sleep(3000);
            Assert.True(Page.PlantSetupPage.BreadCrumbDetailsList().Contains("Storage Tanks"));
            
            
        }

        /// <summary>n
        /// Test case 20650: Verify user able to navigate to PlantSetup from Controller Setup
        /// </summary>
        [TestCategory(TestType.bvt, "TC03_VerifyControllerToPlantSetupNavigation")]
        [TestCategory(TestType.regression, "TC03_VerifyControllerToPlantSetupNavigation")]
        [TestCategory(TestType.functional, "TC03_VerifyControllerToPlantSetupNavigation")]
        [Test]
        public void TC03_VerifyControllerToPlantSetupNavigation()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.LoginPage.TopMainMenu.NavigateToPlantSetupPage();

            Thread.Sleep(3000);
            Assert.True(Page.PlantSetupPage.BreadCrumbDetailsList().Contains("Plant Setup"));
        }

        [TestCategory(TestType.bvt, "TC04_VerifyLogOut")]
        [TestCategory(TestType.regression, "TC04_VerifyLogOut")]
        [TestCategory(TestType.functional, "TC04_VerifyLogOut")]
        [Test]
        public void TC04_VerifyLogOut()
        {
            Page.LoginPage.TopMainMenu.LogOut();
        }
    }
}
