using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;
using System.Windows.Forms;
using ArtOfTest.WebAii.Core;

namespace Ecolab.Pages.Pages
{
    public class WashersTunnelGeneralPage : PageBase
    {
        private string guiMap;

        public WashersTunnelGeneralPage(List<object> utilsList)
            : base(utilsList, "WashersTunnelGeneralTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "WashersTunnelGeneralTab.xml");
        }

        public CommonControls.EcolabDataGrid WashersTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "WashersTableGrid");
            }
        }

        public HtmlButton AddNewWasher
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnAddNewWasher");
            }
        }

        public HtmlInputText Name
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtName");
            }
        }

        public HtmlSelect Controller
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlController");
            }
        }

        public HtmlSelect WasherMode
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherMode");
            }
        }

        public HtmlInputText NoOfCompartments
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNoOfCompartments");
            }
        }

        public HtmlSelect Press
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlPress");
            }
        }

        public HtmlInputText PlantWasher
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtPlantWasher");
            }
        }

        public HtmlSelect Model
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlModel");
            }
        }

        public HtmlAnchor BackToWasher
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("bckWasherbutton");
            }
        }

        public HtmlInputText MaxLoad
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtMaxLoad");
            }
        }

        public HtmlInputText NoOfTanks
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNoOfTanks");
            }
        }

        public HtmlSelect TransferType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlTransferType");
            }
        }

        public HtmlInputText ProgramNo
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtProgramNo");
            }
        }

        public HtmlButton SaveTunnel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveTunnel");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlTextArea Description
        {
            get
            {
                return GetHtmlControl<HtmlTextArea>("txtDescription");
            }
        }

        public HtmlControl divMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divMessage");
            }
        }

        public void AddingWahser(string WahserName, string PWasher, string PNumber)
        {
            AddNewWasher.Click();
            Thread.Sleep(2000);
            Name.Focus();
            Name.TypeText(WahserName);
            Thread.Sleep(2000);
            Controller.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            //WasherMode.SelectByIndex(1);
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NoOfCompartments.Focus();
            NoOfCompartments.TypeText("0");
            Press.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Model.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            PlantWasher.TypeText(PWasher);
            MaxLoad.TypeText("0");
            NoOfTanks.TypeText("0");
            TransferType.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ProgramNo.TypeText(PNumber);
            Description.TypeText("Test Washer Creation");
            SaveTunnel.Focus();
            SaveTunnel.Click();
        }

        public void UpdatingWasher(string WahserName, string PWasher, string PNumber)
        {
            Name.Focus();
            Name.TypeText(WahserName);
            Thread.Sleep(2000);
            Controller.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            //WasherMode.SelectByIndex(1);
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NoOfCompartments.Focus();
            NoOfCompartments.TypeText("0");
            Press.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Model.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            PlantWasher.TypeText(PWasher);
            MaxLoad.TypeText("0");
            NoOfTanks.TypeText("0");
            TransferType.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ProgramNo.TypeText(PNumber);
            Description.TypeText("Test Washer Creation");
            SaveTunnel.Focus();
            SaveTunnel.Click();
        }

        public void CancellingAddWahser(string WahserName, string PWasher, string PNumber)
        {
            AddNewWasher.Click();
            Thread.Sleep(2000);
            Name.Focus();
            Name.TypeText(WahserName);
            Thread.Sleep(2000);
            Controller.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            //WasherMode.SelectByIndex(1);
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NoOfCompartments.Focus();
            NoOfCompartments.TypeText("0");
            Press.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Model.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            PlantWasher.TypeText(PWasher);
            MaxLoad.TypeText("0");
            NoOfTanks.TypeText("0");
            TransferType.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ProgramNo.TypeText(PNumber);
            Description.TypeText("Test Washer Creation");
            SaveTunnel.Focus();
            SaveTunnel.Click();
        }

    }
}
