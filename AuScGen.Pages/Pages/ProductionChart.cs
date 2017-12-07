using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecolab.Pages.Pages
{
   public class ProductionChart : PageBase
    {
       private string guiMap;
       public ProductionChart(Ecolab.TelerikPlugin.TelerikFramework TelerikPlugin)
            : base(TelerikPlugin)
        {
            guiMap = string.Concat(GuiMapPath, "ProductionChart.xml");
        }

       public ProductionChart(List<object> utilsList):base(utilsList, "ProductionChart.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ProductionChart.xml");
        }
       /// <summary>
       /// Returns From date control
       /// </summary>
       public HtmlInputText FromDate
       {
           get
           {
               return GetHtmlControl<HtmlInputText>("txtFromDate");
           }
       }
       /// <summary>
       /// Returns Reset button control
       /// </summary>
       public HtmlButton Reset
       {
           get
           {
               return GetHtmlControl<HtmlButton>("btnReset");
           }
       }
       /// <summary>
       /// returns Apply button control 
       /// </summary>
       public HtmlButton Apply
       {
           get
           {
               return GetHtmlControl<HtmlButton>("btnApply");
           }
       }
       /// <summary>
       /// Returns visualization chart control
       /// </summary>
       public HtmlControl VisualizationChart
       {
           get
           {
               return GetHtmlControl<HtmlControl>("visualizationChart");
           }
       }
       /// <summary>
       /// Parsing date method
       /// </summary>
       /// <param name="parsingDate"></param>
       /// <returns></returns>
       public  DateTime GetParseDate(string parsingDate)
       {
           return DateTime.ParseExact(parsingDate, "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
       }
       /// <summary>
       /// My top main menu
       /// </summary>
       private CommonControls.MainMenu myTopMainMenu;
       /// <summary>
       /// Gets the top main menu.
       /// </summary>
       /// <value>
       /// The top main menu.
       /// </value>
       public CommonControls.MainMenu TopMainMenu
       {
           get
           {
               if (null == myTopMainMenu)
               {
                   myTopMainMenu = new CommonControls.MainMenu(Telerik, GuiMapPath);
               }
               return myTopMainMenu;
           }
       }
       /// <summary>
       /// Gets the bread crumb.
       /// </summary>
       /// <value>
       /// The bread crumb.
       /// </value>
       public HtmlControl BreadCrumbControl
       {
           get
           {
               return TopMainMenu.BreadCrumb;
           }
       }
       /// <summary>
       /// Gets the bread crumb.
       /// </summary>
       /// <returns></returns>
       public string GetBreadCrumbList()
       {
           string strbreadCrumb = string.Empty;
           List<string> myList = new List<string>();
           ICollection<Element> ctrl = BreadCrumbControl.ChildNodes;
           foreach (Element e in ctrl)
           {
               myList.Add(e.InnerText.Trim());
           }
           strbreadCrumb = myList[0] + "->" + myList[1] + "->" + myList[2];
           return strbreadCrumb;
       }
    }
}
