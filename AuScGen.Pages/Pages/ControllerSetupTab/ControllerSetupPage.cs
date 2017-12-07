using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using ArtOfTest.WebAii.Controls.HtmlControls;
using Telerik.TestingFramework.Controls.KendoUI;
using System.Threading;

namespace Ecolab.Pages
{
    public class ControllerSetupPage : PageBase
    {
        private string guiMap;

        public ControllerSetupPage(List<object> utilsList)
            : base(utilsList, "ControllerGeneralSetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ControllerGeneralSetupPage.xml");
        }

        public HtmlControl AddControllerButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("btnAddController");
            }
        }

        public CommonControls.EcolabDataGrid ControllersTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "ControllerTabGridTable");
            }
        }

        public HtmlButton Save
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }

        public HtmlButton Cancel
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlControl ControllersTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabControllersList");
            }
        }

        public HtmlControl GeneralTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabGeneral");
            }
        }

        public HtmlControl AdvanceTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabAdvanced");
            }
        }

        public HtmlControl PumpsValvesTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabPumpList");
            }
        }

        public HtmlControl BackToControllers
        {
            get
            {
                return GetHtmlControl<HtmlControl>("backtoContollers");
            }
        }

        public HtmlControl ControllerAddMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("controllerAddMessage");
            }
        }

        public HtmlControl ControllerListMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("controllerListMessage");
            }
        }

        public KendoCalendar ControllerListPageCalender 
        { 
            get
            {
                return GetHtmlControl<KendoCalendar>("controllerListPageCalender");
            }            
        }

        public void DeleteController(string searchRowByValue)
        {
            List<CommonControls.EcolabDataGridItems> items = ControllersTabGrid.SelectedRows(searchRowByValue);

            //TODO: Change this after javascript popup is changed to html popup
            DialogHandler.ClickonOKButton();
            items.FirstOrDefault().GetButtonControls()[1].Click();
        }

        public void DeleteAllControllers()
        {
            List<CommonControls.EcolabDataGridItems> items;

            items = ControllersTabGrid.Rows;
            while (ControllersTabGrid.Rows.Count > 0)
            {
                items = ControllersTabGrid.Rows;
                DialogHandler.ClickonOKButton();
                items.FirstOrDefault().GetButtonControls()[1].Click();
            }

            DBAccess.UpdateData("Update TCD.ConduitController set IsDeleted='True' where IsDeleted in (select IsDeleted from TCD.ConduitController where IsDeleted='False')");
        }

        public bool IsAddControllerPresent
        {
            get
            {
                HtmlControl controllerTab = ControllersTab;
                return IsPresent<HtmlControl>("btnAddController");
            }
        }

        public bool IsControllerGridFirstColumnContains(string value)
        {
            ReadOnlyCollection<HtmlTableRow> rows = ControllersTabGrid.MainTable.BodyRows;

            List<HtmlTableRow> searchedRows = rows.Where(row => row.Cells[1].InnerText.Equals(value)).ToList<HtmlTableRow>();

            if (searchedRows.Count < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSaveButtonEnabled()
        {
            return (null == Save.BaseElement.GetAttribute("disabled"));
        }

        public void ExpandCalender()
        {
            string queryString = ".//*[@id='gridControllerSetupList']/div[3]/table/tbody/tr[1]/td[5]/span/span/span/span";

            WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue).DeskTopMouseClick();

        }

        public void SetDate(string day)
        {           
            //string defaultdate = "2014, 11, 18";
            ExpandCalender();
            ControllerListPageCalender.SelectDay(day);
        } 

        public void TrySelectBacktoController()
        {
            int counter = 0;
            Thread.Sleep(2000);
            BackToControllers.DeskTopMouseClick();
            while (null == ControllersTab && ++counter < 5)
            {
                BackToControllers.DeskTopMouseClick();
                Telerik.ActiveBrowser.RefreshDomTree();
            }
        }
    }
}
