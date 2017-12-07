using System.Collections.Generic;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    /// <summary>
    ///		Class LoginPage
    /// </summary>
    public class LoginPage : PageBase
    {
		/// <summary>
		/// The GUI map
		/// </summary>
        private string guiMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginPage"/> class.
		/// </summary>
		/// <param name="utilsList">The utils list.</param>
        public LoginPage(List<object> utilsList)
            : base(utilsList, "LoginPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "LoginPage.xml");
        }

        //public WebEditBox Username
        //{ 
        //    get
        //    {
        //        return GetHtmlControl<WebEditBox>("TxtUserName");
        //    }
        //}

        //public WebEditBox Password
        //{
        //    get
        //    {
        //        return GetHtmlControl<WebEditBox>("TxtPassword");
        //    }
        //}

        //public WebButton LoginButton
        //{
        //    get
        //    {
        //        return GetHtmlControl<WebButton>("BtnLogin");
        //    }
        //}
        //public WebControl ITChoice
        //{
        //    get
        //    {
        //        return GetHtmlControl<WebControl>("ITChoice");
        //    }
        //}

        //public bool IsChoicePresent
        //{
        //    get
        //    {
        //        return IsPresent<WebButton>("ITChoice", 3000);
        //    }
        //}

		/// <summary>
		/// Gets the log in page link.
		/// </summary>
		/// <value>
		/// The log in page link.
		/// </value>
        public WebButton LogInPageLink
        {
            get
            {
                return GetHtmlControl<WebButton>("loginPageLink");
            }
        }

		/// <summary>
		/// Gets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
        public WebEditBox Username
        {
            get
            {
                return GetHtmlControl<WebEditBox>("userName");
            }
        }

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
        public WebEditBox Password
        {
            get
            {
                return GetHtmlControl<WebEditBox>("password");
            }
        }

		/// <summary>
		/// Gets the log in button.
		/// </summary>
		/// <value>
		/// The log in button.
		/// </value>
        public WebButton LogInButton
        {
            get
            {
                return GetHtmlControl<WebButton>("logInButton");
            }
        }


		/// <summary>
		/// Gets it choice.
		/// </summary>
		/// <value>
		/// It choice.
		/// </value>
        public WebControl ITChoice
        {
            get
            {
                return GetHtmlControl<WebControl>("SearchBox");
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is choice present.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is choice present; otherwise, <c>false</c>.
		/// </value>
        public bool IsChoicePresent
        {
            get
            {
                return IsPresent<WebLabel>("SearchBox", 3000);
            }
        }

    }
}
