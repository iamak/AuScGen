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
    public class StorageTanksPage : PageBase
    {
        private string guiMap;

        public StorageTanksPage(List<object> utilsList)
            : base(utilsList, "StorageTanksTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "StorageTanksTab.xml");
        }

        public CommonControls.EcolabDataGrid StorageTanksTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "StorageTankTableGrid");
            }
        }

        public HtmlAnchor StorageTanksTab
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("tabStorageTanks");
            }
        }

        public HtmlAnchor AddStorageTanks
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("btnAddStorageTanks");
            }
        }
        
        public HtmlSpan BackToStorageTanks
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnBackToStorageTanks");
            }
        }

        public HtmlInputText TankName
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtTankName");
            }
        }

        public HtmlSelect ProductName
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlProductName");
            }
        }

        public HtmlInputText LowLevel
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLowLevel");
            }
        }

        public HtmlInputText Size
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtSize");
            }
        }

        public HtmlSelect ControllerName
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlControllerName");
            }
        }

        public HtmlInputText EmptyLevel
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtEmptyLevel");
            }
        }

        public HtmlInputText LevelDeviation
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLevelDeviation");
            }
        }

        public HtmlInputText CallibrationLevel
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtCallibrationLevel");
            }
        }

        public HtmlInputText InputType
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("ddlInputType");
            }
        }

        public HtmlSpan Save
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("btnSave");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlDiv Errordiv
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("errordiv");
            }
        }

        public HtmlSpan dialogMsg
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("dialogMsg");
            }
        }
        

        public void AddingStorageTank(string tankName, string lowLevel, string size, string emptyLevel, string levelDeviation)
        {
            AddStorageTanks.Click();
            Thread.Sleep(2000);
            TankName.TypeText(tankName);
            ProductName.SelectByIndex(1);
            LowLevel.TypeText(lowLevel);
            EmptyLevel.TypeText(emptyLevel);
            ControllerName.SelectByIndex(1);
            LevelDeviation.TypeText(levelDeviation);
            Size.SetText(size);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            EmptyLevel.Focus();
            EmptyLevel.MouseClick();
            EmptyLevel.TypeText("1");
            Save.Focus();
            Save.Click();
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            //Thread.Sleep(2000);
            
            
        }

        public void UpdatingStorageTank(string tankName, string size, string emptyLevel)
        {
            TankName.TypeText(tankName);
            //ProductName.SelectByIndex(1);
            //LowLevel.TypeText(lowLevel);
            //EmptyLevel.TypeText(emptyLevel);
            //ControllerName.SelectByIndex(1);
            //LevelDeviation.TypeText(levelDeviation);
            Size.SetText(size);
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            EmptyLevel.Focus();
            EmptyLevel.MouseClick();
            EmptyLevel.TypeText(emptyLevel);
            Save.Focus();
            Save.Click();
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            //Thread.Sleep(2000);


        }

        public void InLineEditingStorageTank(string tankName, string lowLevel, string size, string emptyLevel,
            string levelDeviation)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            TankName.Focus();
            TankName.TypeText(tankName);
            ControllerName.SelectByIndex(1);
            ProductName.SelectByIndex(1);
            EmptyLevel.TypeText(emptyLevel);
            LowLevel.TypeText(lowLevel);
            LevelDeviation.TypeText(levelDeviation);
            //Size.TypeText(size);
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
            //Thread.Sleep(2000);


        }

        public bool isRecordExist(string tankName)
        {
            ICollection<Element> eChild = StorageTanksTableGrid.Find.AllByXPath(@"//tbody/tr");
            foreach (Element e in eChild)
            {
                if (e.ChildNodes[3].InnerText == tankName)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
