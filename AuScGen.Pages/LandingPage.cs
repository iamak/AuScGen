// ***********************************************************************
// <copyright file="LandingPage.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>LandingPage class</summary>
// ***********************************************************************
using System.Collections.Generic;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen.Pages
{
	/// <summary>
	///		Class Landing Page
	/// </summary>
	public class LandingPage : PageBase
	{
		/// <summary>
		/// The GUI map
		/// </summary>
		private string guiMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="LandingPage"/> class.
		/// </summary>
		/// <param name="utilitiesList">The utils list.</param>
		public LandingPage(IList<object> utilitiesList)
			: base(utilitiesList, "LandingPage.xml")
		{
			this.guiMap = string.Concat(LandingPage.MapPath, "LoginPage.xml");

		}

		/// <summary>
		/// Gets the main tray.
		/// </summary>
		/// <value>
		/// The main tray.
		/// </value>
		public HtmlControl MainTray
		{
			get
			{
				return this.GetHtmlControl<HtmlControl>("MainTray");
			}
		}

		/// <summary>
		/// The main tray button
		/// </summary>
		private CommonControls.Tray mainTrayButton;
		/// <summary>
		/// Gets the tray.
		/// </summary>
		/// <value>
		/// The tray.
		/// </value>
		public CommonControls.Tray Tray
		{
			get
			{
				if (null == this.mainTrayButton)
				{
					this.mainTrayButton = new CommonControls.Tray(Telerik, MapPath);
				}
				return this.mainTrayButton;
			}
		}
	}
}
