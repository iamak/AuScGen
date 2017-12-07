using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;
using System.Threading;
using ArtOfTest.WebAii.ObjectModel;
using Ecolab.CommonUtilityPlugin;

namespace Ecolab.Pages
{
    public class ShiftTabPage : PageBase
    {

        private string guiMap;

        public ShiftTabPage(List<object> utilsList)
            : base(utilsList, "ShiftTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ShiftTab.xml");
        }

        public HtmlControl ShiftTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabShiftLabor");
            }
        }

        public HtmlButton AddShift
        {
            get
            {
                return GetHtmlControl<HtmlButton>("addShift");
            }
        }

        public HtmlInputText ShiftNameTextBox
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("shiftName");
            }
        }

        public HtmlInputText TargetProductionTextBox
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("targetProduction");
            }
        }

        public string TargetProdAddShiftUoM
        {
            get
            {
                return GetHtmlControl<HtmlControl>("targetProductionAddShiftUoM").BaseElement.InnerText;
            }
        }

        public HtmlInputText ShiftFromTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("shiftFromTime");
            }
        }

        public HtmlInputText ShiftToTime
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("shiftToTime");
            }
        }

        public HtmlButton AddBreak
        {
            get
            {
                Thread.Sleep(2000);
                return GetHtmlControl<HtmlButton>("addBreak");
            }
        }

        public HtmlButton SaveShiftButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSaveShift");
            }
        }

        public HtmlButton CancelShiftButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancelShift");
            }
        }

        public HtmlInputCheckBox Sunday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbSunday");
            }
        }

        public HtmlInputCheckBox Monday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbMonday");
            }
        }

        public HtmlInputCheckBox Tuesday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbTueday");
            }
        }

        public HtmlInputCheckBox Wednesday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbWedday");
            }
        }

        public HtmlInputCheckBox Thursday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbThuday");
            }
        }

        public HtmlInputCheckBox Friday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbFriday");
            }
        }

        public HtmlInputCheckBox Saturday
        {
            get
            {
                return GetHtmlControl<HtmlInputCheckBox>("cbSatday");
            }
        }

        public HtmlControl ShiftAddMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("shiftAddMessage");
            }

        }

        public HtmlControl ShiftOverlapMessage
        {
            get
            {
                Thread.Sleep(2000);
                return GetHtmlControl<HtmlControl>("shiftOverlapMessage");
            }

        }

        public HtmlSelect LabourType
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLaborType");
            }
        }

        public HtmlSelect LabourLocation
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("ddlLaborLocation");
            }
        }

        public HtmlInputText ManHours
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("manHours");
            }
        }

        public HtmlInputText AvgPricePerHr
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("avgPricePerHr");
            }
        }

        public HtmlButton AddLabourCancelButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("addLaourCancelButton");
            }
        }

        public HtmlButton AddLabourSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("addLaourSaveButton");
            }
        }

        public string IsBreakOverlapping 
        { 
            get
            {
                Thread.Sleep(2000);
                return GetHtmlControl<HtmlControl>("errorMsgBreaks").BaseElement.InnerText;               
            }
        }

        public ReadOnlyCollection<HtmlControl> BreakFromTime 
        { 
            get
            {
                //Thread.Sleep(2000);
                return WaitforAction <ReadOnlyCollection<HtmlControl>>(() =>
                {
                    return Telerik.Find.AllByXPath<HtmlControl>(".//*[@id='timepickerBreakfromtime']");
                },Config.PageClassSettings.Default.MaxTimeoutValue);                
            }
        }

        public ReadOnlyCollection<HtmlControl> BreakToTime
        {
            get
            {
                //Thread.Sleep(2000);
                return WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
                {
                   return Telerik.Find.AllByXPath<HtmlControl>(".//*[@id='timepickerBreaktotime']");
                }, Config.PageClassSettings.Default.MaxTimeoutValue);                
            }
        }

        public bool IsShiftTabPresent
        {
            get
            {
                return IsPresent<HtmlControl>("tabShiftLabor");
            }
        }

        public bool IsDaySelected(DayOfWeek day)
        {
            if (GetDay(day).BaseElement.Parent.GetAttributeValueOrEmpty("class").Contains("btn-primary active"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> IsDaySelectedList(List<DayOfWeek> dys)
        {
            List<string> selectedDays = new List<string>();
            foreach (DayOfWeek day in dys)
            {
                if (GetDay(day).BaseElement.Parent.GetAttributeValueOrEmpty("class").Contains("btn-primary active"))
                {
                    selectedDays.Add(GetDay(day).BaseElement.Parent.InnerText);
                }
            }
            return selectedDays;
        }
                       
        public void SelectDay(DayOfWeek day)
        {
            if (!IsDaySelected(day))
            {
                GetDay(day).Click();
            }
        }

        public void UnSelectDay(DayOfWeek day)
        {
            if (IsDaySelected(day))
            {
                HtmlInputControl currentDay = GetDay(day);
                currentDay.Focus();
                currentDay.Click();            
                currentDay.Refresh();
            }
        }

        public void UnSelectDay(List<DayOfWeek> days)
        {
            foreach(DayOfWeek day in days)
            {
                UnSelectDay(day);
            }            
        }

        public HtmlControl ShiftDeleteButton(string day,string shiftName)
        {
            string queryString
                = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../*[@id='deleteShift']", day, shiftName);
            Telerik.ActiveBrowser.RefreshDomTree();            
            return WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public HtmlControl ShiftEditButton(string day, string shiftName)
        {
            string queryString
                = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../*[@id='editShift']", day, shiftName);            
            Telerik.ActiveBrowser.RefreshDomTree();           
            return WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);
            },Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public HtmlControl NoBreakMessage(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/label", day, shiftName);           
            Telerik.ActiveBrowser.RefreshDomTree();
            return WaitforAction(() => 
                    { 
                        Telerik.ActiveBrowser.RefreshDomTree();
                        return Telerik.Find.ByXPath<HtmlControl>(queryString);
            
                    }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public ReadOnlyCollection<HtmlControl> DeleteBreakButtonsFromShiftPage(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/span/*[@id='deleteBreak']", day, shiftName);
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(2000);
            return (ReadOnlyCollection<HtmlControl>)WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.AllByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public ReadOnlyCollection<HtmlControl> DeleteBreakButtonsFromPopup()
        {
            string queryString = ".//*[@id='deleteBreakEdit']";
            Telerik.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(2000);
            return (ReadOnlyCollection<HtmlControl>)WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.AllByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public List<string> ShiftTexts(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/span[@class='breaks-block']", day, shiftName);
            List<string> shiftStrings = new List<string>();
            Telerik.ActiveBrowser.RefreshDomTree();
            ReadOnlyCollection<HtmlControl> shiftControls = (ReadOnlyCollection<HtmlControl>)WaitforAction(() =>
                                                                    {
                                                                        Telerik.ActiveBrowser.RefreshDomTree();
                                                                        return Telerik.Find.AllByXPath<HtmlControl>(queryString);

                                                                    }, Config.PageClassSettings.Default.MaxTimeoutValue);

            foreach(HtmlControl control in shiftControls)
            {
                shiftStrings.Add(control.BaseElement.InnerText);
            }
            return shiftStrings;
        }

        public string ShiftTargetProductionUoM(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../following-sibling::div/div[1]/span", day, shiftName);
            List<string> shiftStrings = new List<string>();
            Telerik.ActiveBrowser.RefreshDomTree();
            HtmlControl shiftTargetControlUoM = WaitforAction<HtmlControl>(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.ByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);

            return shiftTargetControlUoM.BaseElement.InnerText;
        }
        /// <summary>
        /// Overloading method to get shift name on a particular day
        /// </summary>
        /// <param name="day"></param>
        /// <param name="shiftName"></param>
        /// <returns></returns>
        public List<string> GetShiftDetails(string day, string shiftName)
        {            
            string queryString
              = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]", day, shiftName);            
            List<string> shiftStrings = new List<string>();
            Telerik.ActiveBrowser.RefreshDomTree();
            ReadOnlyCollection<HtmlControl> shiftControls = WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
                                                                {
                                                                    Telerik.ActiveBrowser.RefreshDomTree();
                                                                    return Telerik.Find.AllByXPath<HtmlControl>(queryString);
                                                                }, Config.PageClassSettings.Default.MaxTimeoutValue);            
            foreach (HtmlControl control in shiftControls)
            {
                shiftStrings.Add(control.BaseElement.InnerText);
            }
            return shiftStrings;
        }

        /// <summary>
        /// Overloading method to get shiftname irrespective of days.
        /// </summary>
        /// <param name="shiftName"></param>
        /// <returns></returns>
        public List<string> GetShiftDetails(string shiftName)
        {
            string queryString
              = string.Format("//*/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{0}')]", shiftName);

            List<string> shiftNamesList = new List<string>();
            Telerik.ActiveBrowser.RefreshDomTree();
            ReadOnlyCollection<HtmlControl> shiftControls = WaitforAction<ReadOnlyCollection<HtmlControl>>(() =>
                                                                {
                                                                    Telerik.ActiveBrowser.RefreshDomTree();
                                                                    return Telerik.Find.AllByXPath<HtmlControl>(queryString);
                                                                }, Config.PageClassSettings.Default.MaxTimeoutValue);                
            foreach (HtmlControl control in shiftControls)
            {
                shiftNamesList.Add(control.BaseElement.InnerText);
            }
            return shiftNamesList;
        }

        public ReadOnlyCollection<HtmlControl> LabourRows(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/div[3]/div[2]/table/tbody/tr", day, shiftName);            
            Telerik.ActiveBrowser.RefreshDomTree();
            return (ReadOnlyCollection<HtmlControl>)WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.AllByXPath<HtmlControl>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public bool IsLaborRowAbsent(string day, string shiftName)
        {
            string queryString
               = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/div[3]/div[2]/table/tbody/tr", day, shiftName);
            Telerik.ActiveBrowser.RefreshDomTree();           
            ReadOnlyCollection<HtmlControl> labourTable = WaitforCountAction<ReadOnlyCollection<HtmlControl>>(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                var test = Telerik.Find.AllByXPath<HtmlControl>(queryString);
                return test;

            },0, Config.PageClassSettings.Default.MaxTimeoutValue);

            if (labourTable.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public HtmlControl LabourRowEditButton(HtmlControl row)
        {            
            HtmlControl editButton = new HtmlControl(row.ChildNodes.FirstOrDefault().ChildNodes[2]);
            return editButton;
        }

        public HtmlControl LabourRowDeleteButton(HtmlControl row)
        {           
            HtmlControl deleteButton = new HtmlControl(row.ChildNodes.FirstOrDefault().ChildNodes[3]);
            return deleteButton;
        }

        public HtmlControl LabourRowUpdateButton(HtmlControl row)
        {           
            HtmlControl updateButton = new HtmlControl(row.ChildNodes.FirstOrDefault().ChildNodes[4]);
            return updateButton;
        }

        public List<string> RowValues(HtmlControl row)
        {
            ReadOnlyCollection<Element> cells = row.ChildNodes;
            List<string> values = new List<string>();
            foreach(Element cell in cells)
            {
                if(!cell.InnerText.Equals(string.Empty))
                {
                    values.Add(cell.ChildNodes.FirstOrDefault().InnerText);
                }
            }

            return values;
        }

        public HtmlButton AddLabour(string day, string shiftName)
        {
            string queryString
              = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong[contains(text(),'{1}')]/../../../div[2]/div[3]/div[1]/*[@id='btnAddLabor']", day, shiftName);
            Telerik.ActiveBrowser.RefreshDomTree();            
            return (HtmlButton)WaitforAction(() =>
                {
                    Telerik.ActiveBrowser.RefreshDomTree();
                    return Telerik.Find.ByXPath<HtmlButton>(queryString);

                }, Config.PageClassSettings.Default.MaxTimeoutValue);
        }

        public List<string> ShiftsPresent(string day)
        {
            string queryString
              = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/strong", day);
            List<string> shiftTexts = new List<string>();
            Telerik.ActiveBrowser.RefreshDomTree();
           ReadOnlyCollection<HtmlButton> shifts = (ReadOnlyCollection<HtmlButton>)WaitforAction(() =>
            {
                Telerik.ActiveBrowser.RefreshDomTree();
                return Telerik.Find.AllByXPath<HtmlButton>(queryString);

            }, Config.PageClassSettings.Default.MaxTimeoutValue);

            foreach(HtmlControl shift in shifts)
            {
                shiftTexts.Add(shift.BaseElement.InnerText);
            }

            return shiftTexts;
        }

        public void DeleteAllShifts(string day)
        {
            string queryString
              = string.Format("//*[text()='{0}']/../../../../div[2]/div/div/div/div[1]/span/../../div/*[@id='deleteShift']", day);
            Telerik.ActiveBrowser.RefreshDomTree();
            ReadOnlyCollection<HtmlControl> allDeleteButtons;             
            allDeleteButtons = Telerik.Find.AllByXPath<HtmlControl>(queryString);
            while ((null != allDeleteButtons && allDeleteButtons.Count > 0))
            {   
                foreach(HtmlControl deleteButton in allDeleteButtons)
                {
                    //DialogHandler.ClickonOKButton();
                    deleteButton.Click();
                    DialogHandler.OkButton.Click();                    
                }
                allDeleteButtons = Telerik.Find.AllByXPath<HtmlControl>(queryString);             
            }
        }

        private HtmlInputCheckBox GetDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return Sunday;

                case DayOfWeek.Monday:
                    return Monday;

                case DayOfWeek.Tuesday:
                    return Tuesday;

                case DayOfWeek.Wednesday:
                    return Wednesday;

                case DayOfWeek.Thursday:
                    return Thursday;

                case DayOfWeek.Friday:
                    return Friday;

                case DayOfWeek.Saturday:
                    return Saturday;                    

                default:
                    return Sunday;                    
            }
        }
      
        public HtmlSelect LanguagePreferred
        {
            get
            {
                return GetHtmlControl<HtmlSelect>("SelectPreferredLanguage");
            }
        }

        public HtmlButton GeneralTabSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpload");
            }
        }
    }
}
