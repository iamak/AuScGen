using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.FunctionalTest.Config
{
    class TestSettings : BaseSetings
    {
        private static TestSettings defaultInstance = new TestSettings();
        private static string settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
        private static string settingsFile;
                
        public static TestSettings Default
        {
            get
            {
                settingsFile = Path.Combine(settingsFilePath, "TestSettings.xml");
                return defaultInstance;
            }

        }

        string browser = "InternetExplorer";
        public string Browser
        {
            get
            {
                string value = GetValue(settingsFile, "Browser");
                if (null != value)
                {
                    url = value;
                }
                return url;
                //return ((string)(this["Url"]));
            }
            //set
            //{
            //    this["Url"] = value;
            //}
        }

        //[global::System.Configuration.DefaultSettingValueAttribute("http://hyd-ecolabweb5:9999")]
        string url = "http://hyd-ecolabweb5:9999";
        public string Url
        {
            get
            {
                string value = GetValue(settingsFile, "Url");
                if (null != value)
                {
                    url = value;
                }
                return url;
                //return ((string)(this["Url"]));
            }
            //set
            //{
            //    this["Url"] = value;
            //}
        }

        //[global::System.Configuration.DefaultSettingValueAttribute("Data Source=HYD-ECOLABDB\\SQLExpress;Initial Catalog=ConduitLocalQA;User ID=tcddev" +
        //    ";Password=Agstcd@1")]
        string dBConnection = "Data Source=HYD-ECOLABDB\\SQLExpress;Initial Catalog=ConduitLocalQA;User ID=tcddev;Password=Agstcd@1";
        public string DBConnection
        {
            get
            {
                string value = GetValue(settingsFile, "DBConnection");
                if (null != value)
                {
                    dBConnection = value;
                }
                return dBConnection;
                //return ((string)(this["DBConnection"]));
            }
            //set
            //{
            //    this["DBConnection"] = value;
            //}
        }

        //[global::System.Configuration.DefaultSettingValueAttribute("http://hyd-ecolabweb5:9999/Api/TrendingChart/GetByFilters?")]
        string getByFilterAPI = "http://hyd-ecolabweb5:5555/Api/TrendingChart/GetByFilters?";
        public string GetByFilterAPI
        {
            get
            {
                string value = GetValue(settingsFile, "GetByFilterAPI");
                if (null != value)
                {
                    getByFilterAPI = value;
                }
                return getByFilterAPI;
                //return ((string)(this["GetByFilterAPI"]));
            }
            //set
            //{
            //    this["GetByFilterAPI"] = value;
            //}
        }

        //[global::System.Configuration.DefaultSettingValueAttribute("False")]
        bool userViewMode = false;
        public bool UserViewMode
        {
            get
            {
                string value = GetValue(settingsFile, "UserViewMode");
                if (null != value)
                {
                    userViewMode = value.ToLower().Contains("true");

                }
                return userViewMode;
                //return ((bool)(this["UserViewMode"]));
            }
            //set
            //{
            //    this["UserViewMode"] = value;
            //}
        }

    }
}
