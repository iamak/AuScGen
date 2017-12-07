// ***********************************************************************
// <copyright file="AccntBalancePage.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>AccntBalancePage class</summary>
// ***********************************************************************
using System.Collections.Generic;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen.Pages
{
    /// <summary>
    ///		Class AccntBalancePage
    /// </summary>
    public class AccntBalancePage : PageBase
    {
		/// <summary>
		/// The GUI map
		/// </summary>
        private string guiMap;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccntBalancePage"/> class.
		/// </summary>
		/// <param name="utilitiesList">The utils list.</param>
        public AccntBalancePage(IList<object> utilitiesList)
            : base(utilitiesList, "AccntBalancePage.xml")
        {
            this.guiMap = string.Concat(AccntBalancePage.MapPath, "AccntBalancePage.xml");
        }

		/// <summary>
		/// Gets the account balance tab.
		/// </summary>
		/// <value>
		/// The account balance tab.
		/// </value>
        public HtmlControl AccountBalanceTab
        {
            get
            {
                return this.GetHtmlControl<HtmlControl>("AccountBalanceTab");
            }
        }

		/// <summary>
		/// Gets a value indicating whether [accnt balance tab present].
		/// </summary>
		/// <value>
		/// <c>true</c> if [accnt balance tab present]; otherwise, <c>false</c>.
		/// </value>
        public bool AccountBalanceTabPresent 
        { 
            get
            {
                return Wait.WaitforAction(() =>
                {

                    return AccountBalanceTab.BaseElement.InnerText.Contains("Account Balance");
                }, Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }
    }
}
