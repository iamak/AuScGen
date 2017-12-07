using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    public class NewProductsPage : PageBase
    {
        private string guiMap;

        public NewProductsPage(List<object> utilsList)
            : base(utilsList, "NewProductsPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "NewProductsPage.xml");
        }

        public WebEditBox Productname
        {
            get
            {
                return GetHtmlControl<WebEditBox>("TxtProductName");
            }
        }

        public WebEditBox ProductCode
        {
            get
            {
                return GetHtmlControl<WebEditBox>("TxtProductCode");
            }
        }

        public WebEditBox ProductDescription
        {
            get
            {
                return GetHtmlControl<WebEditBox>("TxtProductDescription");
            }
        }

        public WebComboBox ProductFamily
        {
            get
            {
                return GetHtmlControl<WebComboBox>("ComboProductFamily");
            }
        }

        public WebCheckBox Active
        {
            get
            {
                return GetHtmlControl<WebCheckBox>("CheckActive");
            }
        }

        public WebButton SaveAndAddPrice
        {
            get
            {
                return GetHtmlControl<WebButton>("BtnSaveAndAddPrice");
            }
        }
    }
}
