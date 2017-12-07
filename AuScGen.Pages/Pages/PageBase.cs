using ArtOfTest.WebAii.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecolab.TelerikPlugin;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.Common.Exceptions;
using System.Collections;
using System.Threading;
using ArtOfTest.WebAii.TestTemplates;

namespace Ecolab.Pages
{
    public class PageBase
    {
        private string completeGuiMapPath;

        private Ecolab.TelerikPlugin.TelerikFramework telerik;
        protected Ecolab.TelerikPlugin.TelerikFramework Telerik
        {
            get
            {
                return telerik;
            }
        }

        private CommonUtilityPlugin.DataAccess dBAccess;
        protected CommonUtilityPlugin.DataAccess DBAccess
        {
            get
            {
                return dBAccess;
            }
        }

        private DialogManager dialogManager;
        protected DialogManager DialogHandler
        {
            get
            {
                return dialogManager;
            }
        }

        private CommonUtilityPlugin.MouseKeyBoardSimulator keyboardSimulator;
        protected CommonUtilityPlugin.MouseKeyBoardSimulator KeyBoardSimulator
        {
            get
            {
                return keyboardSimulator;
            }
        }

        protected static string GuiMapPath
        {
            get
            {
                return Directory.GetCurrentDirectory() + @"\GuiMaps\";
            }
        }

        public PageBase(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
        {
            telerik = TelerikPlugin;
        }

        public PageBase(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin, string guiMapName)
            : this(TelerikPlugin)
        {
            completeGuiMapPath = string.Concat(GuiMapPath, guiMapName);
        }

        public PageBase(List<object> utils)
        {
            foreach (object util in utils)
            {
                if (util is Ecolab.TelerikPlugin.TelerikFramework)
                {
                    telerik = (Ecolab.TelerikPlugin.TelerikFramework)util;
                }

                if (util is CommonUtilityPlugin.DataAccess)
                {
                    dBAccess = (CommonUtilityPlugin.DataAccess)util;
                }

                if (util is DialogManager)
                {
                    dialogManager = (DialogManager)util;
                }

                if (util is CommonUtilityPlugin.MouseKeyBoardSimulator)
                {
                    keyboardSimulator = (CommonUtilityPlugin.MouseKeyBoardSimulator)util;
                }
            }
        }

        public PageBase(List<object> utils, string guiMapName)
            : this(utils)
        {
            completeGuiMapPath = string.Concat(GuiMapPath, guiMapName);
        }

        public T GetHtmlControl<T>(string GUIMap, string LogicalName) where T : Control, new()
        {
            T Ctrl = null;

            Ctrl = Telerik.WaitForControl<T>(GUIMap, LogicalName,
                                                Config.PageClassSettings.Default.MaxTimeoutValue);
            if (Ctrl == null)
            {
                throw new GUIException(LogicalName, "Element not found on the Screen");
            }       
            return Ctrl;
        }

        public T GetHtmlControl<T>(string logicalName) where T : Control, new()
        {
            return GetHtmlControl<T>(completeGuiMapPath, logicalName);
        }

        public bool IsPresent<T>(string logicalName) where T : Control, new()
        {
            Thread.Sleep(3000);
            if(null == Telerik.GetControl<T>(completeGuiMapPath, logicalName))
            {
                return false;
            }

            return true;
        }

        public delegate bool waitForTrueAction();
        public bool WaitforAction(waitForTrueAction decisionAction, int MaxWaitTime)
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();
            
            start = DateTime.Now;
          
            while (false == decisionAction() && timeElapsed < MaxWaitTime)
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            return decisionAction();
        }

        public delegate HtmlControl waitForHtmlControlAction();
        public HtmlControl WaitforAction(waitForHtmlControlAction decisionAction, int MaxWaitTime)
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();

            start = DateTime.Now;

            while (null == decisionAction() && timeElapsed < MaxWaitTime)
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            return decisionAction();
        }

        public delegate Object waitForObjectAction();
        public Object WaitforAction(waitForObjectAction decisionAction, int MaxWaitTime)
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();

            start = DateTime.Now;

            while (null == decisionAction() && timeElapsed < MaxWaitTime)
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            return decisionAction();
        }

        public delegate T waitForObjectAction<T>();
        public T WaitforAction<T>(waitForObjectAction decisionAction, int MaxWaitTime)
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();

            start = DateTime.Now;
            if (!typeof(T).Name.Contains("ReadOnlyCollection"))
            {
                while (null == decisionAction() && timeElapsed < MaxWaitTime)
                {
                    Telerik.ActiveBrowser.RefreshDomTree();
                    timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
                }
            }
            else
            {
                while(null == decisionAction() && timeElapsed < MaxWaitTime/2)
                {
                    Telerik.ActiveBrowser.RefreshDomTree();
                    timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
                }

                if (null != decisionAction())
                {
                    WaitforAction(() =>
                    {
                        return (int)typeof(T).GetProperty("Count").GetValue(decisionAction()) > 0;
                    }, Config.PageClassSettings.Default.MaxTimeoutValue / 2);
                }                
            }

            return (T)decisionAction();
        }

        public T WaitforNullAction<T>(waitForObjectAction decisionAction, int MaxWaitTime)
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();

            start = DateTime.Now;           
            while (null != decisionAction() && timeElapsed < MaxWaitTime)
            {
                var test = decisionAction();
                Telerik.ActiveBrowser.RefreshDomTree();
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }          

            return (T)decisionAction();
        }

        public delegate T waitForCountAction<T>() where T : ICollection;
        public T WaitforCountAction<T>(waitForCountAction<T> decisionAction, int countValue ,int MaxWaitTime) where T : ICollection
        {
            DateTime start;
            double timeElapsed = 0;
            Telerik.ActiveBrowser.RefreshDomTree();

            start = DateTime.Now;

            int test = 3;
            while (decisionAction().Count != countValue && timeElapsed < MaxWaitTime)
            {
                test = decisionAction().Count;
                Telerik.ActiveBrowser.RefreshDomTree();
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }
            //int test2 = test;
            return (T)decisionAction();
        }
    }
}
