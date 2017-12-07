// ***********************************************************************
// <copyright file="WhiteFramework.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>WhiteFramework class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AuScGen.WhiteFramework.GUIMapParser;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class WhiteFramework
	/// </summary>
	public class WhiteFramework
	{
		/// <summary>
		/// The identifier
		/// </summary>
		private string identifier;

		/// <summary>
		/// Gets or sets the GUI map path.
		/// </summary>
		/// <value>
		/// The GUI map path.
		/// </value>
		public string MapPath { get; set; }

		/// <summary>
		/// Gets or sets the application window.
		/// </summary>
		/// <value>
		/// The application window.
		/// </value>
		public Window AppWindow { get; set; }

		/// <summary>
		/// Gets or sets the search.
		/// </summary>
		/// <value>
		/// The search.
		/// </value>
		public SearchCriteria SearchCriteria { get; set; }

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <typeparam name="T">IUIItem</typeparam>
		/// <returns></returns>
		public T GetControl<T>() where T : IUIItem
		{
			AppWindow.Focus();
			return AppWindow.Get<T>(SearchCriteria);
		}

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <typeparam name="T">IUIItem</typeparam>
		/// <param name="guiMapPath">The GUI map path.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		public T GetControl<T>(string guiMapPath, string logicalName) where T : IUIItem
		{
			AppWindow.Focus();
			Dictionary<string, GuiMap> guiCollection = guiCollection = GetObjectCollection(guiMapPath);
			GetIdentificationValues(guiCollection, logicalName);
			return AppWindow.Get<T>(SearchCriteria);
		}

		/// <summary>
		/// Extracts the object locator.
		/// </summary>
		/// <param name="objectLocator">The object locator.</param>
		/// <returns></returns>
		protected internal string ExtractObjectLocator(string objectLocator)
		{
			if (objectLocator.StartsWith("id", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 2);
				objectLocator = objectLocator.Substring(2, objectLocator.Length - 2);
			}
			else if (objectLocator.StartsWith("text", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 4);
				objectLocator = objectLocator.Substring(4, objectLocator.Length - 4);
			}
			else if (objectLocator.StartsWith("name", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 4);
				objectLocator = objectLocator.Substring(4, objectLocator.Length - 4);
			}
			else if (objectLocator.StartsWith("xpath", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 5);
				objectLocator = objectLocator.Substring(5, objectLocator.Length - 5);
			}
			else if (objectLocator.StartsWith("tagname", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 7);
				objectLocator = objectLocator.Substring(7, objectLocator.Length - 7);

			}
			else if (objectLocator.StartsWith("content", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 7);
				objectLocator = objectLocator.Substring(7, objectLocator.Length - 7);
			}
			else if (objectLocator.StartsWith("class", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 5);
				objectLocator = objectLocator.Substring(5, objectLocator.Length - 5);
			}
			else if (objectLocator.StartsWith("atribute", StringComparison.CurrentCultureIgnoreCase))
			{
				identifier = objectLocator.Substring(0, 8);
				objectLocator = objectLocator.Substring(8, objectLocator.Length - 8);
			}
			return objectLocator;
		}

		/// <summary>
		/// Gets the search.
		/// </summary>
		/// <param name="strObjectLocator">The string object locator.</param>
		protected internal void GetSearch(string strObjectLocator)
		{
			strObjectLocator = ExtractObjectLocator(strObjectLocator);
			switch (FindType(identifier))
			{
				case FindBy.Text:
					SearchCriteria = SearchCriteria.ByText(strObjectLocator);
					break;

				case FindBy.AutomationId:
					SearchCriteria = SearchCriteria.ByAutomationId(strObjectLocator);
					break;

				case FindBy.ClassName:
					SearchCriteria = SearchCriteria.ByClassName(strObjectLocator);
					break;

				case FindBy.ControlType:
					SearchCriteria = SearchCriteria.ByControlType(GetControlType(strObjectLocator));
					break;

				case FindBy.FrameworkId:
					SearchCriteria = SearchCriteria.ByFramework(strObjectLocator);
					break;

				case FindBy.NativeProperty:
					//Search = SearchCriteria.ByNativeProperty()
					break;

				default:
					SearchCriteria = SearchCriteria.ByText(strObjectLocator);
					break;
			}
		}

		/// <summary>
		/// Finds the type.
		/// </summary>
		/// <param name="strLocator">The string locator.</param>
		/// <returns></returns>
		private FindBy FindType(string strLocator)
		{
			switch (strLocator)
			{
				case "text":
					return FindBy.Text;

				case "id":
					return FindBy.AutomationId;

				default:
					return FindBy.AutomationId;

			}
		}

		/// <summary>
		/// Gets the type of the control.
		/// </summary>
		/// <param name="Type">The type.</param>
		/// <returns></returns>
		private System.Windows.Automation.ControlType GetControlType(string Type)
		{
			switch (Type.ToLower(CultureInfo.CurrentCulture))
			{
				case "button":
					return System.Windows.Automation.ControlType.Button;
				case "editbox":
					return System.Windows.Automation.ControlType.Edit;
				case "custom":
					return System.Windows.Automation.ControlType.Custom;
			}
			return null;
		}

		/// <summary>
		/// Gets the object collection.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		private Dictionary<string, GuiMap> GetObjectCollection(string filePath)
		{
			string originalFilePath = filePath;
			Dictionary<string, Dictionary<string, GuiMap>> collection = new Dictionary<string, Dictionary<string, GuiMap>>();
			string fileName = Path.GetFileName(filePath);
			fileName = fileName.Substring(0, fileName.Length - 4);
			switch (fileName)
			{
				case "GuiMap":
					collection = GlobalGuiCollection.GetInstance().GlobalGuimapCollection;
					if (collection != null && collection.ContainsKey(fileName))
					{
						collection = GlobalGuiCollection.GetInstance().GlobalGuimapCollection;
					}
					else
					{
						GlobalGuiCollection.GetInstance().GlobalGuimapCollection
							.Add(fileName, GuiMapParser.GetInstance().LoadGuiMap(originalFilePath));
						collection = GlobalGuiCollection.GetInstance().GlobalGuimapCollection;
					}
					break;
			}
			return collection[fileName];
		}

		/// <summary>
		/// Extracts the locator.
		/// </summary>
		/// <param name="objectLocator">The object locator.</param>
		/// <returns></returns>
		private string ExtractLocator(Dictionary<string, string> objectLocator)
		{
			string LocatorValue = string.Empty;
			if (objectLocator.Keys.Contains("Id"))
			{
				identifier = "Id";
				LocatorValue = objectLocator["Id"];
			}
			if (objectLocator.Keys.Contains("Text"))
			{
				identifier = "Text";
				LocatorValue = objectLocator["Text"];
			}
			if (objectLocator.Keys.Contains("ClassName"))
			{
				identifier = "ClassName";
				LocatorValue = objectLocator["ClassName"];
			}
			if (objectLocator.Keys.Contains("Framework"))
			{
				identifier = "Framework";
				LocatorValue = objectLocator["Framework"];
			}
			if (objectLocator.Keys.Contains("NativeProperty"))
			{
				identifier = "NativeProperty";
				LocatorValue = objectLocator["NativeProperty"];
			}
			if (objectLocator.Keys.Contains("ControlType"))
			{
				identifier = "ControlType";
				LocatorValue = objectLocator["ControlType"];
			}
			return LocatorValue;
		}

		/// <summary>
		/// Gets the identification values.
		/// </summary>
		/// <param name="guiMapCollection">The g map collection.</param>
		/// <param name="logicalName">Name of the logical.</param>
		private void GetIdentificationValues(Dictionary<string, GuiMap> guiMapCollection, string logicalName)
		{
			GuiMap guiMap = null;
			Dictionary<string, string> Identites = new Dictionary<string, string>();
			string LocatorValue = string.Empty;
			if (guiMapCollection.ContainsKey(logicalName))
			{
				guiMap = guiMapCollection[logicalName];
			}
			Identites = guiMap.MultIdentities;
			LocatorValue = ExtractLocator(Identites);
			switch (FindType(identifier.ToLower(CultureInfo.CurrentCulture)))
			{
				case FindBy.Text:
					SearchCriteria = SearchCriteria.ByText(LocatorValue);
					break;

				case FindBy.AutomationId:
					SearchCriteria = SearchCriteria.ByAutomationId(LocatorValue);
					break;
				case FindBy.ClassName:
					SearchCriteria = SearchCriteria.ByClassName(LocatorValue);
					break;

				case FindBy.ControlType:
					SearchCriteria = SearchCriteria.ByControlType(GetControlType(LocatorValue));
					break;

				case FindBy.FrameworkId:
					//Search = SearchCriteria.ByFramework(LocatorValue);
					break;

				case FindBy.NativeProperty:
					//Search = SearchCriteria.ByNativeProperty()
					break;

				default:
					SearchCriteria = SearchCriteria.ByText(LocatorValue);
					break;
			}
			Identites.Remove(identifier);
			foreach (KeyValuePair<string, string> myKeyValues in Identites)
			{
				LocatorValue = myKeyValues.Key;
				switch (FindType(LocatorValue.ToLower(CultureInfo.CurrentCulture)))
				{
					case FindBy.Text:
						SearchCriteria = SearchCriteria.AndByText(myKeyValues.Value);
						break;

					case FindBy.AutomationId:
						SearchCriteria = SearchCriteria.AndAutomationId(myKeyValues.Value);
						break;

					case FindBy.ClassName:
						SearchCriteria = SearchCriteria.AndByClassName(myKeyValues.Value);
						break;

					case FindBy.ControlType:
						SearchCriteria = SearchCriteria.AndControlType(GetControlType(myKeyValues.Value));
						break;

					case FindBy.FrameworkId:
						//Search = Search.AndOfFramework((WindowsFramework)myKeyValues.Value);
						break;

					case FindBy.NativeProperty:
						//Search = SearchCriteria.ByNativeProperty()
						break;

					default:
						SearchCriteria = SearchCriteria.ByText(myKeyValues.Value);
						break;
				}
			}
		}
	}
}
