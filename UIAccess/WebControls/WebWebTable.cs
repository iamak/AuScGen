using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;
using WebDriverWrapper.IControlHierarchy;

namespace UIAccess.WebControls
{
    public class WebWebTable : WebControl
    {
        public WebWebTable(Browser aBrowser, Locator aLocator)
            : base(aBrowser, aLocator.LocatorType, aLocator.ControlLocator, ControlType.WebTable)
        { }

        private IWebTable WebTable
        {
            get
            {
                return (IWebTable)Control;
            }
        }
    }
}
