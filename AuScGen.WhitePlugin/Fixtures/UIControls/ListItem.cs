// ***********************************************************************
// <copyright file="ListItem.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ListItem class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class ListItem
	/// </summary>
    public class ListItem : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ListItem"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public ListItem(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.ListBoxItems.ListItem>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the listview.
		/// </summary>
		/// <value>
		/// The listview.
		/// </value>
        protected internal TestStack.White.UIItems.ListBoxItems.ListItem Listitem
        {
            get
            {
                return (TestStack.White.UIItems.ListBoxItems.ListItem)Control;
            }
        }  
    }
}
