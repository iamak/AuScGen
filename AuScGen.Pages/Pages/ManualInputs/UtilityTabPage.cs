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

namespace Ecolab.Pages
{
    public class UtilityTabPage : PageBase
    {
        private string guiMap;
        
        public UtilityTabPage(List<object> utilsList)
            : base(utilsList, "ManualInputUtilityTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "ManualInputUtilityTab.xml");
        }

        public HtmlControl UtilityTab
        {
            get
            {
                return GetHtmlControl<HtmlControl>("tabUtility");
            }
        }


        public CommonControls.EcolabDataGrid UtilityTabGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "UtiltyTabGridTable");
            }
        }

        public HtmlInputText LastValue
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtLastValue");
            }
        }

        public HtmlInputText LastUsage
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtUsage");
            }
        }

        public HtmlInputText NewValue
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNewValue");
            }
        }

        public HtmlInputText NewUsage
        {
            get
            {
                return GetHtmlControl<HtmlInputText>("txtNewUsage");
            }
        }

        public HtmlButton SaveManualInput
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnUpdate");
            }
        }

        public HtmlButton CancelManualInput
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnCancel");
            }
        }

        public HtmlControl SuccessMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("divErrorMsg");
            }
        }

        public HtmlControl PopupMessage
        {
            get
            {
                return GetHtmlControl<HtmlControl>("userErrorMsg");
            }
        }

        public HtmlControl UOM
        {
            get
            {
                return GetHtmlControl<HtmlControl>("UOM");
            }
        }

        public HtmlControl LastRecordDeleteButton
        {
            get
            {
                return GetHtmlControl<HtmlControl>("lastRecordDeleteButton");
            }
        }
    }
}
