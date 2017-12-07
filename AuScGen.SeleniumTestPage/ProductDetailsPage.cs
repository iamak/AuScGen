using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    public class ProductDetailsPage : PageBase
    {
         private string guiMap;

         public ProductDetailsPage(List<object> utilsList)
             : base(utilsList, "ProductDetailsPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ProductDetailsPage.xml");
        }
         public WebSpanArea ProductName
         {
             get
             {
                 return GetHtmlControl<WebSpanArea>("WebProductName");
             }
         }

    }
}
