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
    public class FinishersTabPage : PageBase
    {
        private string guiMap;

        public FinishersTabPage(List<object> utilsList)
            : base(utilsList, "FinishersTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "FinishersTab.xml");
        }

        public HtmlAnchor FinishersTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabFinnisherContainer");
            }
        }

        public HtmlSpan AddFinisherGroup
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("addFinnisherGroup");
            }
        }

        public HtmlInputText FinisherNumber
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFinnisherNumber");
            }
        }

        public HtmlInputText FinisherGroupName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFinnisherGroupName");
            }
        }

        public HtmlSpan SaveFinisherGroup
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("AddFinisherGroupSave");
            }
        }

        public HtmlButton CancelFinisherGroup
        {
            get
            {
                return GetHtmlControl<HtmlButton>("AddFinisherGroupCancel");
            }
        }

        public HtmlControl FinisherGroups
        {
            get
            {
                return GetHtmlControl<HtmlControl>("FinisherGroups");
            }
        }

        public HtmlControl FinishersMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("finnisherErrorMsgDiv");
            }
        }

        public HtmlControl FinishersDuplicateMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("finnisherErrorMsg");
            }
        }

        public HtmlInputText FinnisherName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtFinnisherName");
            }
        }

        public HtmlSelect FinnisherType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlFinnisherType");
            }
        }

        public HtmlSpan SaveFinisher
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("AddFinisherSave");
            }
        }

        public HtmlButton CancelFinisher
        {
            get
            {
                return GetHtmlControl<HtmlButton>("AddFinisherCancel");
            }
        }

        private HtmlControl SelectedFinisherGroup(string strFinisherGroupName)
        {
            ICollection<Element> colGrps = FinisherGroups.ChildNodes;
            foreach (Element e in colGrps)
            {
                if (e.InnerText.Contains(strFinisherGroupName))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }

        public List<HtmlControl> GetAddFinisherGroupActionItems(string strFinisherGroupName)
        {
            List<HtmlControl> list = new List<HtmlControl>();
            HtmlControl ctrl = SelectedFinisherGroup(strFinisherGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[1]/span/a");
            foreach (Element e in eList)
            {
                list.Add(new HtmlControl(e));
            }
            return list;
        }

        public bool IsContainsFinisherGroup(string FinisherGroupName)
        {
            bool bStatus = false;
            ICollection<Element> colGrps = FinisherGroups.ChildNodes;
            foreach (Element e in colGrps)
            {
                if (e.InnerText.Contains(FinisherGroupName))
                {
                    bStatus = true;
                }
            }
            if (bStatus == true)
                return true;
            else
                return false;
        }

        public void AddingFinisherGroup(string finisherGroupName)
        {
            AddFinisherGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherGroupName.Focus();
            FinisherGroupName.TypeText(finisherGroupName);
            Thread.Sleep(2000);
            SaveFinisherGroup.Click();
        }

        public void EditingFinisherGroup(string finisherGroupName)
        {
            //AddFinisherGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherGroupName.Focus();
            FinisherGroupName.TypeText(finisherGroupName);
            Thread.Sleep(2000);
            SaveFinisherGroup.Click();
        }

        public void EditCancellingFinisherGroup(string finisherGroupName)
        {
            //AddFinisherGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherGroupName.Focus();
            FinisherGroupName.TypeText(finisherGroupName);
            Thread.Sleep(2000);
            CancelFinisherGroup.Click();
        }

        public void CancelingFinisherGroup(string finisherGroupName)
        {
            AddFinisherGroup.Click();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherGroupName.Focus();
            FinisherGroupName.TypeText(finisherGroupName);
            Thread.Sleep(2000);
            CancelFinisherGroup.Click();
        }

        public HtmlControl GetAddFinisherButton(string strFinisherGroupName)
        {
            HtmlControl ctrl = SelectedFinisherGroup(strFinisherGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div/a");
            foreach (Element e in eList)
            {
                return (new HtmlControl(e));
            }
            return null;
        }

        public bool IsContainsFinisher(string strFinisherGroupName, string strFinisher)
        {
            HtmlControl ctrl = SelectedFinisherGroup(strFinisherGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div[2]/table/tbody/tr");
            bool bStatus = false;
            foreach (Element e in eList)
            {
                if (e.InnerText.Contains(strFinisher))
                {
                    bStatus = true;
                }
            }
            if (bStatus == true)
                return true;
            else
                return false;
        }

        public HtmlControl GetSelectedFinisherRow(string strFinisherGroupName, string strFinisher)
        {
            List<HtmlControl> controls = new List<HtmlControl>();
            HtmlControl ctrl = SelectedFinisherGroup(strFinisherGroupName);
            ICollection<Element> eList = ctrl.Find.AllByXPath(@"//div[2]/div/div[2]/table/tbody/tr");
            foreach (Element e in eList)
            {
                if (e.InnerText.Contains(strFinisher))
                {
                    return (new HtmlControl(e));
                }
            }
            return null;
        }

        public List<HtmlControl> GetFinisherButtonControls(string strFinisherGroupName, string strFinisher)
        {
            int nCount = 0;
            List<HtmlControl> controls = new List<HtmlControl>();
            HtmlControl ctrl = GetSelectedFinisherRow(strFinisherGroupName, strFinisher);
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

        public void AddingFinisher(string finisherNumber, string finisherName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherNumber.Focus();
            FinisherNumber.TypeText(finisherNumber);
            FinnisherName.Focus();
            FinnisherName.TypeText(finisherName);
            FinnisherType.SelectByIndex(1);
            //FinnisherType.SelectByText(finisherType);
            SaveFinisher.Focus();
            SaveFinisher.Click();
        }

        public void EditingFinisher(string finisherNumber, string finisherName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherNumber.Focus();
            FinisherNumber.TypeText(finisherNumber);
            FinnisherName.Focus();
            FinnisherName.TypeText(finisherName);
            //FinnisherType.SelectByText(finisherType);
            FinnisherType.SelectByIndex(1);
            SaveFinisher.Focus();
            SaveFinisher.Click();
        }

        public void CancellingFinisher(string finisherNumber, string finisherName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherNumber.Focus();
            FinisherNumber.TypeText(finisherNumber);
            FinnisherName.Focus();
            FinnisherName.TypeText(finisherName);
            //FinnisherType.SelectByText(finisherType);
            FinnisherType.SelectByIndex(1);
            CancelFinisher.TypeEnterKey();
        }

        public void CancelingEditFinisher(string finisherNumber, string finisherName)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            Thread.Sleep(2000);
            FinisherNumber.Focus();
            FinisherNumber.TypeText(finisherNumber);
            FinnisherName.Focus();
            FinnisherName.TypeText(finisherName);
            //FinnisherType.SelectByText(finisherType);
            FinnisherType.SelectByIndex(1);
            CancelFinisher.TypeEnterKey();
        }

    }
}
