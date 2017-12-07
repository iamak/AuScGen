using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuScGen.FunctionalTest
{
    public class LoginTest : TestBase
    {
        [TestFixtureSetUp]
        public void TestFixture()
        {
            Console.WriteLine("Test Fixture overridden");
            //base.TestFixture();
            Telerik.ActiveBrowser.NavigateTo(TCDAppUrl);
                //Steps.Login();
        }

        [Test]    
        public void TC01_Login()
        {
            //Page.LoginPage.UserName.DeskTopMouseClick();
            //KeyBoardSimulator.SetText("CSR_LEVEL1");
            //Page.LoginPage.Password.Text = "TEST";
            //Page.LoginPage.LoginButton.Click();    

            if (null == Page.LandingPage.Tray.TrayButton)
            {
                Assert.Fail("Unable to login to the application");
            }
            
            //Page.LandingPage.Tray.TrayButton.DeskTopMouseClick();
            
        }

        [Test]
        public void TC02_NavigateToAccountBalancePage()
        {
            Runner.DoStep("", () => 
            {
                //Page.LandingPage.Tray.TrayButton.DeskTopMouseClick();
                //Page.LandingPage.Tray.ClickTrayItem("account balance");
                //if (!Page.AccntBalancePage.AccntBalanceTabPresent)
                //{
                //    Assert.Fail("Unable to navigate to Account Balance page");
                //}
            });

            //Runner.DoStep("", () => Page.LandingPage.Tray.TrayButton.DeskTopMouseClick());

            //DoStep("",() => Page.LandingPage.Tray.TrayButton.DeskTopMouseClick());
            

            Thread.Sleep(5000);
        }


        //private void DoStep(string message, Action action)
        //{
        //    action();

        //    Telerik.ActiveBrowser.Capture().Save(@".\TC02_NavigateToAccountBalancePage\Image" + Guid.NewGuid() + ".png");
        //}

    }
}
