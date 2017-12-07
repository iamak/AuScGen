using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using log4net.Config;
using System.IO;

/// <summary>
/// 
/// </summary>
namespace Framework
{
    /// <summary>
    /// This class parses the GUI objects from an XML file and
    /// return the actual Element Value
    /// based on the logical name provided
    /// </summary>
    public class GuiMapParser
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static log4net.ILog Logger =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()
           .DeclaringType);
        //Identifier constants
        /// <summary>
        /// The identifier
        /// </summary>
        private const string id = "id";
        /// <summary>
        /// The name
        /// </summary>
        private const string name = "name";
        /// <summary>
        /// The xpath
        /// </summary>
        private const string xpath = "xpath";
        /// <summary>
        /// The classname
        /// </summary>
        private const string classname = "class";
        /// <summary>
        /// The tagname
        /// </summary>
        private const string tagname = "tagname";
        /// <summary>
        /// The content
        /// </summary>
        private const string content = "content";
        /// <summary>
        /// The atribute
        /// </summary>
        private const string atribute = "atribute";
        /// <summary>
        /// The XML node path
        /// </summary>
        private const string xmlNodePath = "/ObjectRepository/FeatureSet";
        /// <summary>
        /// The GUI map parser
        /// </summary>
        private static GuiMapParser guiMapParser;

        /// <summary>
        /// Prevents a default instance of the <see cref="GuiMapParser" /> class from being created.
        /// </summary>
        private GuiMapParser()
        {
            //log4net.Config.XmlConfigurator.Configure();
            log4net.ThreadContext.Properties["myContext"] = "Logging from GuiMapParser Class";
            Logger.Debug("Inside GuiMapParser Constructor!");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static GuiMapParser GetInstance()
        {
            if (guiMapParser == null)
            {
                Logger.Debug("Creating new instance of GuiMapParser");
                guiMapParser = new GuiMapParser();
                return guiMapParser;
            }
            return guiMapParser;
        }

        /// <summary>
        /// Loads a single GUIMap XML as dictionary collection
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <exception cref="Ecolab.TelerikPlugin.ResourceException"></exception>
        /// <exception cref="TCDFrameworkException"></exception>
        public Dictionary<String, Guimap> LoadGuiMap(String filePath)
        {
            Logger.Debug("Creating instance of XML Document");
            XmlDocument doc = new XmlDocument();
            Dictionary<String, Guimap> guiObjCollection = null;
            try
            {
                Logger.Debug(string.Concat("Loading the Guimap xml file : [", filePath, "]"));
                doc.Load(filePath);
                XmlNodeList rootNode = doc.DocumentElement.SelectNodes(xmlNodePath);
                //Create a dictionary object that can hold key value pairs of string and GUIMap objects
                guiObjCollection = new Dictionary<string, Guimap>();
                Guimap guimap = null;
                foreach (XmlNode featureSetNode in rootNode)
                {

                    XmlNodeList elementNodes = featureSetNode.ChildNodes;
                    foreach (XmlNode node in elementNodes)
                    {
                        guimap = new Guimap();
                        string logicalName = node.Attributes["name"].InnerText;
                        string identificationType = node.FirstChild.Name;
                        string elementValue = node.FirstChild.InnerText;
                        //Assgin logical name
                        guimap.LogicalName = logicalName;
                        //Save the XML details to a GUIMAP class
                        //identify the identifier && assign the identifier type
                        switch (identificationType.ToLower())
                        {
                            case id:
                                guimap.IdentificationType = identificationType;
                                guimap.Id = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                { 
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                }
                                continue;
                            case name:
                                guimap.IdentificationType = identificationType;
                                guimap.Name = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                }
                                continue;
                            case xpath:
                                guimap.IdentificationType = identificationType;
                                guimap.Xpath = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                }
                                continue;
                            case classname:
                                guimap.IdentificationType = identificationType;
                                guimap.ClassName = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                }
                                continue;
                            case tagname:
                                guimap.IdentificationType = identificationType;
                                guimap.Tagname = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                } continue;
                            case content:
                                guimap.IdentificationType = identificationType;
                                guimap.Content = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                }
                                continue;
                            case atribute:
                                guimap.IdentificationType = identificationType;
                                guimap.Atribute = elementValue;
                                //Add the logical name and GUIMap to the Object Collection
                                if (!guiObjCollection.ContainsKey(guimap.LogicalName))
                                {
                                    guiObjCollection.Add(guimap.LogicalName, guimap);
                                } continue;
                        }
                    }
                }
            }
            catch (FileNotFoundException fne)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Logger.Error("File " + filePath + "not found", fne);
                string message = string.Format("Exception occured while loading the values from Gui map xml {0} not found", filePath);
                Logger.Error(message, fne);
                throw new ResourceException(methodName, message, fne);
            }
            //catch (Exception ex)
            //{
            //    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            //    string message = "Exception occured while loading the values" +
            //        "from Gui map xml" + filePath + "not found";
            //    Logger.Error(message, ex);
            //    throw new ResourceException(methodName, message, ex);
            //}
            return guiObjCollection;
        }

        /// <summary>
        /// Gets the element value.
        /// </summary>
        /// <param name="guiObjCollection">The GUI object collection.</param>
        /// <param name="logicalName">Name of the logical.</param>
        /// <returns></returns>
        /// <exception cref="Ecolab.TelerikPlugin.GUIException">
        /// identifier Not implemented!
        /// or
        /// Exception occured while trying to fetch value from Guimap!
        /// </exception>
        public string GetElementValue(Dictionary<string, Guimap> guiObjCollection, string logicalName)
        {
            Guimap tempMap = null;
            string elementValue = "";
            try
            {
                if (guiObjCollection.ContainsKey(logicalName))
                {
                    tempMap = guiObjCollection[logicalName];
                    string identityType = tempMap.IdentificationType;
                    switch (identityType.ToLower())
                    {
                        case id:
                            elementValue = "id" + tempMap.Id;
                            break;
                        case name:
                            elementValue = "name" + tempMap.Name;
                            break;
                        case xpath:
                            elementValue = "xpath" + tempMap.Xpath;
                            break;
                        case classname:
                            elementValue = "class" + tempMap.ClassName;
                            break;
                        case tagname:
                            elementValue = "tagname" + tempMap.Tagname;
                            break;
                        case content:
                            elementValue = "content" + tempMap.Content;
                            break;
                        case atribute:
                            elementValue = "atribute" + tempMap.Atribute;
                            break;
                        default:
                            throw new GUIException("identifier Not implemented!");
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Logger.Error("Exception occured while trying to fetch value from Guimap!");
                throw new GUIException("Exception occured while trying to fetch value from Guimap!", e);
            }
            catch (ArgumentException e)
            {
                Logger.Error("Exception occured while trying to fetch value from Guimap!");
                throw new GUIException("Exception occured while trying to fetch value from Guimap!", e);
            }

            return elementValue;
        }
    }
}

