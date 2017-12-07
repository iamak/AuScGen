// ***********************************************************************
// <copyright file="Button.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Button class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Button
	/// </summary>
    public class Button : BaseControl, IButton
    {

		/// <summary>
		/// Initializes a new instance of the <see cref="Button"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public Button(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.Button>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the button.
		/// </summary>
		/// <value>
		/// The button.
		/// </value>
        protected internal TestStack.White.UIItems.Button ButtonControl
        {
            get
            {
                return (TestStack.White.UIItems.Button)Control;
            }
        }       
    }
}
