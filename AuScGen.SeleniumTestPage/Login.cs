using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    public class Login : PageBase
    {
        private string guiMap;

        public Login(List<object> utilsList)
            : base(utilsList, "LoginPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "LoginPage.xml");
        }

        public WebEditBox Username
        { 
            get
            {
                return GetHtmlControl<WebEditBox>("TxtUserName");
            }
        }

        public WebEditBox Password
        {
            get
            {
                return GetHtmlControl<WebEditBox>("TxtPassword");
            }
        }

        public WebButton LoginButton
        {
            get
            {
                return GetHtmlControl<WebButton>("BtnLogin");
            }
        }
    }
}
