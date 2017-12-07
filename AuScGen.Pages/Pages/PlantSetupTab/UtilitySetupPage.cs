using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using ArtOfTest.WebAii.Core;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.ObjectModel;

namespace Ecolab.Pages.Pages.PlantSetupTab
{
    public class UtilitySetupPage : PageBase
    {
        private string guiMap;

        public UtilitySetupPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin) : base(TelerikPlugin, "UtilitySetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "UtilitySetupPage.xml");
        }

		public UtilitySetupPage(List<object> utilsList) : base(utilsList, "UtilitySetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "UtilitySetupPage.xml");
        }

		public ReadOnlyCollection<HtmlControl> WaterfactorTempControls 
		{ 
			get
			{
				string queryString = ".//*[@id='tabUtilitiesContainer']/div[1]/div/div/div/div[2]/div/span";

				Telerik.ActiveBrowser.RefreshDomTree();
				return WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					return Telerik.Find.AllByXPath<HtmlControl>(queryString);
				}, Config.PageClassSettings.Default.MaxTimeoutValue);
			}
		}

		public ReadOnlyCollection<HtmlControl> WaterfactorPriceControls
		{
			get
			{
				string queryString = ".//*[@id='tabUtilitiesContainer']/div[1]/div/div/div/div[3]/div/span";

				Telerik.ActiveBrowser.RefreshDomTree();
				return WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					return Telerik.Find.AllByXPath<HtmlControl>(queryString);
				}, Config.PageClassSettings.Default.MaxTimeoutValue);
			}
		}

		public ReadOnlyCollection<HtmlControl> OtherenergyOilPriceControls
		{
			get
			{
				string queryString = ".//*[@id='txtGasPrice']/../span/span/span[1]";

				Telerik.ActiveBrowser.RefreshDomTree();
				return WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					return Telerik.Find.AllByXPath<HtmlControl>(queryString);
				}, Config.PageClassSettings.Default.MaxTimeoutValue);
			}
		}

		public ReadOnlyCollection<HtmlControl> OtherenergyElectricityControls
		{
			get
			{
				string queryString = ".//*[@id='txtElectricityTarrif']/../span";

				Telerik.ActiveBrowser.RefreshDomTree();
				return WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					return Telerik.Find.AllByXPath<HtmlControl>(queryString);
				}, Config.PageClassSettings.Default.MaxTimeoutValue);
			}
		}

		/// <summary>
		/// Gets Button Save for Add control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnSaveAdd
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnSave");
			}
		}

		/// <summary>
		/// Gets BUtton Cancel control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnCancelAdd
		{
			get
			{
				return GetHtmlControl<HtmlButton>("btnCancel");
			}
		}

		/// <summary>
		/// Gets Button Tab Utilities control
		/// </summary>
		/// <returns></returns>
		public HtmlControl BtnTabUtilities
		{
			get
			{
				return GetHtmlControl<HtmlControl>("tabUtilities");
			}
		}

		public bool WaterFactorTemp(string waterfactortemp, string otherenergyoil)
		{
			foreach(HtmlControl waterTemp in WaterfactorTempControls)
			{
				int count = WaterfactorTempControls.Count;
				if (waterTemp.BaseElement.InnerText.Equals(waterfactortemp) || waterTemp.BaseElement.InnerText.Equals(otherenergyoil))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		public bool WaterFactorPrice(string waterfactorprice, string otherenergyelectricity)
		{
			foreach (HtmlControl waterTemp in WaterfactorPriceControls)
			{
				if (waterTemp.BaseElement.InnerText.Equals(waterfactorprice) || waterTemp.BaseElement.InnerText.Equals(otherenergyelectricity))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Gets Button Yes control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnimbYes
		{
			get
			{
				return GetHtmlControl<HtmlButton>("imbYes");
			}
		}

		/// <summary>
		/// Gets GroupType control
		/// </summary>
		/// <returns></returns>
		public HtmlButton BtnimbNo
		{
			get
			{
				return GetHtmlControl<HtmlButton>("imbNo");
			}
		}

		/// <summary>
		/// Gets First Name from User Profile
		/// </summary>
		/// <returns></returns>
		public HtmlInputText TxtColdSoftTemp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtColdSoftTemp");
			}
		}

		public HtmlSpan SuccessMessage
		{
			get
			{
				return GetHtmlControl<HtmlSpan>("successMsg");
			}
		}

		public HtmlInputText TxtColdTemp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtColdTemp");
			}
		}

		

		public HtmlInputText TxtTemperedTemp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtTemperedTemp");
			}
		}

		public HtmlInputText TxtTemperedPrice
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtTemperedPrice");
			}
		}

		public HtmlInputText TxtWastePrice
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtWastePrice");
			}
		}

		public HtmlSpan DdlGasOilProperties
		{
			get
			{
				return GetHtmlControl<HtmlSpan>("ddlGasOilProperties");
			}
		}

		public HtmlInputText TxtFreeWater1Temp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater1Temp");
			}
		}

		public HtmlInputText TxtFreeWater1Price
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater1Price");
			}
		}

		public HtmlInputText TxtFreeWater2Temp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater2Temp");
			}
		}

		public HtmlInputText TxtFreeWater2Price
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater2Price");
			}
		}

		public HtmlInputText TxtFreeWater3Temp
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater3Temp");
			}
		}

		public HtmlInputText TxtFreeWater3Price
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtFreeWater3Price");
			}
		}

		public HtmlInputText TxtEvaporationFactor
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtEvaporationFactor");
			}
		}

		public HtmlInputText TxtGasPrice
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtGasPrice");
			}
		}

		public HtmlInputText TxtElectricityTarrif
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtElectricityTarrif");
			}
		}

		public HtmlInputText TxtRewashFactor
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtRewashFactor");
			}
		}

		public HtmlInputText TxtSteamPercentage
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtSteamPercentage");
			}
		}

		public HtmlInputText TxtBoilerPercentage
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtBoilerPercentage");
			}
		}

		public HtmlInputText TxtStackPercentage
		{
			get
			{
				return GetHtmlControl<HtmlInputText>("txtStackPercentage");
			}
		}

		public HtmlSpan DdlBoilerType
		{
			get
			{
				return GetHtmlControl<HtmlSpan>("ddlBoilerType");
			}
		}

		public void AddUtilityDetails()
		{
			Random rand = new Random();
			string ranColdTemp = Convert.ToString(rand.Next(10, 99));
			Thread.Sleep(2000);
			TxtTemperedTemp.Focus();
			TxtTemperedTemp.MouseClick();
			TxtTemperedTemp.Text = string.Empty;
			TxtTemperedTemp.SetText(ranColdTemp);
			TxtTemperedTemp.Text = string.Empty;
			TxtTemperedTemp.MouseClick();
			TxtTemperedTemp.TypeText(ranColdTemp);
			Thread.Sleep(2000);
			TxtEvaporationFactor.Focus();
			TxtEvaporationFactor.TypeText("5");
			Thread.Sleep(2000);
			BtnSaveAdd.Focus();
			BtnSaveAdd.DeskTopMouseClick();
		}

	}
}
