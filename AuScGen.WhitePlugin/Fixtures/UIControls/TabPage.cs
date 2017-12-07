// ***********************************************************************
// <copyright file="TabPage.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TabPage class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.UIItems.Actions;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class TabPage
	/// </summary>
    public class TabPage : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="TabPage"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="ControlAccess">The control access.</param>
        internal TabPage(TestStack.White.UIItems.TabItems.TabPage control, ControlAccess ControlAccess)
        {
            this.Control = control;
            this.MyControlAccess = ControlAccess;
        }

		/// <summary>
		/// Gets the tab page.
		/// </summary>
		/// <value>
		/// The tab page.
		/// </value>
        private TestStack.White.UIItems.TabItems.TabPage Tabpage
        {
            get
            {
                return (TestStack.White.UIItems.TabItems.TabPage)Control;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is selected.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
        public bool IsSelected 
        { 
            get
            {
                return this.Tabpage.IsSelected;
            }
        }

		/// <summary>
		/// Selects this instance.
		/// </summary>
        public void Select()
        {
            this.Tabpage.Select();
        }
    }
}
