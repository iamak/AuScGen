// ***********************************************************************
// <copyright file="LoginPage.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>LoginPage class</summary>
// ***********************************************************************
using System.Collections.Generic;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen.Pages
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
		/// <param name="utilitiesList">The utils list.</param>
        public LoginPage(IList<object> utilitiesList)
            : base(utilitiesList, "LoginPage.xml")
        {
            this.guiMap = string.Concat(LoginPage.MapPath, "LoginPage.xml");
        }

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
        public HtmlControl UserName
        {
            get
            {
                return this.GetHtmlControl<HtmlControl>("txtUserName");
            }
        }

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
        public HtmlInputPassword Password
        {
            get
            {
                return this.GetHtmlControl<HtmlInputPassword>("txtPassword");
            }
        }

		/// <summary>
		/// Gets the login button.
		/// </summary>
		/// <value>
		/// The login button.
		/// </value>
        public HtmlControl LoginButton
        {
            get
            {
                return this.GetHtmlControl<HtmlControl>("btnLogin");
            }
        }
    }
}
