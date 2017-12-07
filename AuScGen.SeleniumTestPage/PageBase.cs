using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using UIAccess.WebControls;
using UIAccess;


namespace AuScGen.SeleniumTestPage
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
		/// The d b access
		/// </summary>
        private AuScGen.CommonUtilityPlugin.DataAccess dBAccess;
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
                return dBAccess;
            }
        }

		/// <summary>
		/// The web driver
		/// </summary>
        private WebDriverPlugin webDriver;
		/// <summary>
		/// Gets the web driver.
		/// </summary>
		/// <value>
		/// The web driver.
		/// </value>
        protected WebDriverPlugin WebDriver
        {
            get
            {
                return webDriver;
            }
        }

        //private Utils.Wait wait;
        //public Utils.Wait Wait 
        //{ 
        //    get
        //    {
        //        return new Utils.Wait(Telerik);
        //    }
        //}

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
        protected AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator KeyBoardSimulator
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
        protected static string GuiMapPath
        {
            get
            {
                return Directory.GetCurrentDirectory() + @"\GuiMaps\";
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="webDriverPlugin">The web driver plugin.</param>
        public PageBase(WebDriverPlugin webDriverPlugin)
        {
            webDriver = webDriverPlugin;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="webDriverPlugin">The web driver plugin.</param>
		/// <param name="guiMapName">Name of the GUI map.</param>
        public PageBase(WebDriverPlugin webDriverPlugin, string guiMapName)
            : this(webDriverPlugin)
        {
            completeGuiMapPath = string.Concat(GuiMapPath, guiMapName);
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="utils">The utils.</param>
        public PageBase(List<object> utils)
        {
            foreach (object util in utils)
            {
                if (util is WebDriverPlugin)
                {
                    webDriver = (WebDriverPlugin)util;
                }

                if (util is AuScGen.CommonUtilityPlugin.DataAccess)
                {
                    dBAccess = (AuScGen.CommonUtilityPlugin.DataAccess)util;
                }

                if (util is AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator)
                {
                    keyboardSimulator = (AuScGen.CommonUtilityPlugin.MouseKeyBoardSimulator)util;
                }
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBase"/> class.
		/// </summary>
		/// <param name="utils">The utils.</param>
		/// <param name="guiMapName">Name of the GUI map.</param>
        public PageBase(List<object> utils, string guiMapName)
            : this(utils)
        {
            completeGuiMapPath = string.Concat(GuiMapPath, guiMapName);
        }

		/// <summary>
		/// Determines whether the specified logical name is present.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="waitBeforeCheck">The wait before check.</param>
		/// <returns></returns>
        public bool IsPresent<T>(string logicalName,int waitBeforeCheck) where T : WebControl
        {
            Thread.Sleep(waitBeforeCheck);
            if (null == WebDriver.GetControl<T>(completeGuiMapPath, logicalName).SeleniumControl)
            {
                return false;
            }

            return true;
        }

		/// <summary>
		/// Gets the HTML control.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="GUIMap">The GUI map.</param>
		/// <param name="LogicalName">Name of the logical.</param>
		/// <returns></returns>
        public T GetHtmlControl<T>(string GUIMap, string LogicalName) where T : WebControl
        {
            T Ctrl = null;

            Ctrl = WebDriver.WaitForControl<T>(GUIMap, LogicalName,
                                                Config.PageClassSettings.Default.MaxTimeoutValue);
            if (Ctrl == null)
            {
                //throw new GUIException(LogicalName, "Element not found on the Screen");
            }       
            return Ctrl;
        }

		/// <summary>
		/// Gets the HTML control.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public T GetHtmlControl<T>(string logicalName) where T : WebControl
        {
            return GetHtmlControl<T>(completeGuiMapPath, logicalName);
        }

    }
}
