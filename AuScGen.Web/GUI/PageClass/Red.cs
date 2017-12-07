using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAccess.WebControls;

namespace EDMC.DigitalAppPages
{
	public class Red : AUDigitalPageBase
	{
		private string guiMap;
		private List<object> utilityList;
		public Red(IList<object> utilsList)
			:base(utilsList, "Red.xml")
		{
			guiMap = string.Concat(GuiMapPath, "Red.xml");
			utilityList = utilsList;
		}

		public WebLink linkGmail
		{
			get
			{
				return GetHtmlControl<WebLink> ("linkGmail");
			}
		}

		public WebLink linkImages
		{
			get
			{
				return GetHtmlControl<WebLink> ("linkImages");
			}
		}

		public WebEditBox txtGoogleSearch
		{
			get
			{
				return GetHtmlControl<WebEditBox> ("txtGoogleSearch");
			}
		}

		public WebControl btnGoogleSearch
		{
			get
			{
				return GetHtmlControl<WebControl> ("btnGoogleSearch");
			}
		}

protected string GuiMapPath{get{	return Directory.GetCurrentDirectory()}}	}
}
	}
}
