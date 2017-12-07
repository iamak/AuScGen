// ***********************************************************************
// <copyright file="GuiMapParser.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>GuiMapParser class</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;

namespace AuScGen.WhiteFramework.GUIMapParser
{
	/// <summary>
	///		Class Gui MAp Parser
	/// </summary>
	class GuiMapParser
	{
		//Identifier constants
		/// <summary>
		/// The identifier
		/// </summary>
		private const string id = "id";
		/// <summary>
		/// The text
		/// </summary>
		private const string text = "text";
		/// <summary>
		/// The classname
		/// </summary>
		private const string classname = "class";
		/// <summary>
		/// The native property
		/// </summary>
		private const string nativeProperty = "nativeProperty";
		/// <summary>
		/// The control type
		/// </summary>
		private const string controlType = "controlType";
		/// <summary>
		/// The framework
		/// </summary>
		private const string framework = "framework";
		/// <summary>
		/// The multiple
		/// </summary>
		private const string multiple = "multiple";
		/// <summary>
		/// The XML node path
		/// </summary>
		private const string xmlNodePath = "/ObjectRepository/FeatureSet";
		/// <summary>
		/// The GUI map parser
		/// </summary>
		private static GuiMapParser guiMapParser;

		/// <summary>
		/// Prevents a default instance of the <see cref="GuiMapParser"/> class from being created.
		/// </summary>
		private GuiMapParser() { }

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns></returns>
		public static GuiMapParser GetInstance()
		{
			if (guiMapParser == null)
			{
				guiMapParser = new GuiMapParser();
				return guiMapParser;
			}
			return guiMapParser;
		}

		/// <summary>
		/// Loads the GUI map.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public Dictionary<string, GuiMap> LoadGuiMap(string filePath)
		{
			XmlDocument doc = new XmlDocument();
			Dictionary<string, GuiMap> guiObjCollection = null;
			try
			{
				List<string> IdentType = new List<string>();
				List<string> Value = new List<string>();
				string logicalName = string.Empty;

				doc.Load(filePath);
				XmlNodeList rootNode = doc.DocumentElement.SelectNodes(xmlNodePath);
				//Create a dictionary object that can hold key value pairs of string and GUIMap objects
				guiObjCollection = new Dictionary<string, GuiMap>();
				GuiMap guimap = null;
				foreach (XmlNode featureSetNode in rootNode)
				{
					XmlNodeList elementNodes = featureSetNode.ChildNodes;
					foreach (XmlNode node in elementNodes)
					{
						guimap = new GuiMap();
						logicalName = node.Attributes["name"].InnerText;
						guimap.LogicalName = logicalName;
						
						if (node.ChildNodes.Count > 0)
						{
							for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
							{
								IdentType.Add(node.ChildNodes[i].Name);
								Value.Add(node.ChildNodes[i].InnerText);
								switch (IdentType[i].ToLower(CultureInfo.CurrentCulture))
								{
									case id:
										guimap.MultIdentities.Add(IdentType[i], Value[i]);
										continue;
									case text:
										guimap.MultIdentities.Add(IdentType[i], Value[i]);
										continue;
								}
							}
							break;
						}
						if (!guiObjCollection.ContainsKey(guimap.LogicalName))
						{
							guiObjCollection.Add(guimap.LogicalName, guimap);
							IdentType.Clear();
							Value.Clear();
						}
					}
				}
			}
			catch (FileNotFoundException fne)
			{
				string message = fne.Message;
				Debug.Print(message);
				throw;
			}
			catch
			{
				string message = "Exception occured while loading the values" +
					"from Gui map xml" + filePath + "not found";
				Debug.Print(message);
			}
			return guiObjCollection;
		}

	}
}
