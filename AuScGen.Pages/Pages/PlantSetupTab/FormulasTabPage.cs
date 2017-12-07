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

namespace Ecolab.Pages.Pages.PlantSetupTab
{
    public class FormulasTabPage : PageBase
    {
        private string guiMap;

        public FormulasTabPage(List<object> utilsList)
            : base(utilsList, "FormulasTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "FormulasTab.xml");
        }

        public HtmlControl FormulasTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabFormulas");
            }
        }

        public HtmlControl AddFormula
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddFormulas");
            }
        }

        public CommonControls.EcolabDataGrid FormulasTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "FormulasTableGrid");
            }
        }

        public HtmlControl FormulaTable
        {
            get
            {
                return GetHtmlControl<HtmlControl>("FormulasTableGrid");
            }
        }

        public HtmlInputText FormulaName
        {
             get
            {
                return GetHtmlControl<HtmlInputText>("txtFormulaName");
            }
            
        }

        public HtmlInputText CopyFormulaName
        {
             get
            {
                return GetHtmlControl<HtmlInputText>("txtcopyFormulaName");
            }
            
        }
        
        public HtmlControl TextileCategoryEcolab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlEcolabTextileCategoryAdd_listbox");
            }

        }

        public HtmlControl TextileSaturation
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlEcolabSaturationAdd_listbox");
            }

        }

        public HtmlControl ChainFormulaName
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlChainFormulaAdd_listbox");
            }

        }

        public HtmlControl TextileCategoryInternal
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlChainTextileCategoryAdd_listbox");
            }

        }

        public HtmlControl EditTextileCategoryEcolab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlEcolabTextileCategoryEdit_listbox");
            }

        }

        public HtmlControl EditTextileSaturation
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlEcolabSaturationEdit_listbox");
            }

        }

        public HtmlControl EditChainFormulaName
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlChainFormulaEdit_listbox");
            }

        }

        public HtmlControl EditTextileCategoryInternal
        {
            get
            {
                return GetHtmlControl<HtmlControl>("ddlChainTextileCategoryEdit_listbox");
            }

        }

        public HtmlInputText NumberofweightedPieces
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtaddNumberofweightedPieces");
            }

        }

        public HtmlInputText AddWeight
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtaddWeight");
            }

        }

        public HtmlInputText PieceWeight
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtPieceWeight");
            }

        }

        public HtmlInputText EditPieceWeight
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNumberofweightedPieces");
            }

        }

        public HtmlInputText EditWeight
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtWeight");
            }

        }

        public HtmlSpan Save
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSave");
            }

        }

        public HtmlButton CopySave
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpdatecopy");
            }

        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }

        }

        public HtmlControl SuccessMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("SuccessMsg");
            }
        }

        public HtmlInputText InLineFormulaName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("FormulaNameInLine");
            }
        }

        public void AddingFormula(string strFormulaName, string strEcolabTextileCategory, string strSaturation, string strChainFormulaName,
            string strTextileCategoryInternal, string strNumberofweightedPieces, string strAddWeight)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            FormulaName.Focus();
            FormulaName.TypeText(strFormulaName);
            ListItemClick(TextileCategoryEcolab, strEcolabTextileCategory);
            ListItemClick(TextileSaturation, strSaturation);
            ListItemClick(ChainFormulaName, strChainFormulaName);
            ListItemClick(TextileCategoryInternal, strTextileCategoryInternal);
            NumberofweightedPieces.Focus();
            NumberofweightedPieces.TypeText(strNumberofweightedPieces);
            AddWeight.Focus();
            AddWeight.TypeText(strAddWeight);
            Save.Focus();
            Save.Click();
            
        }

        public void EditingFormula(string strFormulaName, string strEcolabTextileCategory, string strSaturation, string strChainFormulaName,
            string strTextileCategoryInternal, string strNumberofweightedPieces, string strAddWeight)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            FormulaName.Focus();
            FormulaName.TypeText(strFormulaName);
            EditListItemClick(EditTextileCategoryEcolab, strEcolabTextileCategory);
            EditListItemClick(EditTextileSaturation, strSaturation);
            EditListItemClick(EditChainFormulaName, strChainFormulaName);
            EditListItemClick(EditTextileCategoryInternal, strTextileCategoryInternal);
            EditPieceWeight.Focus();
            EditPieceWeight.TypeText(strNumberofweightedPieces);
            EditWeight.Focus();
            EditWeight.TypeText(strAddWeight);
            Save.Focus();
            Save.Click();

        }

        public void EditingFormulaCancelFunctionality(string strFormulaName, string strEcolabTextileCategory, string strSaturation, string strChainFormulaName,
            string strTextileCategoryInternal, string strNumberofweightedPieces, string strAddWeight)

        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            FormulaName.Focus();
            FormulaName.TypeText(strFormulaName);
            EditListItemClick(EditTextileCategoryEcolab, strEcolabTextileCategory);
            EditListItemClick(EditTextileSaturation, strSaturation);
            EditListItemClick(EditChainFormulaName, strChainFormulaName);
            EditListItemClick(EditTextileCategoryInternal, strTextileCategoryInternal);
            EditPieceWeight.Focus();
            EditPieceWeight.TypeText(strNumberofweightedPieces);
            EditWeight.Focus();
            EditWeight.TypeText(strAddWeight);
            Cancel.Focus();
            Cancel.Click();

        }

        public void CopyingFormula(string strFormulaName, string strEcolabTextileCategory, string strSaturation, string strChainFormulaName,
            string strTextileCategoryInternal, string strNumberofweightedPieces, string strAddWeight)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            CopyFormulaName.Focus();
            CopyFormulaName.TypeText(strFormulaName);
            EditListItemClick(EditTextileCategoryEcolab, strEcolabTextileCategory);
            EditListItemClick(EditTextileSaturation, strSaturation);
            EditListItemClick(EditChainFormulaName, strChainFormulaName);
            EditListItemClick(EditTextileCategoryInternal, strTextileCategoryInternal);
            EditPieceWeight.Focus();
            EditPieceWeight.TypeText(strNumberofweightedPieces);
            EditWeight.Focus();
            EditWeight.TypeText(strAddWeight);
            CopySave.Focus();
            CopySave.Click();

        }

        public void ListItemClick(HtmlControl control, string strText)
        {
            Thread.Sleep(3000);
            control.Focus();
            ICollection<Element> ele = control.ChildNodes;
            foreach(Element e in ele)
            {
                if(e.InnerText == strText)
                {
                    (new HtmlControl(e)).Click();
                    MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                    break;
                }
            }
        }

        public void EditListItemClick(HtmlControl control, string strText)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(3000);
            control.Focus();
            ICollection<Element> ele = control.ChildNodes;
            foreach (Element e in ele)
            {
                if (e.InnerText == strText)
                {
                    (new HtmlControl(e)).Click();
                    MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                    break;
                }
            }
        }

        public bool isRecordExist(string strFormulaName)
        {
            ICollection<Element> eChild = FormulaTable.Find.AllByXPath(@"//tbody/tr");
            foreach (Element e in eChild)
            {
                if(e.ChildNodes[1].InnerText == strFormulaName)
                {
                    return true;
                }
            }
            return false;  
        }

        public void InLineEditingFormula(string strFormulaName)
        {      
            InLineFormulaName.Focus();
            InLineFormulaName.SetText(strFormulaName);
            
        }



        
    }
}
