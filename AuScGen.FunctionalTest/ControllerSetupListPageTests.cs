using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Collections.ObjectModel;

using NUnit.Framework;
using Ecolab.Pages.CommonControls;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using System.Collections.Generic;

namespace Ecolab.FunctionalTest
{
    public class ControllerSetupListPageTests : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");

            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
            Page.LoginPage.VerifyLogin("AutoTestEng", "test");
            Page.PlantSetupPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.DeleteAllControllers();
            Precondition();           
        }       

        private void Precondition()
        {
            //Page.ControllerSetupPage.AddControllerButton.Click();
            //Page.ControllerGeneralSetupTabPage.ControllerModel.SelectByText("Ultrax 6/12/16", Timeout);
            //Page.ControllerGeneralSetupTabPage.ControllerType.SelectByText("Beckhoff", Timeout);

            //Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.DeskTopMouseClick();
            //KeyBoardSimulator.SetNumeric("1");
            //KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Back);
            //KeyBoardSimulator.SetNumeric("10.225.134.21.1");
            //Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.TypeText("10.225.134.21.1");
            //Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.DeskTopMouseClick();
            
            ////Page.ControllerSetupPage.Save.DeskTopMouseClick();
            //Page.ControllerSetupPage.Save.DeskTopMouseClick();

            AddBeckhoffController("1", false);
        }
        
        /// <summary>
        /// Test case 25060: RG: Controller(s) Setup -> List Page : Validate audit of Modified fields
        /// Test case 24683: RG: Verify Add Controller Functionality -Ultrax Beckhoff Model
        /// </summary>
        [Test(Description="Test case 25060: RG: Controller(s) Setup -> List Page : Validate audit of Modified fields")]
        public void TC01_VerifyAddEditBeckhoffController()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            AddAndVerifyBeckhoffController("2",true);
            VerifyAdvanceEditBeckhoff();
        }
                
        /// <summary>
        /// Test case 24716: RG: Verify Add Controller Functionality -Ultrax AB Model
        /// </summary>
        [Test]
        public void TC02_VerifyAddEditABController()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            AddAndVerifyABController("4");
            VerifyAdvanceEditAllenBradely();
        }

        /// <summary>
        /// Test case 24706: RG: Controller(s)\Edit Page->Verify Add Controller Functionality -MyControl Model
        /// </summary>
        [Test]
        public void TC03_VerifyAddmyController()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            AddAndVerifymyControl("5", true);
        }

        /// <summary>
        /// Test case 24756: RG: Verify Controller Model-Europe region
        /// </summary>
        [Test]
        public void TC04_VerifyControllerList()
        {
            DBValidation.UpdateData("update TCD.Plant set RegionId = 1 where EcolabAccountNumber = 1");
            //Page.LoginPage.TopMainMenu.LogOut();
            LoginWithUser("AutoTestEng", "test");

            List<string> controllerListEU = new List<string>() { "Ultrax 6/12/16", "Ultrax 2000", "Elados Smart", "Softroll", "Utilitty logger", "Tank-controller"
                                                                ,"myControl","E-Control Plus","E-Control","Ecodose","PLC XL","Solumix"};
            VerifyControllerList(controllerListEU, "Europe");

            DBValidation.UpdateData("update TCD.Plant set RegionId = 2 where EcolabAccountNumber = 1");
            //Page.LoginPage.TopMainMenu.LogOut();
            LoginWithUser("AutoTestEng", "test");
            List<string> controllerListNA = new List<string>() { "myControl", "E-Control Plus", "E-Control", "Ecodose", "PLC XL", "Utilitty logger", "Tank-controller"
                                                                , "Solumix","Ultrax 6/12/16","Ultrax 2000","Elados Smart","Softroll" };
            VerifyControllerList(controllerListNA, "North America");
        }

        /// <summary>
        /// Test case 27083: RG: Controller(s) Setup -> List Page : Verify Delete Controller Functionality
        /// </summary>
        [Test]
        public void TC05_VerifyDelete()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            int intitialNumberOfRows = Page.ControllerSetupPage.ControllersTabGrid.Rows.Count;
            DataRowCollection initialDatarows = DBValidation.DataRows("select IsDeleted from TCD.ConduitController where IsDeleted='False'");
            
            DialogHandler.ClickonOKButton();
            Page.ControllerSetupPage.ControllersTabGrid.Rows.FirstOrDefault().GetButtonControls()[1].Click();

            Thread.Sleep(3000);
            if(null != Page.ControllerSetupPage.ControllerListMessage )
            {
                string message = Page.ControllerSetupPage.ControllerListMessage.BaseElement.InnerText;
                if(!message.Equals("Controller Deleted Successfully"))
                {
                    Assert.Fail("Controller delete message is incorrect Expected: Controller Deleted Successfully , Actual: {0}", message);
                }
            }
            else
            {
                Assert.Fail("Controller delete message is not displayed");
            }

            if(Page.ControllerSetupPage.ControllersTabGrid.Rows.Count == intitialNumberOfRows)
            {
                Assert.Fail("After contorller is deleted grid in the UI is not updated");
            }

            DataRowCollection finalDatarows = DBValidation.DataRows("select IsDeleted from ConduitController where IsDeleted='False'");
            if (finalDatarows.Count == initialDatarows.Count)
            {
                Assert.Fail("DB is not updated after controller is deleted");
            }
        }

        /// <summary>
        /// Test case 40135: RG: Controller(s) Setup -> List Page : Verify Cancel Button Functionality-Inline Edit Button
        /// Test case 40173: RG: Verify Cancel button Functionality-Inline
        /// Test case 25607: RG: Verify Edit Controller Functionality
        /// Test case 41252: RG: Controller(s) Setup -> List Page : Verify edit Button Functionality
        /// Test case 25607: RG: Verify Edit Controller Functionality
        /// </summary>
        [Test]
        public void TC06_VerifyCancelAndSaveUpdate()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            string intitilRowData = Page.ControllerSetupPage.ControllersTabGrid.Rows.FirstOrDefault().InnerText;
            Page.ControllerSetupPage.ControllersTabGrid.Rows.FirstOrDefault().GetButtonControls().FirstOrDefault().Click();
            Page.ControllerSetupPage.ControllersTabGrid.Rows.FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            string finalRowData = Page.ControllerSetupPage.ControllersTabGrid.Rows.FirstOrDefault().InnerText;
            if(!finalRowData.Equals(intitilRowData))
            {
                Assert.Fail("Data is changed after inline edit is cancelled in controller setup page");
            }
            VerifyCancelUpdate();
            VerifySaveUpdate();           
        }

        /// <summary>
        /// Test case 25415: RG: Controller(s) Setup -> List Page : Verify access to Controller Page depending on UserRoleId - 1 to 5
        /// Test case 25411: RG: Verify access to Controller Page depending on UserRoleId - 6
        /// </summary>
        [Test]
        public void TC07_UserRoleValidation()
        {
            LoginWithUser("AutoTestTMBasic", "test");
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            if(Page.ControllerSetupPage.IsAddControllerPresent)
            {
                Assert.Fail("Add controller button is present for level 6 user");
            }

            LoginWithUser("AutoTestPE", "test");
            if(Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Controller Setup"))
            {
                Assert.Fail("Level 3 user is having access to controller setup page");
            }

            LoginWithUser("AutoTestPM", "test");
            if (Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Controller Setup"))
            {
                Assert.Fail("Level 4 user is having access to controller setup page");
            }

            LoginWithUser("AutoTestCAM", "test");
            if (Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Controller Setup"))
            {
                Assert.Fail("Level 5 CAM user is having access to controller setup page");
            }

            LoginWithUser("AutoTestBDM", "test");
            if (Page.LoginPage.TopMainMenu.MenuItemsList.Contains("Controller Setup"))
            {
                Assert.Fail("Level 5 (Business development manager) user is having access to controller setup page");
            }

            LoginWithUser("AutoTestEng", "test");
        }

        /// <summary>
        /// Test case 25600: RG: Controller(s) Setup -> List Page : Verify access to Controller Page depending on UserRoleId - 7 and above
        /// </summary>
        [Test]
        public void TC08_AddControllerWithLevel7User()
        {
            LoginWithUser("AutoTestTMAdvance", "test");
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            AddAndVerifyBeckhoffController("3",false);
            LoginWithUser("AutoTestEng", "test");
        } 

        private void LoginWithUser(string userid, string pwds)
        {
            Page.LoginPage.TopMainMenu.LogOut();
            Page.LoginPage.VerifyLogin(userid, pwds);            
        }

        private void AddAndVerifyBeckhoffController(string controllerNumber, bool enterAMSNetId)
        {
            //Page.ControllerSetupPage.AddControllerButton.Click();
            //Page.ControllerGeneralSetupTabPage.ControllerModel.SelectByText("Ultrax 6/12/16", Timeout);
            //Page.ControllerGeneralSetupTabPage.ControllerType.SelectByText("Beckhoff", Timeout);
            
            //if(enterAMSNetId)
            //{
            //    //Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.DeskTopMouseClick();
            //    //KeyBoardSimulator.SetNumeric("123456789");
            //}
            
            //KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);

            //Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.DeskTopMouseClick();
            ////KeyBoardSimulator.SetNumeric(controllerNumber);
            
            //Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.TypeText(controllerNumber);
            //Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.SpinDownClick();
            //Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.SpinUpClick();
                                 
            

            //Page.ControllerGeneralSetupTabPage.ControllerNameUltraxBEC.TypeText("test");
            //Page.ControllerGeneralSetupTabPage.WebPortLoginUltraxBEC.TypeText("test");
            //Page.ControllerGeneralSetupTabPage.WebPortIPUltraxBEC.TypeText("test");
            //Page.ControllerGeneralSetupTabPage.WebPortPasswordUltraxBEC.TypeText("test");
            //Page.ControllerGeneralSetupTabPage.SerialNumberUltraxBEC.TypeText("12345");

            //if (!Page.ControllerSetupPage.IsSaveButtonEnabled())
            //{
            //    Page.ControllerSetupPage.Save.DeskTopMouseClick();
            //}
            //Page.ControllerSetupPage.Save.DeskTopMouseClick();
            AddBeckhoffController(controllerNumber, enterAMSNetId);
            Thread.Sleep(3000);
            ValidateControllerAddition(controllerNumber, "Ultrax 6/12/16Beckhoff");
        }

        private void AddBeckhoffController(string controllerNumber, bool enterAMSNetId)
        {
            Page.ControllerSetupPage.AddControllerButton.Click();
            Page.ControllerGeneralSetupTabPage.ControllerModel.SelectByText("Ultrax 6/12/16", Timeout);
            Page.ControllerGeneralSetupTabPage.ControllerType.SelectByText("Beckhoff", Timeout);

            if (enterAMSNetId)
            {
                //Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.DeskTopMouseClick();
                //KeyBoardSimulator.SetNumeric("123456789");
            }

            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);

            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.DeskTopMouseClick();
            //KeyBoardSimulator.SetNumeric(controllerNumber);

            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.TypeText(controllerNumber);
            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.SpinDownClick();
            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxBEC.SpinUpClick();



            Page.ControllerGeneralSetupTabPage.ControllerNameUltraxBEC.TypeText("test");
            Page.ControllerGeneralSetupTabPage.WebPortLoginUltraxBEC.TypeText("test");
            Page.ControllerGeneralSetupTabPage.WebPortIPUltraxBEC.TypeText("test");
            Page.ControllerGeneralSetupTabPage.WebPortPasswordUltraxBEC.TypeText("test");
            Page.ControllerGeneralSetupTabPage.SerialNumberUltraxBEC.TypeText("12345");

            if (!Page.ControllerSetupPage.IsSaveButtonEnabled())
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
            }
            Page.ControllerSetupPage.Save.DeskTopMouseClick();
        }

        private void AddAndVerifyABController(string controllerNumber)
        {
            Page.ControllerSetupPage.AddControllerButton.Click();
            Page.ControllerGeneralSetupTabPage.ControllerModel.SelectByText("Ultrax 6/12/16", Timeout);
            Page.ControllerGeneralSetupTabPage.ControllerType.SelectByText("AllenBradley", Timeout);

            Page.ControllerGeneralSetupTabPage.OPCServerUltraxAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1");

            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxAB.DeskTopMouseClick();
            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxAB.TypeText(controllerNumber);
            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxAB.SpinDownClick();
            Page.ControllerGeneralSetupTabPage.ControllerNumberUltraxAB.SpinUpClick();

            Page.ControllerGeneralSetupTabPage.OPCObjectUltraxAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1");

            Page.ControllerGeneralSetupTabPage.IPAddressAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1");

            Page.ControllerGeneralSetupTabPage.DDEDriverUltraxAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1");

            Page.ControllerGeneralSetupTabPage.ComPortNumberUltraxAB.DeskTopMouseClick();
            Page.ControllerGeneralSetupTabPage.ComPortNumberUltraxAB.TypeText("1");
            Page.ControllerGeneralSetupTabPage.ComPortNumberUltraxAB.SpinDownClick();
            Page.ControllerGeneralSetupTabPage.ComPortNumberUltraxAB.SpinUpClick();

            if (!Page.ControllerSetupPage.IsSaveButtonEnabled())
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
            }
            Page.ControllerSetupPage.Save.DeskTopMouseClick();
            Thread.Sleep(3000);
            ValidateControllerAddition(controllerNumber, "Ultrax 6/12/16AllenBradley");
        }

        private void AddAndVerifymyControl(string controllerNumber, bool enterAMSNetId)
        {
            Page.ControllerSetupPage.AddControllerButton.Click();
            Page.ControllerGeneralSetupTabPage.ControllerModel.SelectByText("myControl", Timeout);
            Page.ControllerGeneralSetupTabPage.ControllerType.SelectByText("Beckhoff", Timeout);

            if (enterAMSNetId)
            {
                Page.ControllerGeneralSetupTabPage.AMSNetIDmyControlBEC.DeskTopMouseClick();
                KeyBoardSimulator.SetNumeric("123456789");
            }

            KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);

            Page.ControllerGeneralSetupTabPage.ControllerNumbermyControlBEC.DeskTopMouseClick();
            //KeyBoardSimulator.SetNumeric(controllerNumber);

            Page.ControllerGeneralSetupTabPage.ControllerNumbermyControlBEC.TypeText(controllerNumber);
            Page.ControllerGeneralSetupTabPage.ControllerNumbermyControlBEC.SpinDownClick();
            Page.ControllerGeneralSetupTabPage.ControllerNumbermyControlBEC.SpinUpClick();

            if (!Page.ControllerSetupPage.IsSaveButtonEnabled())
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
            }
            Page.ControllerSetupPage.Save.DeskTopMouseClick();
            Thread.Sleep(3000);
            ValidateControllerAddition(controllerNumber, "myControlBeckhoff");
        }

        private void ValidateControllerAddition(string controllerNumber, string controllerType)
        {
            string message = Page.ControllerSetupPage.ControllerAddMessage.BaseElement.InnerText;

            if (null != message)
            {
                if (!message.Equals("Controller Saved Successfully"))
                {
                    Assert.Fail("Message incorrect after controller addition Expected Controller Saved Successfully, Actual:{0}", message);
                }
            }

            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();

            if (Page.ControllerSetupPage.IsControllerGridFirstColumnContains("1" + controllerNumber))
            {
                Assert.Fail("After adding controller, new controller is not displayed in grid");
            }

            if (Page.ControllerSetupPage.ControllersTabGrid.SelectedRows(controllerType).Count <= 0)
            {
                Assert.Fail("Controller type is not correctly displayed in UI grid after controller addition");
            }

            DataRowCollection dbValues = DBValidation.DataRows("select top 1 * from TCD.ConduitController order by InstallDate desc");

            if (!Convert.ToBoolean(dbValues[0].ItemArray[18]))
            {
                Assert.Fail("Database is not updated after adding controller");
            }

            //TODO: Looks like audit tables are not getting updated, add audit validation here
            //select * from auditcolumnsdetails where id = (select top 1 ControllerId from ConduitController order by InstallDate desc)
        }    

        private void VerifyCancelUpdate()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            string initialValueCancel = Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.Value;
            Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("123000");

            Page.ControllerSetupPage.WaitforAction(() =>
            {
                Page.ControllerSetupPage.Cancel.DeskTopMouseClick();
                return Page.ControllerSetupPage.IsSaveButtonEnabled();
            }, Timeout);

            Page.ControllerSetupPage.Cancel.DeskTopMouseClick();
            DialogHandler.NoButton.Click();            
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            string finalvalueCancel = Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.Value;

            if (initialValueCancel != finalvalueCancel)
            {
                Assert.Fail("After editing controller, new value is not displayed in controller list page grid");
            }
        }

        private void VerifySaveUpdate()
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            string initialValueSave = Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.Value;
            Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("123000");

            Page.ControllerSetupPage.WaitforAction(() =>
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
                return Page.ControllerSetupPage.IsSaveButtonEnabled();
            }, Timeout);

            Page.ControllerSetupPage.Save.DeskTopMouseClick();

            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            string finalvalueSave = Page.ControllerGeneralSetupTabPage.AMSNetIDAddressUltraxBEC.Value;

            if (initialValueSave == finalvalueSave)
            {
                Assert.Fail("After editing controller, new value is not displayed in controller list page grid");
            }
        }

        private void VerifyAdvanceEditAllenBradely()
        {
            Pages.CommonControls.EcolabDataGridItems row = Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("AllenBradley").FirstOrDefault();
            ReadOnlyCollection<string> rowValus = row.GetColumnValues();
            row.GetButtonControls().LastOrDefault().Click();
            Page.ControllerSetupPage.AdvanceTab.Click();

            if(null != Page.ControllerAdvancedSetupPage.ControllerName)
            {
                string actualMessage = Page.ControllerAdvancedSetupPage.ControllerName.BaseElement.InnerText;
                string expectedMessage = string.Format("{0} ({1} - AllenBradley)",rowValus.FirstOrDefault(),rowValus[1]);
                if(actualMessage.Equals(expectedMessage))
                {
                    Assert.Fail("Controller name is incorrect , Expected:{0} , Actual{0}",expectedMessage,actualMessage);
                }
            }

            Page.ControllerAdvancedSetupPage.WebPortIPAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("11");

            Page.ControllerAdvancedSetupPage.WebPortLoginAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("12");

            Page.ControllerAdvancedSetupPage.WebPortPasswordAB.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1234");

            Page.ControllerSetupPage.WaitforAction(() =>
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
                return Page.ControllerSetupPage.IsSaveButtonEnabled();
            }, Timeout);

            Page.ControllerSetupPage.Save.DeskTopMouseClick();
            Page.ControllerSetupPage.TrySelectBacktoController();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("AllenBradley").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.ControllerSetupPage.AdvanceTab.Click();

            if (!Page.ControllerAdvancedSetupPage.WebPortIPAB.Value.Equals("11"))
            {
                Assert.Fail("Web port ip value is not updated corerctly for Allen Bradely controller");
            }

            if (!Page.ControllerAdvancedSetupPage.WebPortLoginAB.Value.Equals("12"))
            {
                Assert.Fail("Web port login value is not updated corerctly for Allen Bradely controller");
            }

            if (!Page.ControllerAdvancedSetupPage.WebPortPasswordAB.Value.Equals("1234"))
            {
                Assert.Fail("Web port password value is not updated corerctly for Allen Bradely controller");
            }
        }

        private void VerifyAdvanceEditBeckhoff()
        {
            Pages.CommonControls.EcolabDataGridItems row = Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault();
            ReadOnlyCollection<string> rowValus = row.GetColumnValues();
            row.GetButtonControls().LastOrDefault().Click();
            Page.ControllerSetupPage.AdvanceTab.Click();

            if (null != Page.ControllerAdvancedSetupPage.ControllerName)
            {
                string actualMessage = Page.ControllerAdvancedSetupPage.ControllerName.BaseElement.InnerText;
                string expectedMessage = string.Format("{0} ({1} - Beckhoff)", rowValus.FirstOrDefault(), rowValus[1]);
                if (actualMessage.Equals(expectedMessage))
                {
                    Assert.Fail("Controller name is incorrect , Expected:{0} , Actual{0}", expectedMessage, actualMessage);
                }
            }
            
            Page.ControllerAdvancedSetupPage.WebPortIPBEC.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("11");

            Page.ControllerAdvancedSetupPage.WebPortLoginBEC.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("12");

            Page.ControllerAdvancedSetupPage.WebPortPasswordBEC.DeskTopMouseClick();
            KeyBoardSimulator.SetNumeric("1234");

            Page.ControllerSetupPage.WaitforAction(() =>
            {
                Page.ControllerSetupPage.Save.DeskTopMouseClick();
                return Page.ControllerSetupPage.IsSaveButtonEnabled();
            }, Timeout);

            Page.ControllerSetupPage.Save.DeskTopMouseClick();
            Page.ControllerSetupPage.TrySelectBacktoController();
            Page.ControllerSetupPage.ControllersTabGrid.SelectedRows("Beckhoff").FirstOrDefault().GetButtonControls().LastOrDefault().Click();
            Page.ControllerSetupPage.AdvanceTab.Click();

            if (!Page.ControllerAdvancedSetupPage.WebPortIPBEC.Value.Equals("11"))
            {
                Assert.Fail("Web port ip value is not updated corerctly for Allen Bradely controller");
            }

            if (!Page.ControllerAdvancedSetupPage.WebPortLoginBEC.Value.Equals("12"))
            {
                Assert.Fail("Web port login value is not updated corerctly for Allen Bradely controller");
            }

            if (!Page.ControllerAdvancedSetupPage.WebPortPasswordBEC.Value.Equals("1234"))
            {
                Assert.Fail("Web port password value is not updated corerctly for Allen Bradely controller");
            }            
        }

        private void VerifyControllerList(List<string> expectedList, string region)
        {
            Page.LoginPage.TopMainMenu.NavigateToControlerSetupPage();
            Page.ControllerSetupPage.AddControllerButton.Click();
            ReadOnlyCollection<string> actualList = Page.ControllerGeneralSetupTabPage.GetControllerModelList();
            //new List<string>() { "A", "B", "C" }.AsReadOnly();
            foreach(string controller in actualList)
            {
                if(actualList.IndexOf(controller) != expectedList.IndexOf(controller))
                {
                    Assert.Fail("Order of controller {0} for region {1} is different Expected order:{2}, Actual order:{3}", controller, region,expectedList.IndexOf(controller)+1, actualList.IndexOf(controller)+1);
                }
            }            
        }
    }
}
