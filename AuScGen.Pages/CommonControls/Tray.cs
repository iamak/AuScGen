// ***********************************************************************
// <copyright file="Tray.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Tray class</summary>
// ***********************************************************************
using System.Collections.ObjectModel;
using System.Globalization;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen.Pages.CommonControls
{
	/// <summary>
	///		Class Tray
	/// </summary>
	public class Tray
    {
		/// <summary>
		/// The telerik
		/// </summary>
        private TelerikPlugin.TelerikFramework telerik;
		/// <summary>
		/// The mygui map
		/// </summary>
        private string myguiMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tray"/> class.
		/// </summary>
		/// <param name="telerik">The telerik.</param>
		/// <param name="mapPath">The GUI map path.</param>
        public Tray(TelerikPlugin.TelerikFramework telerik, string mapPath)
        {
            this.telerik = telerik;
            myguiMap = string.Concat(mapPath, "CommonControls.xml");
        }

		/// <summary>
		/// The wait
		/// </summary>
        private Utils.Wait wait;
		/// <summary>
		/// Gets the wait.
		/// </summary>
		/// <value>
		/// The wait.
		/// </value>
        public Utils.Wait Wait 
        {
            get
            {
                if(null == wait)
                {
                    wait = new Utils.Wait(telerik);
                }
                return wait;
            }
        }

		/// <summary>
		/// Gets the tray button.
		/// </summary>
		/// <value>
		/// The tray button.
		/// </value>
        public HtmlButton TrayButton
        {
            get
            {
                return telerik.WaitForControl<HtmlButton>(myguiMap, "btnTrayButton",Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

		/// <summary>
		/// Gets the tray options.
		/// </summary>
		/// <value>
		/// The tray options.
		/// </value>
        private ReadOnlyCollection<HtmlControl> TrayOptions 
        { 
            get
            {
                return Wait.WaitForAction<ReadOnlyCollection<HtmlControl>>(() =>
                {
                    return telerik.Find.AllByXPath<HtmlControl>("html/body/div[1]/div[1]/ul/div[1]/div/li");
                }, Config.PageClassSettings.Default.MaxTimeoutValue);         
            }
        }

		/// <summary>
		/// Clicks the tray item.
		/// </summary>
		/// <param name="itemName">Name of the item.</param>
        public void ClickTrayItem(string itemName)
        {
            foreach(HtmlControl option in TrayOptions)
            {
                if(option.BaseElement.InnerText.ToLower(CultureInfo.CurrentCulture).Trim().Equals(itemName))
                {
                    option.DesktopMouseClick();
                }
            }
        }
    }
}
