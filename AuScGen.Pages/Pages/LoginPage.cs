using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;


namespace Ecolab.Pages
{
    public class LoginPage : PageBase
    {
        private string guiMap;         

        public LoginPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin) 
        {
            guiMap = string.Concat(GuiMapPath, "LoginPage.xml");
        }

        public LoginPage(List<object> utilsList)
            :base(utilsList)
        {
            guiMap = string.Concat(GuiMapPath, "LoginPage.xml");
        }

        private CommonControls.MainMenu myTopMainMenu;
        public CommonControls.MainMenu TopMainMenu 
        { 
            get
            {
                if (null == myTopMainMenu)
                {
                    myTopMainMenu = new CommonControls.MainMenu(Telerik,GuiMapPath);
                }
                return myTopMainMenu;
            }
        }

        public void SelectRememberMe()
        {
            Telerik.WaitForControl<HtmlInputCheckBox>(guiMap, "chkbxTCDRememberMe", 
                    Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public bool IsRememberMeSelected
        {
            get
            {
                return Telerik.WaitForControl<HtmlInputCheckBox>(guiMap, "chkbxTCDRememberMe", 
                                            Config.PageClassSettings.Default.MaxTimeoutValue).Checked;
            }
        }

        public bool IsLoggedIn 
        { 
            get
            {
                //return Telerik.WaitForControl<HtmlControl>(guiMap, "spnWelcome", Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();  
				return TopMainMenu.WaitforLogin();
            }
        }

        public void ClickLoginLink()
        {
            Telerik.WaitForControl<HtmlControl>(guiMap, "lnkTCDLoginLink",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Wait.ForExists(20000);
            Telerik.WaitForControl<HtmlControl>(guiMap, "lnkTCDLoginLink",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void CloseLoginPopup()
        {
            Telerik.WaitForControl<HtmlButton>(guiMap, "btnTCDCloseLoginPopup",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void EnterLoginCredentials(string Username, string Password)
        {
            Telerik.WaitForControl<HtmlInputText>(guiMap, "txtTCDUsername",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Text = Username;
            Telerik.WaitForControl<HtmlInputPassword>(guiMap, "txtTCDPassword",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Text = Password;            
        }

        public void ClickLoginButton()
        {
            Telerik.WaitForControl<HtmlSpan>(guiMap, "btnLogin", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }
        
        public bool VerifyLogin(string Username,string Password)
        {
            ClickLoginLink();

            EnterLoginCredentials(Username,Password);

            ClickLoginButton();

            Telerik.ActiveBrowser.Refresh();
            Telerik.WaitForControl<HtmlControl>(guiMap, "btnTCDTopNav", Config.PageClassSettings.Default.MaxTimeoutValue).Wait.ForExists(20000);
			return IsLoggedIn;
            ////return Telerik.WaitForControl<HtmlControl>(guiMap, "spnWelcome",Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();    
        }
    }
}
