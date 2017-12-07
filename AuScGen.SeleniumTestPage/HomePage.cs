using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
	/// <summary>
	///		Class HomePage
	/// </summary>
    public class HomePage : PageBase
    {
		/// <summary>
		/// The GUI map
		/// </summary>
        private string guiMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomePage"/> class.
		/// </summary>
		/// <param name="utilsList">The utils list.</param>
        public HomePage(List<object> utilsList)
            : base(utilsList, "HomePage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "HomePage.xml");
        }

		/// <summary>
		/// Gets the products tab.
		/// </summary>
		/// <value>
		/// The products tab.
		/// </value>
        public WebLink ProductsTab
        {
            get
            {
                return GetHtmlControl<WebLink>("LnkProducts");
            }
        }
		/// <summary>
		/// Gets a value indicating whether this instance is popup present.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is popup present; otherwise, <c>false</c>.
		/// </value>
        public bool IsPopupPresent
        {
            get
            {
                return IsPresent<WebControl>("PopupClose",3000);
            }
        }

		/// <summary>
		/// Gets the popup handle close.
		/// </summary>
		/// <value>
		/// The popup handle close.
		/// </value>
        public WebControl PopupHandleClose
        {
            get
            {
                return GetHtmlControl<WebControl>("PopupClose");
            }
        }

		/// <summary>
		/// Gets the side tab handle.
		/// </summary>
		/// <value>
		/// The side tab handle.
		/// </value>
        public WebControl SideTabHandle
        {
            get
            {
                return GetHtmlControl<WebControl>("SideTab");
            }
        }

		/// <summary>
		/// Gets the user menu.
		/// </summary>
		/// <value>
		/// The user menu.
		/// </value>
        public WebButton UserMenu
        {
            get
            {
                return GetHtmlControl<WebButton>("MenuUser");
            }
        }

		/// <summary>
		/// Gets the logout.
		/// </summary>
		/// <value>
		/// The logout.
		/// </value>
        public WebLink Logout
        {
            get
            {
                return GetHtmlControl<WebLink>("LnkLogout");
            }
        }
    }
}
