using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDriverWrapper
{
    public interface IControlAccess
    {
        IControl GetControl();

        List<IControl> GetChildren(string Locator, LocatorType aLocatorType, ControlType aControlType);
    }
}
