// ***********************************************************************
// <copyright file="Image.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Image class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Image
	/// </summary>
    class Image : BaseControl , IControl
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Image"/> class.
		/// </summary>
		/// <param name="GuiMap">The GUI map.</param>
		/// <param name="LogicalName">Name of the logical.</param>
		/// <param name="ControlAccess">The control access.</param>
        public Image(string GuiMap, string LogicalName, ControlAccess ControlAccess)
            : base(GuiMap, LogicalName)
        {
            this.MyControlAccess = ControlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.Image>(this.MapPath, LogicalName);
            this.Control = MyControlAccess.UIControl;

        }

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
        private TestStack.White.UIItems.Image ImageControl
        {
            get
            {
                return (TestStack.White.UIItems.Image)Control;
            }
        }
    }
}
