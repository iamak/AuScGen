using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.SeleniumTestPage.Config 
{
	/// <summary>
	///		Class Page Class Settings
	/// </summary>
    public class PageClassSettings : BaseSetings
    {
		/// <summary>
		/// The default instance
		/// </summary>
        private static PageClassSettings defaultInstance = new PageClassSettings();
		/// <summary>
		/// The settings file path
		/// </summary>
        private static string settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
		/// <summary>
		/// The settings file
		/// </summary>
        private static string settingsFile;

		/// <summary>
		/// Gets the default.
		/// </summary>
		/// <value>
		/// The default.
		/// </value>
        public static PageClassSettings Default
        {
            get
            {
                settingsFile = Path.Combine(settingsFilePath, "TestSettings.xml");
                return defaultInstance;
            }

        }

		/// <summary>
		/// The maximum timeout value
		/// </summary>
        private int maxTimeoutValue = 20000;
		/// <summary>
		/// Gets the maximum timeout value.
		/// </summary>
		/// <value>
		/// The maximum timeout value.
		/// </value>
        public int MaxTimeoutValue
        {
            get
            {
                string value = GetValue(settingsFile, "MaxTimeoutValue");
                if (null != value)
                {
                    maxTimeoutValue = Convert.ToInt32(value);
                }
                return maxTimeoutValue;
                //return ((int)(this["MaxTimeoutValue"]));
            }
            //set
            //{
            //    this["MaxTimeoutValue"] = value;
            //}
        }   

    }
}
