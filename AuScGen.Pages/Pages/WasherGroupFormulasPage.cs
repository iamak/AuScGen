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
    public class WasherGroupFormulasPage : PageBase
    {

        private string guiMap;

        public WasherGroupFormulasPage(List<object> utilsList)
            : base(utilsList, "WasherGroupFormulasTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "WasherGroupFormulasTab.xml");
        }

        public CommonControls.EcolabDataGrid FormulasTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "FormulasTableGrid");
            }
        }

        public CommonControls.EcolabDataGrid WasherGroupsTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "WasherGroupsTableGrid");
            }
        }

        public HtmlAnchor WashergroupTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabWashergroup");
            }
        }

        public HtmlSpan AddWasherGroup
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("AddWasherGroup");
            }
        }

        public HtmlAnchor WashersSetupTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabWashersSetup");
            }
        }

        public HtmlAnchor FormulaTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabFormula");
            }
        }

        public HtmlInputText GroupNumber
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtGroupNumber");
            }
        }

        public HtmlInputText GroupName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtGroupName");
            }
        }

        public HtmlSelect WasherGroupType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlWasherGroupType");
            }
        }

        public HtmlButton Save
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }

        public HtmlButton WasherGroupSaveSave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnWasherGroupSaveSave");
            }
        }

        public HtmlControl divMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divMessage");
            }
        }

        public HtmlControl divMessageCreate
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divMessageCreate");
            }
        }

        public HtmlAnchor AddFormula
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("AddFormula");
            }
        }

        public HtmlSpan BacktoWasherGroups
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnBacktoWasherGroups");
            }
        }

        public HtmlInputText Number
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNumber");
            }
        }

        public HtmlSelect FormulaName
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlFormulaName");
            }
        }

        public HtmlInputText NominalLoad
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNominalLoad");
            }
        }

        public HtmlInputText LoadsPerMonth
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLoadsPerMonth");
            }
        }

        public HtmlInputText ExtraTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtExtraTime");
            }
        }

        public void AddingWasherGroup(string groupNumber, string groupName)
        {
            GroupNumber.TypeText(groupNumber);
            GroupName.TypeText(groupName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GroupName.SetText(groupName);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            GroupNumber.MouseClick();
            GroupNumber.TypeText(groupNumber);
            GroupName.TypeText(groupName);
            WasherGroupSaveSave.Focus();
            WasherGroupSaveSave.Click();
        }

        public void AddingFormula(string number, string nominalLoad, string loadsPerMonth, string extraTime)
        {
            Number.TypeText(number);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            FormulaName.Focus();
            FormulaName.SelectByIndex(1);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NominalLoad.TypeText(nominalLoad);
            LoadsPerMonth.TypeText(loadsPerMonth);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ExtraTime.TypeText(extraTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NominalLoad.MouseClick();
            NominalLoad.SetText("0");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ExtraTime.MouseClick();
            NominalLoad.TypeText(nominalLoad);
            Save.Focus();
            Save.Click();
        }

        public void UpdatingFormula(string nominalLoad, string loadsPerMonth, string extraTime)
        {
            NominalLoad.Focus();
            NominalLoad.TypeText(nominalLoad);
            LoadsPerMonth.TypeText(loadsPerMonth);
            NominalLoad.SetText("0");
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            ExtraTime.MouseClick();
            ExtraTime.TypeText(extraTime);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            NominalLoad.TypeText(nominalLoad);
            Save.Focus();
            Save.Click();
        }

        public void InLineEditingFormula(string nominalLoad, string loadsPerMonth, string extraTime)
        {
            NominalLoad.Focus();
            NominalLoad.TypeText(nominalLoad);
            LoadsPerMonth.TypeText(loadsPerMonth);
            ExtraTime.TypeText(extraTime);
        }
    }

}
