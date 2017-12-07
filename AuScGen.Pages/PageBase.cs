// ***********************************************************************
// <copyright file="PageBase.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>PageBase class</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ArtOfTest.WebAii.Controls;

namespace AuScGen.Pages
{
	/// <summary>
	///		Class Page Base
	/// </summary>
	public class PageBase
	{
		/// <summary>
		/// The complete GUI map path
		/// </summary>
		private string completeGuiMapPath;

		/// <summary>
		/// The telerik
		/// </summary>
		private AuScGen.TelerikPlugin.TelerikFramework telerik;
		/// <summary>
		/// Gets the telerik.
		/// </summary>
		/// <value>
		/// The telerik.
		/// </value>
		protected AuScGen.TelerikPlugin.TelerikFramework Telerik
		{
			get
			{
				return telerik;
			}
		}

		/// <summary>
		/// The d b access
		/// </summary>
		private AuScGen.CommonUtilityPlugin.DataAccess databaseAccess;
		/// <summary>
		/// Gets the database access.
		/// </summary>
		/// <value>
		/// The database access.
		/// </value>
		protected AuScGen.CommonUtilityPlugin.DataAccess DBAccess
		{
			get
			{
				return databaseAccess;
			}
		}

		/// <summary>
		/// The wait
		/// </summary>
		private Utils.Wait wait;
		/// <summary>
		/// Gets the wait.
		/// </summary>
		/// <value>
		/// The wait.
		/// </value>
		public Utils.Wait Wait
		{
			get
			{
				return new Utils.Wait(Telerik);
			}
		}

		/// <summary>
		/// The keyboard simulator
		/// </summary>
		private AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator keyboardSimulator;
		/// <summary>
		/// Gets the key board simulator.
		/// </summary>
		/// <value>
		/// The key board simulator.
		/// </value>
		protected AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator KeyboardSimulator
		{
			get
			{
				return keyboardSimulator;
			}
		}

		/// <summary>
		/// Gets the GUI map path.
		/// </summary>
		/// <value>
		/// The GUI map path.
		/// </value>
		protected static string MapPath
		{
			get
			{
				return Directory.GetCurrentDirectory() + @"\GuiMaps\";
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="telerikPlugin">The telerik plugin.</param>
		public PageBase(AuScGen.TelerikPlugin.TelerikFramework telerikPlugin)
		{
			telerik = telerikPlugin;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="telerikPlugin">The telerik plugin.</param>
		/// <param name="guiMapName">Name of the GUI map.</param>
		public PageBase(AuScGen.TelerikPlugin.TelerikFramework telerikPlugin, string guiMapName)
			: this(telerikPlugin)
		{
			completeGuiMapPath = string.Concat(MapPath, guiMapName);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="utilities">The utils.</param>
		public PageBase(IList<object> utilities)
		{
			foreach (object util in utilities)
			{
				if (util.GetType() == typeof(AuScGen.TelerikPlugin.TelerikFramework))
				{
					telerik = (AuScGen.TelerikPlugin.TelerikFramework)util;
				}

				if (util.GetType() == typeof(AuScGen.CommonUtilityPlugin.DataAccess))
				{
					databaseAccess = (AuScGen.CommonUtilityPlugin.DataAccess)util;
				}

				if (util.GetType() == typeof(AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator))
				{
					keyboardSimulator = (AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator)util;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="utilities">The utils.</param>
		/// <param name="guiMapName">Name of the GUI map.</param>
		public PageBase(IList<object> utilities, string guiMapName)
			: this(utilities)
		{
			completeGuiMapPath = string.Concat(MapPath, guiMapName);
		}

		/// <summary>
		/// Determines whether the specified logical name is present.
		/// </summary>
		/// <typeparam name="T">Control</typeparam>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		public bool IsPresent<T>(string logicalName) where T : Control, new()
		{
			Thread.Sleep(3000);
			if (null == Telerik.GetControl<T>(completeGuiMapPath, logicalName))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Gets the HTML control.
		/// </summary>
		/// <typeparam name="T">The generic object</typeparam>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		/// <exception cref="GUIException">Element not found on the Screen</exception>
		public T GetHtmlControl<T>(string map, string logicalName) where T : Control, new()
		{
			T Ctrl = null;

			Ctrl = Telerik.WaitForControl<T>(map, logicalName, Config.PageClassSettings.Default.MaxTimeoutValue);
			if (Ctrl == null)
			{
				throw new GUIException(logicalName, "Element not found on the Screen");
			}
			return Ctrl;
		}

		/// <summary>
		/// Gets the HTML control.
		/// </summary>
		/// <typeparam name="T">Control</typeparam>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		public T GetHtmlControl<T>(string logicalName) where T : Control, new()
		{
			return GetHtmlControl<T>(completeGuiMapPath, logicalName);
		}

	}
}
