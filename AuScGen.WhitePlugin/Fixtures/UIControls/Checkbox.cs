// ***********************************************************************
// <copyright file="CheckBox.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>checkBox class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Checkbox
	/// </summary>
    public class Checkbox : BaseControl , ICheckbox
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Checkbox"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public Checkbox(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.CheckBox>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the checkbox.
		/// </summary>
		/// <value>
		/// The checkbox.
		/// </value>
        protected internal TestStack.White.UIItems.CheckBox CheckBoxControl
        {
            get
            {
                return (TestStack.White.UIItems.CheckBox)Control;
            }
        }        
    }
}
