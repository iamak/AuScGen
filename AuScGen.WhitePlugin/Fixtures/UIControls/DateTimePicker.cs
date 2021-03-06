﻿// ***********************************************************************
// <copyright file="DateTimePicker.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>DateTimePicker class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class DateTimePicker
	/// </summary>
    public class DateTimePicker : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimePicker"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public DateTimePicker(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.DateTimePicker>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;
        }

		/// <summary>
		/// Gets the datetimepicker.
		/// </summary>
		/// <value>
		/// The datetimepicker.
		/// </value>
        protected internal TestStack.White.UIItems.DateTimePicker Datetimepicker
        {
            get
            {
                return (TestStack.White.UIItems.DateTimePicker)Control;
            }
        }      
    }
}
