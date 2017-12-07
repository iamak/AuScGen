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

namespace Ecolab.Pages.Pages
{
    public class AlarmPage : PageBase
    {
        private string guiMap;

        public AlarmPage(List<object> utilsList)
            : base(utilsList, "Alarm.xml")
        {
            guiMap = string.Concat(GuiMapPath, "Alarm.xml");
        }

        public CommonControls.EcolabDataGrid AlarmTableGrid
        {
            get
            {
                return new CommonControls.EcolabDataGrid(Telerik, guiMap, "AlarmTableGrid");
            }
        }

        public HtmlAnchor AlarmIcon
        {
            get
            {
                return GetHtmlControl<HtmlAnchor>("lnkAlarm");
            }
        }

        public HtmlDiv AlarmCountContainer
        {
            get
            {
                return GetHtmlControl<HtmlDiv>("divAlarmCountContainer");
            }
        }

        public HtmlSpan AlarmPopupTitle
        {
            get
            {
                return GetHtmlControl<HtmlSpan>("AlarmPopupTitle");
            }
        }

        public HtmlButton Close
        {
            get
            {
                return GetHtmlControl<HtmlButton>("btnClose");
            }
        }

    }
}
