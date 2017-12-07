// ***********************************************************************
// <copyright file="Tab.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Tab class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.TabItems;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Tab
	/// </summary>
	public class Tab : BaseControl, IControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tab"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
		public Tab(string map, string logicalName, ControlAccess controlAccess)
			: base(map, logicalName)
		{
			this.MyControlAccess = controlAccess;
			MyControlAccess.InitializeControl<TestStack.White.UIItems.TabItems.Tab>(this.MapPath, logicalName);
			this.Control = MyControlAccess.UIControl;

		}

		/// <summary>
		/// Gets the tab.
		/// </summary>
		/// <value>
		/// The tab.
		/// </value>
		private TestStack.White.UIItems.TabItems.Tab TabControl
		{
			get
			{
				return (TestStack.White.UIItems.TabItems.Tab)Control;
			}
		}

		/// <summary>
		/// Pageses this instance.
		/// </summary>
		/// <returns></returns>
		public IList<TabPage> Pages()
		{
			TabPages pages = this.TabControl.Pages;
			List<TabPage> frameWorkPages = new List<TabPage>();

			foreach (TestStack.White.UIItems.TabItems.TabPage page in pages)
			{
				frameWorkPages.Add(new TabPage(page, MyControlAccess));
			}
			return frameWorkPages;
		}
	}
}
