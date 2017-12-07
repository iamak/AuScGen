// ***********************************************************************
// <copyright file="ImagingPlugin.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ImagingPlugin class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace AuScGen.Imaging
{
	/// <summary>
	///		Class ImagingPlugin
	/// </summary>
    [Export(typeof(IPlugin))]
    class ImagingPlugin : IPlugin
    {
		/// <summary>
		/// Gets the image processor.
		/// </summary>
		/// <value>
		/// The image processor.
		/// </value>
        protected static internal ImageProcessor ImageProcessor 
        { 
            get
            {
                return new ImageProcessor();
            }
        }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		/// <exception cref="System.NotImplementedException">
		/// </exception>
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
