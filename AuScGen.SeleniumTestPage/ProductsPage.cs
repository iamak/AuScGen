using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    public class ProductsPage : PageBase
    {
        private string guiMap;

        public ProductsPage(List<object> utilsList)
            : base(utilsList, "ProductsPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ProductsPage.xml");
        }
        public WebButton NewProducts
        {
            get
            {
                return GetHtmlControl<WebButton>("BtnNewProducts");
            }
        }
    }
}
