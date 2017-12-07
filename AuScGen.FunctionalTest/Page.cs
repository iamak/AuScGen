using AuScGen.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.FunctionalTest
{
    public class Page
    {
        private List<object> utilsList = new List<object>();

        public Page(TestBase testBase)
        {
            //Telerik = testBase.Telerik;

            utilsList.Add(testBase.Telerik);
            utilsList.Add(testBase.KeyBoardSimulator);
            //utilsList.Add(testBase.DialogHandler);
            utilsList.Add(testBase.DBValidation);
        }

        private LoginPage loginPage;
        public LoginPage LoginPage
        {
            get
            {
                if (null == loginPage)
                {
                    loginPage = new LoginPage(utilsList);
                }
                return loginPage;
            }
        }

        private LandingPage landingPage;
        public LandingPage LandingPage
        {
            get
            {
                if (null == landingPage)
                {
                    landingPage = new LandingPage(utilsList);
                }
                return landingPage;
            }
        }

        private AccntBalancePage accntBalancePage;
        public AccntBalancePage AccntBalancePage
        {
            get
            {
                if (null == accntBalancePage)
                {
                    accntBalancePage = new AccntBalancePage(utilsList);
                }
                return accntBalancePage;
            }
        }
    }
}
