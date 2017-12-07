// ***********************************************************************
// <copyright file="Program.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Program class</summary>
// ***********************************************************************
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace AuScGen.PageMethodGenerator
{
	/// <summary>
	///		Class Program
	/// </summary>
    public class Program
    {
		/// <summary>
		/// Gets the application settings.
		/// </summary>
		/// <value>
		/// The application settings.
		/// </value>
        private static NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
        static void Main()
        {
            GeneratePageClass(@"D:\AutomationsXmlSave\GoogleSearch");
        }

		/// <summary>
		/// Generates the page class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
        public static void GeneratePageClass(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument xmldoc = new XmlDocument();

            fileName = fileName + ".xml";

            string GUIMapPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            GUIMapPath = GUIMapPath + fileName;
            xmldoc.Load(fileName);
            string[] name = fileName.Split('.');
            string className = name[0].Split('\\').Last();

            sb.Append("using " + AppSettings.Get("Using1") + ";\n");
            sb.Append("using " + AppSettings.Get("Using2") + ";\n");
            sb.Append("using " + AppSettings.Get("Using3") + ";\n");
            sb.Append("using " + AppSettings.Get("Using4") + ";\n");
            sb.Append("using " + AppSettings.Get("Using5") + ";\n");
            sb.Append("using " + AppSettings.Get("UsingPM") + ";\n\n");
            sb.Append("namespace " + AppSettings.Get("NameSpacePM") + "\n");
            sb.Append("{\n");
            sb.Append("\t\tpublic partial class " + className + "Method : " + className + "\n");
            sb.Append("\t{\n");
            sb.Append("\t\tpublic " + className + "Method(IList<object> utilsList)\n");
            sb.Append("\t\t\t:base(utilsList)\n\t\t{\t}\n");
            sb.Append("\t\tpublic void TestProperties()\n\t{\n");
            XmlNodeList xmlNodeList = xmldoc.SelectNodes("ObjectRepository/FeatureSet/Element");
            foreach (XmlNode item in xmlNodeList)
            {
                sb.Append("\tthis." + item.Attributes["name"].Value + ControlSelect(item.Attributes["type"].Value) + ";\n");
            }
            sb.Append("}");
            string outputPath = AppSettings.Get("PageMethodsCreated");

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            outputPath = outputPath + "\\" + fileName.Split('\\').Last().Split('.').First() + "Method.cs";
            File.WriteAllText(GetUniqueFileName(outputPath), sb.ToString());
        }

		/// <summary>
		/// Gets the name of the unique file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
        public static string GetUniqueFileName(string filePath)
        {
            string baseName = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
            string uniqueName = string.Format("{0}{1}", baseName, ".cs");
            return uniqueName;
        }

		/// <summary>
		/// Controls the select.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
        public static string ControlSelect(string text)
        {
            switch (text)
            {
                case "SpanArea":
                    return "WebSpanArea";
                case "Textbox":
                    return ".SendKeys()";
                case "Combobox":
                    return "WebComboBox";
                case "Dialog":
                    return "WebDialog";
                case "Button":
                    return ".Click()";
                case "Label":
                    return "WebLabel";
                case "Calender":
                    return "WebCalender";
                case "Frame":
                    return "WebFrame";
                case "Image":
                    return "WebImage";
                case "Link":
                    return "WebLink";
                case "Listbox":
                    return "WebListBox";
                case "Page":
                    return "WebPage";
                case "Radio Button":
                    return "WebRadioButton";
                case "Web Table":
                    return "WebTable";
                default:
                    return "WebControl";
            }
        }
    }
}
