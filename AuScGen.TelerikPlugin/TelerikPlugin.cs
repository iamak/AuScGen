// ***********************************************************************
// <copyright file="TelerikPlugin.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TelerikPlugin class</summary>
// ***********************************************************************
using System.ComponentModel.Composition;
using Framework;

namespace AuScGen.TelerikPlugin
{
	/// <summary>
	///		Class TelerikPlugin
	/// </summary>
    [Export(typeof(IPlugin))]
    public class TelerikPlugin : IPlugin
    {
		/// <summary>
		/// The telerik framework
		/// </summary>
        private TelerikFramework telerikFramework;
		/// <summary>
		/// Gets the telerik framework.
		/// </summary>
		/// <value>
		/// The telerik framework.
		/// </value>
        public TelerikFramework TelerikFramework 
        { 
            get
            {
                if (null == telerikFramework)
                {
                    telerikFramework = new TelerikFramework();
                }
                return telerikFramework;
            }
        }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
        public string Description
        {
            get
            {
                return "Telerik Plugin";
            }
            set
            {
                Description = value;
            }
        }
    }
}
