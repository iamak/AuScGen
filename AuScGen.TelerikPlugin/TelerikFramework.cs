// ***********************************************************************
// <copyright file="TelerikFramework.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TelerikFramework class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using ArtOfTest.WebAii.Controls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;

namespace AuScGen.TelerikPlugin
{
    /// <summary>
    /// Teleframework class
    /// </summary>
    public class TelerikFramework : BaseTest
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private string identifier;

        /// <summary>
        /// The logger
        /// </summary>
        private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="TelerikFramework" /> class.
        /// </summary>
        public TelerikFramework()
        {
            log4net.ThreadContext.Properties["myContext"] = "Logging from TelerikFramework Class";
            Logger.Debug("Inside TelerikFramework Constructor!");
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Element GetControl(string id)
        {
            Logger.Debug("Inside GetControl Method..");
            return Find.ById(id);
        }

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <typeparam name="T">The generic object</typeparam>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public T GetControl<T>(string id) where T : Control, new()
        {
            Logger.Debug(string.Format("Inside GetControl<T> Method", typeof(T).ToString()));
            return Find.ById<T>(id);
        }

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <typeparam name="T">The generic object</typeparam>
		/// <param name="mapPath">The map path.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		public T GetControl<T>(string mapPath, string logicalName) where T : Control, new()
        {
            Logger.Debug(string.Format("Inside GetControl<T> Method", typeof(T).ToString()));
            Dictionary<string, Guimap> guiCollection = guiCollection = GetObjectCollection(mapPath);
            return this.GetControlFromObjectLocator<T>(GuiMapParser.GetInstance.GetElementValue(guiCollection, logicalName));
        }

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <param name="mapPath">The map path.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
		public Element GetControl(string mapPath, string logicalName)
        {
            Logger.Debug("Inside GetControl Method..");
            Dictionary<string, Guimap> guiCollection = GetObjectCollection(mapPath);
            return this.GetElementFromObjectLocator(GuiMapParser.GetInstance.GetElementValue(guiCollection, logicalName));
        }

        /// <summary>
        /// Shuts down.
        /// </summary>
        public void Shutdown()
        {
            BaseTest.ShutDown();
        }

        /// <summary>
        /// Extracts the object locator.
        /// </summary>
        /// <param name="objectLocator">The object locator.</param>
        /// <returns></returns>
        private string ExtractObjectLocator(string objectLocator)
        {
            Logger.Debug("Inside ExtractObjectLocator");

            if (objectLocator.StartsWith("id", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 2);
                objectLocator = objectLocator.Substring(2, objectLocator.Length - 2);
            }
            else if (objectLocator.StartsWith("name", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 4);
                objectLocator = objectLocator.Substring(4, objectLocator.Length - 4);
            }
            else if (objectLocator.StartsWith("xpath", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 5);
                objectLocator = objectLocator.Substring(5, objectLocator.Length - 5);
            }
            else if (objectLocator.StartsWith("tagname", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 7);
                objectLocator = objectLocator.Substring(7, objectLocator.Length - 7);

            }
            else if (objectLocator.StartsWith("content", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 7);
                objectLocator = objectLocator.Substring(7, objectLocator.Length - 7);
            }
            else if (objectLocator.StartsWith("class", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 5);
                objectLocator = objectLocator.Substring(5, objectLocator.Length - 5);
            }
            else if (objectLocator.StartsWith("atribute", StringComparison.CurrentCultureIgnoreCase))
            {
                this.identifier = objectLocator.Substring(0, 8);
                objectLocator = objectLocator.Substring(8, objectLocator.Length - 8);
            }

            Logger.Debug(string.Format("ExtractObjectLocator, {0}={1}", this.identifier, objectLocator));
            return objectLocator;
        }

        /// <summary>
        /// Gets the control from object locator.
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="objectLocator">The object locator.</param>
        /// <returns></returns>
        private T GetControlFromObjectLocator<T>(string objectLocator) where T : Control, new()
        {
            T SearchControl;
            objectLocator = this.ExtractObjectLocator(objectLocator);
            switch (this.identifier)
            {
                case "id":
                    SearchControl = Find.ById<T>(objectLocator);
                    break;
                case "name":
                    SearchControl = Find.ByName<T>(objectLocator);
                    break;
                case "tagindex":
                    SearchControl = Find.ByTagIndex<T>(objectLocator, 1);
                    break;
                case "xpath":
                    SearchControl = Find.ByXPath<T>(objectLocator);
                    break;
                case "content":
                    SearchControl = Find.ByContent<T>(objectLocator);
                    break;
                case "class":
                    SearchControl = Find.ByAttributes<T>(new string[] { string.Format("class={0}", objectLocator) });
                    break;
                case "atribute":
                    SearchControl = Find.ByAttributes<T>(objectLocator.Split(new char[] { ';' }));
                    break;
                default:
                    SearchControl = Find.ById<T>(objectLocator);
                    break;
            }

            if (null != SearchControl)
            {
                Logger.Debug(string.Format("Inside GetControlFromObjectLocator , return type {0}", SearchControl.GetType().ToString()));
            }

            return SearchControl;
        }

        /// <summary>
        /// Gets the element from object locator.
        /// </summary>
        /// <param name="objectLocator">The object locator.</param>
        /// <returns></returns>
        private Element GetElementFromObjectLocator(string objectLocator)
        {
            Element SearchControl;
            objectLocator = this.ExtractObjectLocator(objectLocator);
            switch (this.identifier)
            {
                case "id":
                    SearchControl = Find.ById(objectLocator);
                    break;
                case "name":
                    SearchControl = Find.ByName(objectLocator);
                    break;
                case "tagindex":
                    SearchControl = Find.ByTagIndex(objectLocator, 1);
                    break;
                case "xpath":
                    SearchControl = Find.ByXPath(objectLocator);
                    break;
                case "content":
                    SearchControl = Find.ByXPath(objectLocator);
                    break;
                case "class":
                    SearchControl = Find.ByAttributes(new string[] { string.Format("class={0}", objectLocator) });
                    break;
                case "atribute":
                    SearchControl = Find.ByAttributes(objectLocator.Split(new char[] { ';' }));
                    break;
                default:
                    SearchControl = Find.ByContent(objectLocator);
                    break;
            }

            if (null != SearchControl)
            {
                Logger.Debug(string.Format("Inside GetControlFromObjectLocator , return type {0}", SearchControl.GetType().ToString()));
            }

            return SearchControl;
        }

		/// <summary>
		/// Waits for control.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mapPath">The GUI map path.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		/// <exception cref="GUIException"></exception>
        public T WaitForControl<T>(string mapPath, string logicalName, int maximumWaitTime) where T : Control, new()
        {
            T control;
            DateTime start; 
            DateTime end;
            double timeElapsed = 0;
            try
            {
                ActiveBrowser.RefreshDomTree();
                control = this.GetControl<T>(mapPath, logicalName);
                start = DateTime.Now;

                if (null == control)
                {
                    while (null == control && timeElapsed < maximumWaitTime)
                    {
                        end = DateTime.Now;
                        ActiveBrowser.RefreshDomTree();
                        control = this.GetControl<T>(mapPath, logicalName);
                        timeElapsed = ((TimeSpan)(end - start)).TotalMilliseconds;
                    }
                }
                Logger.Debug(string.Format("Inside WaitForControl , returned control in {0}ms", timeElapsed));

                return control;
            }
            catch (NullReferenceException e)
            {
                throw new GUIException(logicalName, e);
            }
        }

		/// <summary>
		/// Waits for element.
		/// </summary>
		/// <param name="mapPath">The GUI map path.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
        public Element WaitForElement(string mapPath, string logicalName, int maximumWaitTime)
        {
            Element element;
            DateTime start; 
            DateTime end;
            double timeElapsed = 0;
            ActiveBrowser.RefreshDomTree();
            element = this.GetControl(mapPath, logicalName);
            start = DateTime.Now;
            if (null == element)
            {
                while (null == element && timeElapsed < maximumWaitTime)
                {
                    end = DateTime.Now;
                    ActiveBrowser.RefreshDomTree();
                    element = this.GetControl(mapPath, logicalName);
                    timeElapsed = ((TimeSpan)(end - start)).TotalMilliseconds;
                }
            }
            Logger.Debug(string.Format("Inside WaitForControl , returned control in {0}ms", timeElapsed));
            return element;
        }

        /// <summary>
        /// Gets the object collection.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static Dictionary<string, Guimap> GetObjectCollection(string filePath)
        {
            return GlobalGuiCollection.GetMap(filePath);
        }

        /// <summary>
        /// Logs the check for collection.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        protected internal static void LogCheckForCollection(string fileName)
        {
            Logger.Debug(string.Concat("There is no existing ", fileName, " Object Collection"));
            Logger.Debug(string.Concat("Creating a new instance for ", fileName, " Object Collection"));
        }

    }
}
