// ***********************************************************************
// <copyright file="ControlAccess.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ControlAccess class</summary>
// ***********************************************************************
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Control Access
	/// </summary>
	public class ControlAccess
    {
		/// <summary>
		/// a framework
		/// </summary>
        private WhiteFramework whiteFramework;
		/// <summary>
		/// Gets the framework.
		/// </summary>
		/// <value>
		/// The framework.
		/// </value>
        public WhiteFramework Framework 
        { 
            get
            {
                if (null == whiteFramework)
                {
                    whiteFramework = new WhiteFramework();
                }
                return whiteFramework;
            }
        }

		/// <summary>
		/// Gets or sets the name of the process.
		/// </summary>
		/// <value>
		/// The name of the process.
		/// </value>
        public string ProcessName { get; set; }
		/// <summary>
		/// Gets or sets the name of the application window.
		/// </summary>
		/// <value>
		/// The name of the application window.
		/// </value>
        public string AppWindowName { get; set; }

		/// <summary>
		/// Gets or sets the UI control.
		/// </summary>
		/// <value>
		/// The UI control.
		/// </value>
        internal UIItem UIControl { get; set; }

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
        public IList<UIItem> Children 
        { 
            get
            {
                return Framework.AppWindow.ItemsWithin(UIControl);
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlAccess"/> class.
		/// </summary>
        public ControlAccess()
        { }

		/// <summary>
		/// Initializes the application window.
		/// </summary>
        public void InitializeAppWindow()
        {
            Application application = Application.Attach(ProcessName);
            Framework.AppWindow = application.GetWindow(AppWindowName, InitializeOption.NoCache);
        }

		/// <summary>
		/// Intitializes the control.
		/// </summary>
        /// <typeparam name="T">UIItem</typeparam>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
        public void InitializeControl<T>(string map,string logicalName) where T : UIItem
        {  
            UIControl = Framework.GetControl<T>(map, logicalName);            
        }        
    }
}
