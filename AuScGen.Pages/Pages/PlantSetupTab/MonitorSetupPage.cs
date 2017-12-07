using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using ArtOfTest.Common.UnitTesting;
using Ecolab.CommonUtilityPlugin;
using System.Threading;
using System.Collections.ObjectModel;

namespace Ecolab.Pages
{
    public class MonitorSetupPage : PageBase
    {
        private string guiMap;
        
        public MonitorSetupPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "MonitorSetupTab.xml");
        }

        public MonitorSetupPage(List<object> utilsList)
            : base(utilsList, "MonitorSetupTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "MonitorSetupTab.xml");
        }

        public HtmlControl MonitorSetupTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabMonitorSetup");
            }
        }

        public HtmlButton AddMonitor
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnAddMonitor");
            }
        }

        public HtmlInputText DashboardName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDashboardName");
            }
        }

        public HtmlSelect DashboardType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlDashboardType");
            }
        }

        public HtmlInputCheckBox EnableParameter
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbEnableParameter");
            }
        }

        public HtmlInputCheckBox Customer
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbCustomer");
            }
        }

        public HtmlInputCheckBox Formula
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbFormula");
            }
        }

        public HtmlInputCheckBox Load
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbLoad");
            }
        }

        public HtmlInputCheckBox DisplayonLogin
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbDisplayonLogin");
            }
        }

        public HtmlButton Save
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpdate");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlControl Message
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblMessage");
            }
        }

        public HtmlControl DuplicateMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("duplicateMessage");
            }
        }

        public CommonControls.EcolabDataGrid MonitorTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "monitorGrid");
            }
        }

        public ReadOnlyCollection<string> GetMachineNames 
        { 
            get
            {
                string queryString = ".//*[@id='details-container']/div[6]/div/button/../ul/li/a/label";
                List<string> optionValues = new List<string>();
                ReadOnlyCollection<HtmlControl> controls;
                Telerik.ActiveBrowser.RefreshDomTree();
                controls = WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
                {
                    Telerik.ActiveBrowser.RefreshDomTree();
                    return Telerik.Find.AllByXPath<HtmlControl>(queryString);
                }, Config.PageClassSettings.Default.MaxTimeoutValue);
                if(null != controls)
                {
                    controls.ToList().ForEach(control => optionValues.Add(control.BaseElement.InnerText));
                }
                return optionValues.AsReadOnly();
                
            }
        }

        public ReadOnlyCollection<string> GetMonitorNames
        {
            get
            {
                string queryString
                = ".//*[@id='details-container']/div[8]/div/button/../ul/li/a/label";
                List<string> optionValues = new List<string>();
                ReadOnlyCollection<HtmlControl> controls;
                Telerik.ActiveBrowser.RefreshDomTree();
                controls = WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
                {
                    Telerik.ActiveBrowser.RefreshDomTree();
                    return Telerik.Find.AllByXPath<HtmlControl>(queryString);
                }, Config.PageClassSettings.Default.MaxTimeoutValue);
                if (null != controls)
                {
                    controls.ToList().ForEach(control => optionValues.Add(control.BaseElement.InnerText));
                }
                return optionValues.AsReadOnly();

            }
        }

        public void SelectMachineName(string machineName)
        {
            string queryString
               = string.Format(".//*[@id='details-container']/div[6]/div/button/../ul/li/a/label[contains(text(),'{0}')]", machineName);           
            Telerik.ActiveBrowser.RefreshDomTree();
            WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void SelectMonitorName(string monitorName)
        {
            string queryString
               = string.Format(".//*[@id='details-container']/div[8]/div/button/../ul/li/a/label[contains(text(),'{0}')]", monitorName);
            Telerik.ActiveBrowser.RefreshDomTree();
            WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

    }
}
