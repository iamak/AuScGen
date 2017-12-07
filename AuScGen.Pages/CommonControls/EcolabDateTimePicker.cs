using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;


namespace Ecolab.Pages.CommonControls
{
    public class EcolabDateTimePicker : HtmlTable
    {
        private Ecolab.TelerikPlugin.TelerikFramework telerikFramework;

        private string strLogicalName = string.Empty;

        private string myguiMap;

        public EcolabDateTimePicker(HtmlTable gridelement)
            : base(gridelement.BaseElement) { }

        /// <summary>
        /// EcolabDataGrid
        /// </summary>
        /// <param name="telerik"></param>
        /// <param name="guiMapPath"></param>
        public EcolabDateTimePicker(Ecolab.TelerikPlugin.TelerikFramework telerik, string guiMapPath)
        {
            telerikFramework = telerik;
            myguiMap = guiMapPath;
        }

        public EcolabDateTimePicker(Ecolab.TelerikPlugin.TelerikFramework telerik, string guiMapPath, string logicalName)
            :this(telerik,guiMapPath)
        {
            telerikFramework = telerik;
            strLogicalName = logicalName;
            myguiMap = string.Concat(guiMapPath);
        }

        /// MainTable
        /// </summary>
        private HtmlTable BaseTable
        {
            get
            {
                return telerikFramework.WaitForControl<HtmlTable>(myguiMap, strLogicalName, Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        private ReadOnlyCollection<Element> HeaderControls
        {
            get
            {
                return BaseTable.HeadRows.FirstOrDefault().ChildNodes;
            }
        }

        private List<HtmlControl> Dates
        {
            get
            {
                ReadOnlyCollection<HtmlTableRow> dateRows = BaseTable.BodyRows;
                List<HtmlControl> dates = new List<HtmlControl>();
                foreach(HtmlTableRow dateRow in dateRows)
                {
                    foreach(Element date in dateRow.ChildNodes)
                    {
                        if(!date.GetAttribute("class").Value.Equals("day old"))
                        {
                            dates.Add(new HtmlControl(date));
                        }
                    }
                }

                return dates;
            }
        }

        public HtmlControl PrevMonthButton
        {
            get
            {
                return new HtmlControl(HeaderControls.FirstOrDefault());
            }
        }

        public HtmlControl NextMonthButton
        {
            get
            {
                return new HtmlControl(HeaderControls.LastOrDefault());
            }
        }

        public string MonthAndYear
        {
            get
            {
                var test = HeaderControls[1].InnerText;
                return HeaderControls[1].InnerText;
            }
        }

        public string TodayDate
        {
            get
            {
                return Dates.Where(date => date.BaseElement.GetAttribute("class").Value.Contains("today")).FirstOrDefault().BaseElement.InnerText;
            }
        }

        public string SelectedDate
        {
            get
            {
                return Dates.Where(date => date.BaseElement.GetAttribute("class").Value.Contains("active")).FirstOrDefault().BaseElement.InnerText;
            }
        }

        public void SelectDay(string monthYearText,string date)
        {
            while(!MonthAndYear.Equals(monthYearText) && !MonthAndYear.ToLower().Contains("december"))
            {
                Thread.Sleep(2000);
                NextMonthButton.DeskTopMouseClick();
            }

            while(!MonthAndYear.Equals(monthYearText) && !MonthAndYear.ToLower().Contains("january"))
            {
                Thread.Sleep(2000);
                PrevMonthButton.DeskTopMouseClick();
            }

            HtmlControl dateToBeSelected = Dates.Where(selectdate => selectdate.BaseElement.InnerText.Equals(date)).FirstOrDefault();

            dateToBeSelected.DeskTopMouseClick();
        }

    }
}
