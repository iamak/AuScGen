using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WebDriverWrapper
{
    public interface IControl
    {
        bool Enabled { get; }
        
        Point ClickablePoint { get; }

        Rectangle BoundingRectangle { get; }

        bool Visible { get; }

        string Text { get; }

        bool IsChecked { get;}

        string TagName { get; }

        IControl Parent { get; }

        Point ScrollToElement();

        List<IControl> GetChildren(string Locator, LocatorType aLocatorType, ControlType aControlType);

        void Highlight(Browser aBrowser);
        
        void Click();

        void DesktopMouseClick();

        string GetAttributeFromNode(string Attribute);

        object ExecuteJavaScript(Browser aBrowser, string JavaScript);
    }

    public interface IButton : IControl
    {
        
    }

    public interface IEditBox : IControl
    {
        void SendKeys(string aText);
    }
}
