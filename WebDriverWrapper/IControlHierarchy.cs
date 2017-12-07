using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WebDriver_Test
{
    public interface IControl
    {
        bool Enabled { get; }
        
        Point ClickablePoint { get; }

        Rectangle BoundingRectangle { get; }

        bool Visible { get; }       
    }

    public interface IButton : IControl
    {
        void Click();
    }

    public interface IEditBox : IControl
    {
        void SendKeys(string aText);
    }
}
