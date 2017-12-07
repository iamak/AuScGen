using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuScGen.TestExecutionUtil;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace AuScGen.FunctionalTest.Utils
{
    public class ScreenShot : IScreenPrint
    {
        private TelerikPlugin.TelerikFramework telerik;
        private string folderPath;
        private string declaringType;
        private string methodName;
        private MethodBase MethodBase 
        { 
            get
            {
                StackTrace stackTrace = new StackTrace();
                return stackTrace.GetFrame(4).GetMethod();
            }
        }
        private int index;

        public string LogFolder
        { 
            get
            {
                declaringType = MethodBase.DeclaringType.ToString();
                methodName = MethodBase.Name;
                string screenShotFolder = string.Format(@"{0}\{1}.{2}", folderPath, declaringType, methodName);
                CreateDirectory(screenShotFolder);
                Console.WriteLine(MethodBase.Name); // e.g.
                return screenShotFolder;
            }
        }

        public ScreenShot(TelerikPlugin.TelerikFramework telerikFw, string  folder)
        {
            telerik = telerikFw;
            folderPath = folder;
        }

        public void ScreenPrint()
        {
            telerik.ActiveBrowser.Capture().Save(string.Format(@"{0}\{1}_{2}.png", LogFolder, methodName, index.ToString()));
        }
        
        public void ScreenPrint(string message)
        {
            GetScreenPrint();
            SaveStepMessage(GetStepMessage(message));
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                index = 1;
            }
        }

        private void GetScreenPrint()
        {
            using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);
                }

                bmpScreenCapture.Save(string.Format(@"{0}\{1}_{2}.png", LogFolder, methodName, Guid.NewGuid()));
            }
        }

        private void SaveStepMessage(StepMessage message)
        {
            //XmlSerializer ser = new XmlSerializer(typeof(StepMessage));
            //bool bAppend = true ;

            //TextWriter writer = new StreamWriter(string.Format(@"{0}\message.xml",LogFolder), bAppend);

            //ser.Serialize(writer, message);

            //writer.Close();   
            //string sb = string.Format("{1}", message.Message);
            if (!File.Exists(string.Format(@"{0}\message.csv", LogFolder)))
            {
                StreamWriter w = File.CreateText(string.Format(@"{0}\message.csv", LogFolder));
                w.WriteLine(message.Message);
                w.Close();
            }
            else
            {
                StreamWriter w = File.AppendText(string.Format(@"{0}\message.csv", LogFolder));
                w.WriteLine(message.Message);
                w.Close();
            }
        }

        private StepMessage GetStepMessage(string messagetext)
        {
            StepMessage message = new StepMessage();
            message.StepNumber = 1;
            message.StepName = string.Format("Step:{0}", message.StepNumber);
            message.Message = messagetext;
            return message;
        }       
    }
}
