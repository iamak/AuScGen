using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telerik.TestingFramework.Controls.KendoUI;

namespace Ecolab.Pages
{
    public class ControllerAdvancedSetupPage : PageBase
    {
        private string guiMap;

        public ControllerAdvancedSetupPage(List<object> utilsList)
            : base(utilsList, "ControllerAdvancedSetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ControllerAdvancedSetupPage.xml");
        }

        public KendoExtendedInput FactorsMultiplierAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtFactorsMultiplierAB");
            }
        }

        public KendoExtendedInput InjectionQuantityMultiplierAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtInjectionQuantityMultiplierAB");
            }
        }

        public KendoExtendedInput OZSecondMultiplierAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtOZSecondMultiplierAB");
            }
        }

        public KendoExtendedInput NumberOfChemicalValvesAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtNumberOfChemicalValvesAB");
            }
        }

        public HtmlInputText WebPortPasswordAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortPasswordAB");
            }
        }

        public HtmlInputText WebPortIPAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortIPAB");
            }
        }

        public HtmlInputText WebPortLoginAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortLoginAB");
            }
        }

        public HtmlControl ControllerName
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblControllerName");
            }
        }

        public KendoExtendedInput MaxWashFormulasBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtMaxWashFormulasBEC");
            }
        }

        public KendoExtendedInput MaxFormulaInjectionBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtMaxFormulaInjectionBEC");
            }
        }

        public KendoExtendedInput NumberOfChemicalValvesBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtNumberOfChemicalValvesBEC");
            }
        }

        public HtmlInputText WebPortPasswordBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortPasswordBEC");
            }
        }

        public HtmlInputText WebPortIPBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortIPBEC");
            }
        }

        public HtmlInputText WebPortLoginBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortLoginBEC");
            }
        }

    }
}
