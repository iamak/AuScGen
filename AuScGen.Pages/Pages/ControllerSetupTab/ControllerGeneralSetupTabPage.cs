using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using System.Collections.ObjectModel;

namespace Ecolab.Pages
{
    public class ControllerGeneralSetupTabPage : PageBase
    {
        private string guiMap;

        public ControllerGeneralSetupTabPage(List<object> utilsList)
            : base(utilsList, "ControllerGeneralSetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ControllerGeneralSetupPage.xml");
        }

        public HtmlSelect ControllerModel
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlControllerModel");
            }
        }

        public HtmlSelect ControllerType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlControllerType");
            }
        }

        public HtmlInputText OPCServerUltraxAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtOPCServerUltraxAB");
            }
        }

        public HtmlInputText OPCObjectUltraxAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtOPCObjectUltraxAB");
            }
        }

        public KendoExtendedInput ControllerNumberUltraxAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtControllerNumberUltraxAB");
            }
        }

        public HtmlInputText IPAddressAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtIPAddressAB");
            }
        }

        public HtmlSelect ControllerVersionUltraxAB
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("txtControllerVersionUltraxAB");
            }
        }

        public KendoExtendedInput ComPortNumberUltraxAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtComPortNumberUltraxAB");
            }
        }

        public KendoExtendedInput PreflushTimeUltraxAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtPreflushTimeUltraxAB");
            }
        }

        public KendoExtendedInput PostflushUltraxAB
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtPostflushUltraxAB");
            }
        }

        public HtmlInputText DDEDriverUltraxAB
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDDEDriverUltraxAB");
            }
        }

        public HtmlInputText ControllerNameUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtControllerNameUltraxBEC");
            }
        }

        public HtmlInputText WebPortLoginUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortLoginUltraxBEC");
            }
        }

        public HtmlInputText WebPortIPUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortIPUltraxBEC");
            }
        }

        public HtmlInputText WebPortPasswordUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWebPortPasswordUltraxBEC");
            }
        }

        public HtmlInputText SerialNumberUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtSerialNumberUltraxBEC");
            }
        }

        public HtmlInputText AMSNetIDAddressUltraxBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtAMSNetIDAddressUltraxBEC");
            }
        }

        public KendoExtendedInput ControllerNumberUltraxBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtControllerNumberUltraxBEC");
            }
        }

        public KendoExtendedInput PreflushTimeUltraxBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtPreflushTimeUltraxBEC");
            }
        }

        public KendoExtendedInput PostflushTimeUltraxBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtPostflushTimeUltraxBEC");
            }
        }

        public HtmlInputText AMSNetIDmyControlBEC
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtAMSNetIDmyControlBEC");
            }
        }

        public KendoExtendedInput ControllerNumbermyControlBEC
        {
            get
            {
                return GetHtmlControl<KendoExtendedInput>("txtControllerNumbermyControlBEC");
            }
        }

        public string GetUltraxABControllerNumber
        {
            get
            {
                return ControllerNumberUltraxAB.BaseElement.GetAttribute("value").Value;
            }
        }

        public string GetUltraxBECControllerNumber 
        { 
            get
            {
                return ControllerNumberUltraxBEC.BaseElement.GetAttribute("value").Value;
            }
        }

        public string GetUltraxBECPreflushTime
        {
            get
            {
                return PreflushTimeUltraxBEC.BaseElement.GetAttribute("value").Value;
            }
        }

        public string GetUltraxBECPostflushTime 
        { 
            get
            {
                return PostflushTimeUltraxBEC.BaseElement.GetAttribute("value").Value;
            }            
        }

        public ReadOnlyCollection<string> GetControllerModelList()
        {
            ReadOnlyCollection<HtmlOption> options = ControllerModel.Options;
            List<HtmlOption> controllers = options.Where(option => options.IndexOf(option) > 0).ToList();
            List<string> listOfControllers = new List<string>();

            foreach(HtmlOption controller in controllers)
            {
                listOfControllers.Add(controller.Text);
            }

            return listOfControllers.AsReadOnly();
        }

    }
}
