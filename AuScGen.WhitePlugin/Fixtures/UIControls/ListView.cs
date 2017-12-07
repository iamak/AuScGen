// ***********************************************************************
// <copyright file="ListView.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ListView class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class ListView
	/// </summary>
    public class ListView : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ListView"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
		public ListView(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.ListView>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the listview.
		/// </summary>
		/// <value>
		/// The listview.
		/// </value>
        protected internal TestStack.White.UIItems.ListView Listview
        {
            get
            {
                return (TestStack.White.UIItems.ListView)Control;
            }
        }  
    }
}
