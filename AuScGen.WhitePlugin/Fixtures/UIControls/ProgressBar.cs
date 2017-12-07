// ***********************************************************************
// <copyright file="ProgressBar.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ProgressBar class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class ProgressBar
	/// </summary>
    public class ProgressBar : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressBar"/> class.
		/// </summary>
        /// <param name="map">The GUI map.</param>
        /// <param name="logicalName">Name of the logical.</param>
        /// <param name="controlAccess">The control access.</param>
        public ProgressBar(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.ProgressBar>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the progressbar.
		/// </summary>
		/// <value>
		/// The progressbar.
		/// </value>
        protected internal TestStack.White.UIItems.ProgressBar Progressbar
        {
            get
            {
                return (TestStack.White.UIItems.ProgressBar)Control;
            }
        }      
    }
}
