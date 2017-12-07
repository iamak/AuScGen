using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalGuiCollection
    {
        private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, Dictionary<string, Guimap>> globalPageCollection;

        public GlobalGuiCollection()
        {
            log4net.ThreadContext.Properties["myContext"] = "Logging from GlobalGuiCollection Class";
            Logger.Debug("Inside GlobalGuiCollection Constructor!");
        }   

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

        public static Dictionary<string, Guimap> GetGuimap(string filepath)
        {
            string originalFilePath = filepath;
            Dictionary<string, Dictionary<string, Guimap>> collection = null;
            string filename = Path.GetFileName(filepath);
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

        private static void AddNewGuiMap(string filename, string filepath)
        {
            LogCheckForCollection(filename);
            GlobalPageCollection.Add(filename, GuiMapParser.GetInstance().LoadGuiMap(filepath));
            Logger.Debug(string.Concat("Successfully Created ", filename, " Object Collection!"));
        }

        private static void QueueCleanup()
        {
            ThreadPool.QueueUserWorkItem(Cleanup);
        }

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

        private static void LogCheckForCollection(string fileName)
            {
            Logger.Debug(string.Concat("There is no existing ", fileName, " Object Collection"));
            Logger.Debug(string.Concat("Creating a new instance for ", fileName, " Object Collection"));
        }

    }
}
