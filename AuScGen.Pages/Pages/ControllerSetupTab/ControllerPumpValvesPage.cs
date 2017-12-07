using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecolab.Pages
{
    public class ControllerPumpValvesPage : PageBase
    {
        private string guiMap;

        public ControllerPumpValvesPage(List<object> utilsList)
            : base(utilsList, "ControllerGeneralSetupPage.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ControllerGeneralSetupPage.xml");
        }

        public HtmlButton SaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnSave");
            }
        }

        public HtmlButton CancelSaveButton
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }
    }
}
