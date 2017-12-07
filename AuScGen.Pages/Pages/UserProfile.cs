using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using ArtOfTest.WebAii.ObjectModel;
using Ecolab.CommonUtilityPlugin;
using ArtOfTest.WebAii.Win32.Dialogs;


namespace Ecolab.Pages.Pages
{
	public class UserProfile : PageBase
	{

		private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
		public UserProfile(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin) : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "UserProfile.xml");
        }

		public UserProfile(List<object> utilsList) : base(utilsList, "UserProfile.xml")
        {
			guiMap = string.Concat(GuiMapPath, "UserProfile.xml");
        }

		public HtmlControl UserRole
		{
			get
			{
				return GetHtmlControl<HtmlControl>("lblUserProfile");
			}
		}
		public HtmlControl SuccessMsg
		{
			get
			{
				return GetHtmlControl<HtmlControl>("successMsg");
			}
		}

		/// <summary>
		/// Gets Button Save for Add control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnSaveAdd
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnSave");
			}
		}

		/// <summary>
		/// Gets BUtton Cancel control
		/// </summary>
		/// <returns></returns>
		//public HtmlButton BtnCancelPassword
		//{
		//	get
		//	{
		//		return GetHtmlControl<HtmlButton>("btnCancel");
		//	}
		//}

		///// <summary>
		///// Gets Button Save for Add control
		///// </summary>
		///// <returns></returns>
		//public HtmlButton BtnSavePassword
		//{
		//	get
		//	{
		//		return GetHtmlControl<HtmlButton>("btnSaveUser");
		//	}
		//}

		/// <summary>
		/// Gets BUtton Cancel control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnCancelAdd
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnCancel");
			}
		}

		/// <summary>
		/// Gets changing password control
		/// </summary>
		/// <returns></returns>
		public HtmlControl LnkChangePwd
		{
			get
			{
				return GetHtmlControl<HtmlControl>("lnkChangePwd");
			}
		}

		/// <summary>
		/// Gets language preferred  from User Profile
		/// </summary>
		public HtmlSelect LanguagePreferred
		{
			get
			{
				return GetHtmlControl<HtmlSelect>("ddlLanguages");
			}
		}

		/// <summary>
		/// Gets select title from User Profile
		/// </summary>
		public HtmlSelect DdlTitle
		{
			get
			{
				return GetHtmlControl<HtmlSelect>("ddlTitle");
			}
		}
		
		/// <summary>
		/// Gets First Name from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtFirstName
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFirstName");
			}
		}

		/// <summary>
		/// Gets Last name from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtLastName
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtLastName");
			}
		}

		/// <summary>
		/// Gets email from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtEmail
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtEmail");
			}
		}

		/// <summary>
		/// Gets Office phone from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtOfficePhone
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtOfficePhone");
			}
		}

		/// <summary>
		/// Gets Mobile phone from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtMobilePhone
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txMobilePhone");
			}
		}

		/// <summary>
		/// Gets Fax No from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtFaxNo
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFaxNo");
			}
		}

		/// <summary>
		/// Gets the MSG from control.
		/// </summary>
		/// <returns></returns>
		public HtmlControl ConfirmDialog
		{
			get
			{
				return GetHtmlControl<HtmlControl>(".//*[@id='ConfirmDialog']/div/div/div/div[2]/span");
			}
		}


		/// <summary>
		/// Gets Button Yes control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnimbYes
		{
			get
			{
				return GetHtmlControl<HtmlButton>("imbYes");
			}
		}

		/// <summary>
		/// Gets GroupType control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnimbNo
		{
			get
			{
				return GetHtmlControl<HtmlButton>("imbNo");
			}
		}

		/// <summary>
		/// Gets Old Password from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputPassword TxtOldPassword
		{
			get
			{
				return GetHtmlControl<HtmlInputPassword>("txtOldPassword");
			}
		}

		/// <summary>
		/// Gets New Password from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputPassword TxtNewPassword
		{
			get
			{
				return GetHtmlControl<HtmlInputPassword>("txtNewPassword");
			}
		}

		/// <summary>
		/// Gets Confirm Password from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputPassword TxtConfirmPassword
		{
			get
			{
				return GetHtmlControl<HtmlInputPassword>("txtConfirmPassword");
			}
		}

		/// <summary>
		/// Gets Save User from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnSavePassword
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnSavePassword");
			}
		}

		/// <summary>
		/// Gets Cancel Usr from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnCancelPassword
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnCancelPassword");
			}
		}
	}
}
