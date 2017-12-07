using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Ecolab.CommonUtilityPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecolab.Pages
{
    public class LabourCostTabPage : PageBase
    {
        private string guiMap;
        /// <summary>
        /// reading guimap OR objects
        /// </summary>
        /// <param name="TelerikPlugin"></param>
        public LabourCostTabPage(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "LabourCostTab.xml");
        }

        public LabourCostTabPage(List<object> utilsList)
            : base(utilsList, "LabourCostTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "LabourCostTab.xml");
        }


        /// <summary>
        /// Labour Cost Table Grid
        /// </summary>
        public CommonControls.EcolabDataGrid LabourCostGridTable
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "labourCostGridTable");
            }
        }
        /// <summary>
        ///Gets Labour Cost tab controls
        /// </summary>
        public HtmlControl LaborCostTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabLaborCost");
            }
        }
        /// <summary>
        /// Gets language preferred control from general tab
        /// </summary>
        public HtmlSelect LanguagePreferred
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("SelectPreferredLanguage");
            }
        }
        /// <summary>
        /// Gets save button control
        /// </summary>
        public HtmlButton GeneralTabSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpload");
            }
        }
        /// <summary>
        /// Gets the control of success MSG.
        /// </summary>
        /// <returns></returns>
        public HtmlControl SuccessMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SuccessValidationMessage");
            }
        }

        public HtmlInputControl NormalCost
        {
            get
            {
                return GetHtmlControl<HtmlInputControl>("normalCost");
            }
        }
        public HtmlControl TemporaryCost
        {
            get
            {
                return GetHtmlControl<HtmlControl>("temporaryCost");
            }
        }
        public HtmlSpan BtnSave
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSave");
            }
        }
        public HtmlButton BtnCancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }
        /// <summary>
        /// Gets the No Button control from the popup on Cancel functionality.
        /// </summary>
        public HtmlButton BtnNo
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnNoPopUp");
            }
        }
        public void AddLaborCost_Working(string[] setLaborCost)
        {
            MouseKeyBoardSimulator objNumeric = new MouseKeyBoardSimulator();
            Random randomNumber = new Random();
            for (int i = 1; i <= LabourCostGridTable.Rows.Count - 1; i++)
            {
                Thread.Sleep(2000);
                string newLabourCost = randomNumber.Next(1, 50).ToString();
                Element inlineLaborCost = LabourCostGridTable.Rows[i].GetEditableControls()[1].ChildNodes[1];
                (new HtmlControl(inlineLaborCost)).TypeText(setLaborCost[0]);
                Thread.Sleep(2000);
                BtnCancel.Focus();
                BtnCancel.MouseClick();

                //HtmlControl ctrlSetValue = new HtmlControl(inlineLaborCost);
                //ctrlSetValue.Focus();
                //ctrlSetValue.ExtendedMouseClick();
                ////ctrlSetValue.Text = string.Empty;
                //objNumeric.SetNumeric(newLabourCost);
                //KeyBoardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                //ctrlSetValue.TypeText(newLabourCost);
            }
        }

        public void AddLaborCost(string[] setLaborCost)
        {
            NormalCost.TypeText(setLaborCost[0]);
            TemporaryCost.TypeText(setLaborCost[1]);
            BtnSave.Focus();
            BtnSave.DeskTopMouseClick();
        }

        public void CancelLaborCost(string[] setLaborCost)
        {
            MouseKeyBoardSimulator objNumeric = new MouseKeyBoardSimulator();
            HtmlInputControl ctrls = NormalCost;
            ctrls.Focus();
            ctrls.Value = string.Empty;
            ctrls.MouseClick();
            objNumeric.SetNumeric(setLaborCost[0]);
            BtnCancel.Focus();
            BtnCancel.DeskTopMouseClick();
            Thread.Sleep(3000);
        }
    }
}
