using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

using Telerik.TestingFramework.Controls.KendoUI;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.Common;
using ArtOfTest.WebAii.Controls.Xaml;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;
using ArtOfTest.WebAii.Controls;
using ArtOfTest.Common.UnitTesting;

namespace Ecolab.Pages
{
    public class PlantSetupPage : PageBase
    {
        private string guiMap;

        public PlantSetupPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            :base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "PlantSetup.xml");
        }

        public PlantSetupPage(List<object> utilsList)
            :base(utilsList)
        {
            guiMap = string.Concat(GuiMapPath, "PlantSetup.xml");
        }

        public HtmlControl GeneralTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabGeneral",
                    Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }
                       
        public void ClickUserPreferencesSavebutton()
        {
            
            Telerik.WaitForControl<HtmlButton>(guiMap, "btnSaveUserPreferences",
                Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void UploadFile(string filePath)
        {
            DialogManager fileDialog = new DialogManager(Telerik);
            fileDialog.UpLoadFile(filePath);
        }

        public HtmlControl MyServiceDefaultPanelControl
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "MyServiceDetails", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        private CommonControls.MainMenu myTopMainMenu;
        public CommonControls.MainMenu TopMainMenu
        {
            get
            {
                if (null == myTopMainMenu)
                {
                    myTopMainMenu = new CommonControls.MainMenu(Telerik, GuiMapPath);
                }
                return myTopMainMenu;
            }
        }

        /// <summary>
        /// Clicks the user preferences savebutton.
        /// </summary>
        public ReadOnlyCollection<string> BreadCrumbDetailsList()
        {
            List<string> ListBreadCrumbItems = new List<string>();
            ICollection<Element> ChildElements = TopMainMenu.BreadCrumb.ChildNodes;
            
            foreach (Element elements in ChildElements)
            {
                ListBreadCrumbItems.Add(elements.InnerText.Trim());
            }
            return ListBreadCrumbItems.AsReadOnly();
        }     

        public string ActiveTabItem 
        { 
            get
            {
                                
                return GetHtmlControl<HtmlControl>(guiMap, "tabActiveTabItem").BaseElement.InnerText;
            }

        }

        public HtmlControl ContactsTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabContacts", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl CustomerTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabCustomer", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl ChemicalsTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabChemicals", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl MeterTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabMeter", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl UtilityTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabUtility", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl CleanSideTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabCleanSide", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }
        public HtmlControl RedFlagTab
        {
            get
            {
                return Telerik.WaitForControl<HtmlControl>(guiMap, "tabRedFlag", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }
    }
}
