using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AuScGen.TestExecutionUtil
{
    public class TestExecute
    {
        public IScreenPrint Print { get; set; }

        private Tray tray;

        public Tray Tray 
        { 
            get
            {
                if(null == tray)
                {
                    tray = new Tray();
                    tray.Show();
                    tray.Activate();
                    tray.Visible = true;
                    tray.TopMost = true;
                    tray.Location = new System.Drawing.Point(0, 0);
                    tray.Height = 19;
                }
                return tray;
            }
        }

        public void DoStep(string message, Action action)
        {
            try
            {
                Tray.Message = TrayMessage(message);
                action();
            }
            finally
            {
                Print.ScreenPrint(message);
            }
            //Telerik.ActiveBrowser.Capture().Save(@".\TC02_NavigateToAccountBalancePage\Image" + Guid.NewGuid() + ".png");
        }

        public void DoStep(Action action)
        {
            try
            {
                Tray.Message = TrayMessage();
                action();
            }
            finally
            {
                Print.ScreenPrint();
            }
            //Telerik.ActiveBrowser.Capture().Save(@".\TC02_NavigateToAccountBalancePage\Image" + Guid.NewGuid() + ".png");
        }

        public void DoStepWithoutScreenShot(Action action)
        {
            Tray.Message = TrayMessage();
            action();
        }

        private string TrayMessage(string message)
        {
            StackTrace stack = new StackTrace();
            string methodName = stack.GetFrames()[2].GetMethod().Name;
            return string.Format("{0} : {1}", methodName, message);
        }

        private string TrayMessage()
        {
            StackTrace stack = new StackTrace();
            string methodName = stack.GetFrames()[2].GetMethod().Name;
            return string.Format("{0} : No message sent from test", methodName);
        }

        public ReadOnlyCollection<StepMessage> GetMessages(string path)
        {
            //var myDeserializer = new XmlSerializer(typeof(StepMessage));
            List<StepMessage> messages = new List<StepMessage>();
            //using (var myFileStream = new FileStream(string.Format(@"{0}\message.xml",path), FileMode.Open))
            //{
            //    messages = (List<StepMessage>)myDeserializer.Deserialize(myFileStream);
            //}

            List<string> lines = File.ReadAllLines(string.Format(@"{0}\message.xml", path)).ToList();
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                messages.Add(new StepMessage
                {
                    StepName = values[0],
                    Message = values[1]
                });
            }
            return messages.AsReadOnly();
        }

    }
}
