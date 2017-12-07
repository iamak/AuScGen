// ***********************************************************************
// <copyright file="WebDriverPlugin.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebDriverPlugin class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Xml;
using Framework;
using UIAccess.WebControls;
using WebDriverWrapper;

namespace UIAccess
{
    /// <summary>
    /// Plugin to add WebDriver to Framework
    /// </summary>
    [Export(typeof(IPlugin))]
    public class WebDriverPlugin : IPlugin
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The identifier
        /// </summary>
        private string identifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverPlugin"/> class.
        /// </summary>
        public WebDriverPlugin()
        {
            log4net.ThreadContext.Properties["myContext"] = "Logging from WebDriverPlugin Class";
            Logger.Debug("Inside WebDriverPlugin Constructor!");
        }

        /// <summary>
        /// My browser
        /// </summary>
        private Browser myBrowser;
        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public Browser Browser
        {
            get
            {
                return myBrowser;
            }
            set
            {
                myBrowser = value;
            }
        }

        /// <summary>
        /// Sets the browser.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        public void SetBrowser(BrowserType browserType)
        {
            myBrowser = new Browser(browserType);
        }

        /// <summary>
        /// Sets the browser.
        /// </summary>
        /// <param name="browserType">Type of a browser.</param>
        /// <param name="binaryPath">The binary path.</param>
        public void SetBrowser(BrowserType browserType, string binaryPath)
        {
            myBrowser = new Browser(browserType, binaryPath);
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="locator">a locator.</param>
        /// <returns>WebCustomControl</returns>
        public WebControl GetControl(Locator locator)
        {
            WebControl webCustomControl = new WebControl(myBrowser, locator);

            if (webCustomControl.IsControlPresent())
            {
                webCustomControl.GetControl();
            }

            return webCustomControl;
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="locator">a locator.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <returns>WebControl</returns>
        public WebControl GetControl(Locator locator, ControlType controlType)
        {
            WebControl webCustomControl;

            switch (controlType)
            {
                case ControlType.Button:
                    webCustomControl = new WebButton(myBrowser, locator);
                    break;
                case ControlType.EditBox:
                    webCustomControl = new WebEditBox(myBrowser, locator);
                    break;
                default:
                    webCustomControl = new WebControl(myBrowser, locator);
                    break;
            }

            if (webCustomControl.IsControlPresent())
            {
                webCustomControl.GetControl();
            }

            return webCustomControl;
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="graphicalUserInterfaceMapPath">The GUI map path.</param>
        /// <param name="logicalName">Name of the logical.</param>
        /// <returns>GetControl for given iputs</returns>
        public T GetControl<T>(string graphicalUserInterfaceMapPath, string logicalName) where T : WebControl
        {
            Logger.Debug(string.Format("Inside GetControl<T> Method", typeof(T).ToString()));
            Dictionary<string, Guimap> guiCollection = guiCollection = GetObjectCollection(graphicalUserInterfaceMapPath);
            return GetElementFromObjectLocator<T>(GuiMapParser.GetInstance.GetElementValue(guiCollection, logicalName));
        }

        /// <summary>
        /// Waits for control.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="graphicalUserInterfaceMapPath">The GUI map path.</param>
        /// <param name="logicalName">Name of the logical.</param>
        /// <param name="maximumWaitTime">The maximum wait time.</param>
        /// <returns>WaitForControl for given iputs</returns>
        public T WaitForControl<T>(string graphicalUserInterfaceMapPath, string logicalName, int maximumWaitTime) where T : WebControl
        {
            T control;
            DateTime start;
            DateTime end;
            double timeElapsed = 0;

            control = GetControl<T>(graphicalUserInterfaceMapPath, logicalName);
            start = DateTime.Now;

            if (null == control.SeleniumControl)
            {
                while (null == control.SeleniumControl && timeElapsed < maximumWaitTime)
                {
                    end = DateTime.Now;
                    control = GetControl<T>(graphicalUserInterfaceMapPath, logicalName);
                    timeElapsed = ((TimeSpan)(end - start)).TotalMilliseconds;
                }
            }
            Logger.Debug(string.Format("Inside WaitForControl , returned control in {0}ms", timeElapsed));
            //control.Highlight();

            return control;
        }

        /// <summary>
        /// Gets the locator.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected internal Locator GetLocator(XmlNode node)
        {
            string Locator = ((XmlElement)node).GetAttribute("Locator");

            return new Locator(Locator, GetLocatorType(node));

        }

        /// <summary>
        /// Gets the type of the locator.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected static internal LocatorType GetLocatorType(XmlNode node)
        {
            string locatorType = ((XmlElement)node).GetAttribute("LocatorType");

            switch (locatorType)
            {
                case "ClassName":
                    return LocatorType.ClassName;

                case "Css":
                    return LocatorType.Css;

                case "PartialLinkText":
                    return LocatorType.PartialLinkText;

                case "Id":
                    return LocatorType.Id;

                case "LinkText":
                    return LocatorType.LinkText;

                case "Name":
                    return LocatorType.Name;

                case "TagName":
                    return LocatorType.TagName;

                case "Xpath":
                    return LocatorType.Xpath;

                default:
                    return LocatorType.Id;
            }
        }

        /// <summary>
        /// Gets the object collection.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static Dictionary<string, Guimap> GetObjectCollection(string filePath)
        {
            return GlobalGuiCollection.GetGraphicalUserInterfaceMap(filePath);
        }

        /// <summary>
        /// Gets the element from object locator.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="objectLocator">The object locator.</param>
        /// <returns></returns>
        private T GetElementFromObjectLocator<T>(string objectLocator) where T : WebControl
        {
            T SearchControl;
            objectLocator = ExtractObjectLocator(objectLocator);
            switch (identifier)
            {
                case "id":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.Id));
                    break;
                case "name":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.Name));
                    break;
                case "tagname":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.TagName));
                    break;
                case "xpath":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.Xpath));
                    break;
                case "linktext":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.LinkText));
                    break;
                case "class":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.ClassName));
                    break;
                case "partiallinktext":
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.PartialLinkText));
                    break;
                //case "atribute":
                //    SearchControl = Find.ByAttributes(objectLocator.Split(new char[] { ';' }));
                //    break;
                default:
                    SearchControl = GetControlFromLocator<T>(new Locator(objectLocator, LocatorType.Id));
                    break;
            }

            return SearchControl;
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
                identifier = objectLocator.Substring(0, 2);
                objectLocator = objectLocator.Substring(2, objectLocator.Length - 2);
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
            else if (objectLocator.StartsWith("linktext", StringComparison.CurrentCultureIgnoreCase))
            {
                identifier = objectLocator.Substring(0, 8);
                objectLocator = objectLocator.Substring(8, objectLocator.Length - 8);
            }
            else if (objectLocator.StartsWith("partiallinktext", StringComparison.CurrentCultureIgnoreCase))
            {
                identifier = objectLocator.Substring(0, 15);
                objectLocator = objectLocator.Substring(15, objectLocator.Length - 15);
            }
            else if (objectLocator.StartsWith("class", StringComparison.CurrentCultureIgnoreCase))
            {
                identifier = objectLocator.Substring(0, 5);
                objectLocator = objectLocator.Substring(5, objectLocator.Length - 5);
            }
            Logger.Debug(string.Format("ExtractObjectLocator, {0}={1}", identifier, objectLocator));
            return objectLocator;
        }

        /// <summary>
        /// Gets the control from locator.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="locator">a locator.</param>
        /// <returns></returns>
        private T GetControlFromLocator<T>(Locator locator) where T : WebControl
        {
            WebControl webCustomControl = null;

            if (typeof(T) == typeof(WebButton))
            {
                webCustomControl = new WebButton(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebEditBox))
            {
                webCustomControl = new WebEditBox(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebCalender))
            {
                webCustomControl = new WebCalender(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebCheckBox))
            {
                webCustomControl = new WebCheckBox(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebComboBox))
            {
                webCustomControl = new WebComboBox(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebDialog))
            {
                webCustomControl = new WebDialog(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebFrame))
            {
                webCustomControl = new WebFrame(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebImage))
            {
                webCustomControl = new WebImage(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebLabel))
            {
                webCustomControl = new WebLabel(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebLink))
            {
                webCustomControl = new WebLink(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebListBox))
            {
                webCustomControl = new WebListBox(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebPage))
            {
                webCustomControl = new WebPage(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebRadioButton))
            {
                webCustomControl = new WebRadioButton(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebSpanArea))
            {
                webCustomControl = new WebSpanArea(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebTable))
            {
                webCustomControl = new WebTable(myBrowser, locator);
            }

            if (typeof(T) == typeof(WebControl))
            {
                webCustomControl = new WebControl(myBrowser, locator);
            }

            if (null == webCustomControl)
            {
                webCustomControl = new WebControl(myBrowser, locator);
            }

            if (webCustomControl.IsControlPresent())
            {
                webCustomControl.GetControl();
            }

            return (T)webCustomControl;
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
                return "WebDriver Plugin";
            }
            set
            {
                Description = value;
            }
        }

    }
}
