using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace AuScGen.SeleniumTestPage
{
    public class AddPricePage : PageBase
    {
        private string guiMap;

        public AddPricePage(List<object> utilsList)
            : base(utilsList, "AddPricePage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "AddPricePage.xml");
        }

        //public WebEditBox ProductPrice
        //{ 
        //    get
        //    {
        //        return GetHtmlControl<WebEditBox>("TxtPrice");
        //    }
        //}
        
        //public WebCheckBox ActiveCheckbox
        //{ 
        //    get
        //    {
        //        return GetHtmlControl<WebCheckBox>("CheckActive");
        //    }
        //}

        //public WebButton SavePrice
        //{ 
        //    get
        //    {
        //        return GetHtmlControl<WebButton>("BtnSave");
        //    }
        //}
        
    }
}
