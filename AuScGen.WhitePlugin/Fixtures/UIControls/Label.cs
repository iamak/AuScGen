// ***********************************************************************
// <copyright file="Label.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Label class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Label
	/// </summary>
    public class Label : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Label"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public Label(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.Label>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;
        }

		/// <summary>
		/// Gets the label.
		/// </summary>
		/// <value>
		/// The label.
		/// </value>
        protected TestStack.White.UIItems.Label LabelControl
        {
            get
            {
                return (TestStack.White.UIItems.Label)Control;
            }
        }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
        public string Text 
        { 
            get
            {
                return this.LabelControl.Text;
            }
        }
    }
}
