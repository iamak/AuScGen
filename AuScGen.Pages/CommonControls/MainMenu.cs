using ArtOfTest.Common;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Telerik.TestingFramework.Controls.KendoUI;

namespace Ecolab.Pages.CommonControls
{
    public class MainMenu 
    {
        private Ecolab.TelerikPlugin.TelerikFramework telerik;

        private string myguiMap;
        public MainMenu(Ecolab.TelerikPlugin.TelerikFramework telerik, string guiMapPath)
        {
            this.telerik = telerik;
            myguiMap = string.Concat(guiMapPath, "CommonControls.xml");
        }
        
        public HtmlControl MainMenuControl 
        { 
            get
            {
                return telerik.GetControl<HtmlControl>(myguiMap, "panelTopMainMenu");
            }
        }

        public ReadOnlyCollection<Element> MenuItems 
        { 
            get
            {
                MainMenuControl.Click();
                return MainMenuControl.ChildNodes;                
            }
        }

        public List<string> MenuItemsList
        {             
            get
            {
                HtmlControl control = new HtmlControl();
                List<string> itemList = new List<string>();
                List<string> correctedItemList = new List<string>();
                foreach (Element item in MenuItems)
                {
                   itemList.Add(item.InnerText);
                }
                foreach(string item in itemList)
                {
                    if(!item.Equals(""))
                    {
                        correctedItemList.Add(item);
                    }
                }
                return correctedItemList;
            }
        }

		public HtmlControl UserProfileLink 
		{ 
			get
			{
				return telerik.WaitForControl<HtmlControl>(myguiMap, "lnkMyProfile", Config.PageClassSettings.Default.MaxTimeoutValue);
			}
		}

		public bool IsUserProfileLinkVisible 
		{ 
			get
			{
				return UserProfileLink.IsVisible();
			}			
		}

        public void NavigateToPlantSetupPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "itemPlantSetup", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void NavigateToManualInput()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "menuManualInputsTab",
                Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void NavigateToControlerSetupPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "itemControllerSetup", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public bool NavigateToControlerSetupPageAvailable()
        {
            return telerik.WaitForControl<HtmlControl>(myguiMap, "itemControllerSetup", Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }


        public bool NavigateToWasherGroupsPageAvailable()
        {
            return telerik.WaitForControl<HtmlControl>(myguiMap, "itemWasherGroups", Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }


        public void NavigateToWasherGroupsPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "itemWasherGroups", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public void NavigateToStorageTanksPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "itemStorageTanks", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }
        public void NavigateToWashersPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "itemWashers", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }
        public bool NavigateToStorageTanksPageAvailable()
        {
            return telerik.WaitForControl<HtmlControl>(myguiMap, "itemStorageTanks", Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }

        public bool IsNavigateToPlantSetupPageAvailable()
        {
            return telerik.WaitForControl<HtmlControl>(myguiMap, "itemPlantSetup",
                Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }

        public void IsNavigateToManualInputPage()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "menuManualInputsTab", Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }

        public bool IsNavigateToControlerSetupPageAvailable()
        {
            return telerik.WaitForControl<HtmlControl>(myguiMap, "itemControllerSetup", 
                Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
        }

        public HtmlControl NavigateToProductionChartsPage
        {
            get
            {
                return telerik.WaitForControl<HtmlControl>(myguiMap, "itemProductionChart", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl NavigateToChemicalChartPage
        {
            get
            {
                return telerik.WaitForControl<HtmlControl>(myguiMap, "itemChemicalChart", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl NavigateToManualInputPage
        {
            get
            {
                return telerik.WaitForControl<HtmlControl>(myguiMap, "menuManualInputsTab", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public void NavigateToVisualizationsTab()
        {
            telerik.WaitForControl<HtmlControl>(myguiMap, "menuVisualizationTab", Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

        public HtmlControl BreadCrumb
        {
            get
            {
                return telerik.WaitForControl<HtmlControl>(myguiMap, "MainMenubread", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        public HtmlControl GetVisualizationSubMenuItems
        {
            get
            {
                return telerik.WaitForControl<HtmlControl>(myguiMap, "menuVisualizationTab", Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }		

        public void LogOut()
        {
			WaitforLogin();
            telerik.WaitForControl<HtmlControl>(myguiMap, "spnLogout",
                                    Config.PageClassSettings.Default.MaxTimeoutValue).Click();
        }

		public void NavigateToUserProfile()
		{
			WaitforLogin();
			//telerik.WaitForControl<HtmlControl>(myguiMap, "lnkMyProfile",
			//						Config.PageClassSettings.Default.MaxTimeoutValue).Click();
			UserProfileLink.Click();
		}

		public bool WaitforLogin()
		{
			return telerik.WaitForControl<HtmlControl>(myguiMap, "spnWelcome",
								   Config.PageClassSettings.Default.MaxTimeoutValue).IsVisible();
		}
       
    }
}
