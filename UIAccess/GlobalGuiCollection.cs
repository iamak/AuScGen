// ***********************************************************************
// <copyright file="GlobalGuiCollection.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>GlobalGuiCollection class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIAccess
{
	/// <summary>
	///     Class GlobalGuiCollection
	/// </summary>
	public class GlobalGuiCollection
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// The global page collection
		/// </summary>
		private static Dictionary<string, Dictionary<string, Guimap>> globalPageCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalGuiCollection"/> class.
		/// </summary>
		public GlobalGuiCollection()
		{
			log4net.ThreadContext.Properties["myContext"] = "Logging from GlobalGuiCollection Class";
			Logger.Debug("Inside GlobalGuiCollection Constructor!");
		}

		/// <summary>
		/// Gets the global page collection.
		/// </summary>
		/// <value>
		/// The global page collection.
		/// </value>
		public static Dictionary<string, Dictionary<string, Guimap>> GlobalPageCollection
		{
			get
			{
				if (null == globalPageCollection)
				{
					globalPageCollection = new Dictionary<string, Dictionary<string, Guimap>>();
				}
				return globalPageCollection;
			}
		}

		/// <summary>
		/// Gets the guimap.
		/// </summary>
		/// <param name="filePath">The filepath.</param>
		/// <returns></returns>
		public static Dictionary<string, Guimap> GetGraphicalUserInterfaceMap(string filePath)
		{
			string originalFilePath = filePath;
			Dictionary<string, Dictionary<string, Guimap>> collection = null;
			string filename = Path.GetFileName(filePath);
			filename = filename.Substring(0, filename.Length - 4);
			Logger.Debug(string.Concat("Checking for ", filename, " Object Collection"));

			collection = GlobalPageCollection;

			if (collection != null && collection.ContainsKey(filename))
			{
				Logger.Debug(string.Concat("Looks like there is an already existing ",
					filename, " Object Collection"));

				collection = globalPageCollection;
				//if (!collection.ContainsKey(filename))
				//{
				//    Logger.Info("oooooohhhhhhh, i am hit");
				//    AddNewGuiMap(filename, originalFilePath);
				//}
				collection[filename].FirstOrDefault().Value.LastUsedTime = DateTime.Now;
			}
			else
			{
				AddNewGuiMap(filename, originalFilePath);
			}

			QueueCleanup();
			return collection[filename];
		}

		/// <summary>
		/// Adds the new GUI map.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="filepath">The filepath.</param>
		private static void AddNewGuiMap(string filename, string filepath)
		{
			LogCheckForCollection(filename);
			lock (globalPageCollection)
			{
				GlobalPageCollection.Add(filename, GuiMapParser.GetInstance.LoadGraphicalUserInterfaceMap(filepath));
				Logger.Debug(string.Concat("Successfully Created ", filename, " Object Collection!"));
			}
		}

		/// <summary>
		/// Queues the cleanup.
		/// </summary>
		private static void QueueCleanup()
		{
			ThreadPool.QueueUserWorkItem(Cleanup);
		}

		/// <summary>
		/// Cleanups the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		private static void Cleanup(object value)
		{
			Dictionary<string, Dictionary<string, Guimap>> temp = globalPageCollection;

			lock (globalPageCollection)
			{
				if (globalPageCollection != null && globalPageCollection.Count > 0)
				{
					foreach (KeyValuePair<string, Dictionary<string, Guimap>> guiMap in globalPageCollection)
					{
						if (((TimeSpan)(DateTime.Now - guiMap.Value.Values.FirstOrDefault().LastUsedTime)).Minutes > 5)
						{
							temp.Remove(guiMap.Key);
						}
					}
				}

				globalPageCollection = temp;
				Logger.Info(string.Format("Unused guimaps collected, number of guimaps present now:{0}", globalPageCollection.Count));
			}
		}

		/// <summary>
		/// Logs the check for collection.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		private static void LogCheckForCollection(string fileName)
		{
			Logger.Debug(string.Concat("There is no existing ", fileName, " Object Collection"));
			Logger.Debug(string.Concat("Creating a new instance for ", fileName, " Object Collection"));
		}

	}
}
