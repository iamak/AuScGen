using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.FunctionalTest.Utils
{
    public class Steps
    {
        private Page page;
        private TestBase test;
        public Steps(Page pageObject, TestBase testInstance)
        {
            page = pageObject;
            test = testInstance;
        }

        public void Login()
        {
            page.LoginPage.UserName.SetText("CSR_LEVEL1");
            //test.KeyBoardSimulator.SetText("");
            //test.KeyBoardSimulator.SetText("CSR_LEVEL1");
            page.LoginPage.Password.Text = "TEST";
            page.LoginPage.LoginButton.DesktopMouseClick();
            page.LandingPage.MainTray.IsVisible();            
        }
    }
}
