// ***********************************************************************
// <copyright file="Hyperlink.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>HyperLink class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.Recording;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Hyperlink
	/// </summary>
    public class Hyperlink : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Hyperlink"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public Hyperlink(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.Hyperlink>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;
        }

		/// <summary>
		/// Gets the hyperlink.
		/// </summary>
		/// <value>
		/// The hyperlink.
		/// </value>
        protected internal TestStack.White.UIItems.Hyperlink HyperlinkControl
        {
            get
            {
                return (TestStack.White.UIItems.Hyperlink)Control;
            }
        }
    }
}
