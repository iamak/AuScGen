// ***********************************************************************
// <copyright file="GlobalGuiCollection.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>GlobalGuiCollection class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework.GUIMapParser
{
	/// <summary>
	///		Class GlobalGuiCollection
	/// </summary>
    class GlobalGuiCollection
    {
		/// <summary>
		/// The global guimap collection
		/// </summary>`
        private Dictionary<string, Dictionary<string, GuiMap>>
           globalGuimapCollection = new Dictionary<string, Dictionary<string, GuiMap>>();

		/// <summary>
		/// Gets or sets the global guimap collection.
		/// </summary>
		/// <value>
		/// The global guimap collection.
		/// </value>
        internal Dictionary<string, Dictionary<string, GuiMap>> GlobalGuimapCollection
        {
            get { return globalGuimapCollection; }
            //set { globalGuimapCollection = value; }
        }

		/// <summary>
		/// The global collection
		/// </summary>
        private static GlobalGuiCollection globalCollection;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns></returns>
        public static GlobalGuiCollection GetInstance()
        {
            if (null == globalCollection)
            {
                globalCollection = new GlobalGuiCollection();
                return globalCollection;
            }
            return globalCollection;
        }
    }
}
