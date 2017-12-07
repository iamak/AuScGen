using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using Ecolab.Pages.CommonControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecolab.Pages.Pages.PlantSetupTab
{
    public class SensorsTabPage:PageBase
    {
         /// <summary>
        /// The GUI map
        /// </summary>
        private string guiMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorsTabPage"/> class.
        /// </summary>
        /// <param name="TelerikPlugin">The telerik plugin.</param>
        public SensorsTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "SensorTab.xml");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorsTabPage"/> class.
        /// </summary>
        /// <param name="utilsList">The utils list.</param>
        public SensorsTabPage(List<object> utilsList)
            : base(utilsList, "SensorTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "SensorTab.xml");
        }

        /// <summary>
        /// Gets the sensor tab grid.
        /// </summary>
        /// <value>
        /// The sensor tab grid.
        /// </value>
        public CommonControls.EcolabDataGrid SensorTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "SensorTabGrid");
            }
        }

        /// <summary>
        /// My top main menu
        /// </summary>
        private CommonControls.MainMenu myTopMainMenu;

        /// <summary>
        /// Gets the top main menu.
        /// </summary>
        /// <value>
        /// The top main menu.
        /// </value>
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
        /// Gets the sensor tab.
        /// </summary>
        /// <value>
        /// The sensor tab.
        /// </value>
        public HtmlControl SensorTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabSensors");
            }
        }

        /// <summary>
        /// Gets the add sensor button.
        /// </summary>
        /// <value>
        /// The add sensor button.
        /// </value>
        public HtmlControl AddSensorButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddSensor");
            }
        }

        /// <summary>
        /// Gets the name of the sensor.
        /// </summary>
        /// <value>
        /// The name of the sensor.
        /// </value>
        public HtmlInputText SensorName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtAddSensorName");
            }
        }

        /// <summary>
        /// Gets the name of the edit sensor.
        /// </summary>
        /// <value>
        /// The name of the edit sensor.
        /// </value>
        public HtmlInputText EditSensorName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEditSensorName");
            }
        }

        /// <summary>
        /// Gets the name of the Inline Edit Sensor name
        /// </summary>
        public HtmlInputText InlineEditSensorName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtInlinEditSensorName");
            }
        }

        /// <summary>
         /// Ins the line.
         /// </summary>
         /// <param name="text">The text.</param>
        public void InLineSensorEdit(string text)
         {
             GetHtmlControl<HtmlInputText>("txtInlinEditSensorName").SetText(text);
         }

        /// <summary>
        /// Gets the name of the in line sensor.
        /// </summary>
        /// <value>
        /// The name of the in line sensor.
        /// </value>
        public HtmlInputText InLineSensorName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtInlinEditSensorName");
            }
        }
   
        /// <summary>
        /// Gets the type of the sensor.
        /// </summary>
        /// <value>
        /// The type of the sensor.
        /// </value>
        public HtmlSelect SensorType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlSensorType");
            }
        }

        /// <summary>
        /// Gets the type of the sensor.
        /// </summary>
        /// <value>
        /// The type of the sensor.
        /// </value>
        public HtmlSelect SensorLocation
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlSensorLocation");
            }
        }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        /// <value>
        /// The name of the machine.
        /// </value>
        public HtmlSelect MachineName
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlAddMachineName");
            }
        }

        /// <summary>
        /// Gets the type of the output.
        /// </summary>
        /// <value>
        /// The type of the output.
        /// </value>
        public HtmlSelect OutputType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlAddOutPutType");
            }
        }

        /// <summary>
        /// Gets the calibration.
        /// </summary>
        /// <value>
        /// The calibration.
        /// </value>
        public HtmlInputText Calibration
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtAddCalibration");
            }
        }

        /// <summary>
        /// Gets the uo m.
        /// </summary>
        /// <value>
        /// The uo m.
        /// </value>
        public HtmlSelect UoM
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlAddUoM");
            }
        }

        /// <summary>
        /// Gets the sensor controller.
        /// </summary>
        /// <value>
        /// The sensor controller.
        /// </value>
        public HtmlSelect SensorController
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlAddSensorController");
            }
        }

        /// <summary>
        /// Gets the edit sensor controller.
        /// </summary>
        /// <value>
        /// The edit sensor controller.
        /// </value>
        public HtmlControl EditSensorController
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlEditSensorController");
            }
        }

        /// <summary>
        /// Gets the chemical.
        /// </summary>
        /// <value>
        /// The chemical.
        /// </value>
        public HtmlSelect Chemical
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlAddChemicalForChart");
            }
        }

        /// <summary>
        /// Gets the analog input.
        /// </summary>
        /// <value>
        /// The analog input.
        /// </value>
        public HtmlControl AnalogInput
        {
            get
            {
                return GetHtmlControl<HtmlControl>("txtAddAnalogueImputNumber");
            }
        }

        /// <summary>
        /// Gets the save.
        /// </summary>
        /// <value>
        /// The save.
        /// </value>
        public HtmlButton btnSave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpdate");
            }
        }

        /// <summary>
        /// Gets the BTN save.
        /// </summary>
        /// <value>
        /// The BTN save.
        /// </value>
        public HtmlSpan btnEditSave
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnEditSave");
            }
        }

        /// <summary>
        /// Gets the meter added success.
        /// </summary>
        /// <value>
        /// The meter added success.
        /// </value>
        public HtmlControl SensorAddedSuccess
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblAddedSuccess");
            }
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public HtmlControl ErrorMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblErrorMsg");
            }

        }

        /// <summary>
        /// Adds the sensor and verify.
        /// </summary>
        /// <returns></returns>
        public bool AddSensorAndVerify()
        {
            btnSave.Focus();
            btnSave.Click();

            if (null == SensorAddedSuccess)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the BTN cancel.
        /// </summary>
        /// <value>
        /// The BTN cancel.
        /// </value>
        public HtmlButton btnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        /// <summary>
        /// Gets the label add machine compartment.
        /// </summary>
        /// <value>
        /// The label add machine compartment.
        /// </value>
        public HtmlControl LabelAddMachineCompartment
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lblAddMachineCompartment");
            }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public HtmlSelect Language
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLanguages");
            }
        }

        /// <summary>
        /// Gets the general tab save.
        /// </summary>
        /// <value>
        /// The general tab save.
        /// </value>
        public HtmlButton GeneralTabSave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("GeneralbtnSave");
            }
        }

        /// <summary>
        /// Gets the add popup title.
        /// </summary>
        /// <value>
        /// The add popup title.
        /// </value>
        public HtmlControl AddPopupTitle
        {
            get
            {
                return GetHtmlControl<HtmlControl>("popupEditor_wnd_title");
            }
        }

        /// <summary>
        /// Gets the bread crumb.
        /// </summary>
        /// <value>
        /// The bread crumb.
        /// </value>
        public HtmlControl BreadCrumb
        {
            get
            {
                return TopMainMenu.BreadCrumb;
            }
        }

        /// <summary>
        /// Gets the bread crumb.
        /// </summary>
        /// <returns></returns>
        public string GetBreadCrumb()
        {
            string strbreadCrumb = string.Empty;
            List<string> myList = new List<string>();
            ICollection<Element> ctrl = BreadCrumb.ChildNodes;
            foreach(Element e in ctrl)
            {
                myList.Add(e.InnerText.Trim());                
            }
            strbreadCrumb = myList[0] + "->" + myList[1] + "->" + myList[2];
            return strbreadCrumb;
        }

        public CommonControls.EcolabDataGrid UsersRolesSensorsTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "UserRolesSensorsTabGridTable");
            }
        }
        public HtmlControl EditSensorsLabel
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SensorsEditLabel");
            }
        }
        public HtmlControl ViewSensorsLabel
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SensorsViewLabel");
            }
        }
        public HtmlControl UtilityContainerSubMenu
        {
            get
            {
                return GetHtmlControl<HtmlControl>("UtilityContainerTabs");
            }
        }
        
    }
}
