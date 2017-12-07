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

namespace Ecolab.Pages
{
    public class DryerTabPage : PageBase
    {
        private string guiMap;

        public DryerTabPage(List<object> utilsList)
            : base(utilsList, "DryerTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "DryerTab.xml");
        }

        public HtmlControl DryerTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabDryerContainer");
            }
        }

        public HtmlControl AddDryerGroup
        {
            get
            {
                return GetHtmlControl<HtmlControl>("addDryerGroup");
            }
        }

        public HtmlInputText DryerGroupName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDryerGroupName");
            }
        }

        public HtmlButton SaveDryerGroup
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveGroup");
            }
        }

        public HtmlButton CancelDryerGroup
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlInputText DryerName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDryerName");
            }
        }
        public HtmlInputText DryerNumber
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDryerNumber");
            }
        }
        

        public HtmlSelect DryerType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlDryerType");
            }
        }

        public HtmlInputText DryerNominalLoad
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtDryerNominalLoad");
            }
        }

        public HtmlControl DryerNominalLoadUnit
        {
            get
            {
                return GetHtmlControl<HtmlControl>("dryerNominalLoadUnit");
            }
        }

        public HtmlButton SaveDryer
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveDryer");
            }
        }

        public HtmlButton CancelDryer
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlControl DryerErrorMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("dryerErrorMsg");
            }
        }

        public HtmlControl AddDryerMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("dryerAdditionMsg");
            }
        }

        public HtmlControl DryerGroups
        { 
            get
            {
                return GetHtmlControl<HtmlControl>("DryerGroups");                
            }
        }

        public bool IsContainsDryerGroup(string strDryerGroupName)
        {
            bool bStatus = false;
            ICollection<Element> colGrps = DryerGroups.ChildNodes;
            foreach(Element e in colGrps)
            {
                if (e.InnerText.Contains(strDryerGroupName))
                {
                    bStatus = true;
                }
            }
            if (bStatus == true)
                return true;
            else
                return false;          
        }

        public void AddingDryerGroup(string dryerGroupName)
        {
            AddDryerGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerGroupName.Focus();
            DryerGroupName.TypeText(dryerGroupName);
            Thread.Sleep(2000);
            SaveDryerGroup.TypeEnterKey();
        }

        public void EditDryerGroup(string dryerGroupName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerGroupName.Focus();
            DryerGroupName.TypeText(dryerGroupName);
            Thread.Sleep(2000);
            SaveDryerGroup.TypeEnterKey();
        }

        public void CancelingDryerGroup(string dryerGroupName)
        {
            AddDryerGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerGroupName.Focus();
            DryerGroupName.TypeText(dryerGroupName);
            CancelDryerGroup.TypeEnterKey();
        }

        public void CancelingEditDryerGroup(string dryerGroupName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerGroupName.Focus();
            DryerGroupName.TypeText(dryerGroupName);
            Thread.Sleep(2000);
            CancelDryerGroup.TypeEnterKey();
        }

        public void AddingDryer(string dryerNumber, string dryerName, string dryerNominalLoad)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerNumber.Focus();
            DryerNumber.TypeText(dryerNumber);
            DryerName.Focus();
            DryerName.TypeText(dryerName);
            DryerType.SelectByIndex(1);
            //DryerType.SelectByText(dryerType);
            DryerNominalLoad.TypeText(dryerNominalLoad);
            SaveDryer.TypeEnterKey();
        }

        public void EditDryer(string dryerNumber, string dryerName, string dryerNominalLoad)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerNumber.Focus();
            DryerNumber.TypeText(dryerNumber);
            DryerName.Focus();
            DryerName.TypeText(dryerName);
            //DryerType.SelectByText(dryerType);
            DryerType.SelectByIndex(1);
            DryerNominalLoad.TypeText(dryerNominalLoad);
            SaveDryer.TypeEnterKey();
        }

        public void CancellingDryer(string dryerNumber, string dryerName, string dryerNominalLoad)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerNumber.Focus();
            DryerNumber.TypeText(dryerNumber);
            DryerName.Focus();
            DryerName.TypeText(dryerName);
            //DryerType.SelectByText(dryerType);
            DryerType.SelectByIndex(1);
            DryerNominalLoad.TypeText(dryerNominalLoad);
            CancelDryer.TypeEnterKey();
        }

        public void CancelingEditDryer(string dryerNumber, string dryerName, string dryerNominalLoad)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            DryerNumber.Focus();
            DryerNumber.TypeText(dryerNumber);
            DryerName.Focus();
            DryerName.TypeText(dryerName);
            //DryerType.SelectByText(dryerType);
            DryerType.SelectByIndex(1);
            DryerNominalLoad.TypeText(dryerNominalLoad);
            CancelDryer.TypeEnterKey();
        }

        private HtmlControl SelectedDryerGroup(string strDryerGroupName)
        {
            ICollection<Element> colGrps = DryerGroups.ChildNodes;
            foreach(Element e in colGrps)
            {
                if(e.InnerText.Contains(strDryerGroupName))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }

        public HtmlControl GetAddDryerButton(string strDryerGroupName)
        {
            HtmlControl ctrl = SelectedDryerGroup(strDryerGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div/a");
            foreach(Element e in eList)
            {
                return (new HtmlControl(e));
            }
            return null;              
        }

        public List<HtmlControl> GetAddDryerGroupActionItems(string strDryerGroupName)
        {
            List<HtmlControl> list = new List<HtmlControl>();
            HtmlControl ctrl = SelectedDryerGroup(strDryerGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[1]/span/a");
            foreach (Element e in eList)
            {
                list.Add(new HtmlControl(e));
            }
            return list;
        }

        public bool IsContainsDryer(string strDryerGroupName, string strDryer)
        {
            HtmlControl ctrl = SelectedDryerGroup(strDryerGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div[2]/table/tbody/tr");
            bool bStatus = false;
            foreach (Element e in eList)
            {
                if (e.InnerText.Contains(strDryer))
                {
                    bStatus = true;
                }
            }
            if (bStatus == true)
                return true;
            else
                return false;
        }

        public HtmlControl GetSelectedDryerRow(string strDryerGroupName, string strDryer)
        {
            List<HtmlControl> controls = new List<HtmlControl>();
            HtmlControl ctrl = SelectedDryerGroup(strDryerGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div[2]/table/tbody/tr");
            foreach (Element e in eList)
            {
                if (e.InnerText.Contains(strDryer))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }

        public List<HtmlControl> GetDryerButtonControls(string strDryerGroupName, string strDryer)
        {
            int nCount = 0;
            List<HtmlControl> controls = new List<HtmlControl>();
            HtmlControl ctrl = GetSelectedDryerRow(strDryerGroupName, strDryer);
            ICollection<Element> cellList = ctrl.ChildNodes;
            foreach (Element cell in cellList)
            {
                if (cell.ChildNodes[0].TagName == "a")
                {
                    nCount = cell.Children.Count;
                    for (int i = 0; i <= nCount - 1; i++)
                    {
                        controls.Add((new HtmlControl(cell.ChildNodes[i].ChildNodes[0])));
                    }
                     return controls;
                }
            }
            return null;
        }

        public HtmlButton NoButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveNo");
            }         
        }

        public HtmlButton YesButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>(guiMap, "btnSaveYes");
            }
        }
    }
}
