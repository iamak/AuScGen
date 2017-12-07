// ***********************************************************************
// <copyright file="RadioButton.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>RadioButton class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class RadioButton
	/// </summary>
    public class RadioButton : BaseControl , IRadioButton
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="RadioButton"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public RadioButton(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.RadioButton>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the radio button.
		/// </summary>
		/// <value>
		/// The radio button.
		/// </value>
        protected internal TestStack.White.UIItems.RadioButton RadioButtonControl
        {
            get
            {
                return (TestStack.White.UIItems.RadioButton)Control;
            }
        }
    }
}
