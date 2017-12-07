// ***********************************************************************
// <copyright file="HtmlInputTextExtension.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>HtmlInputTextExtension class</summary>
// ***********************************************************************
using System.Drawing;
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.HtmlControls;

using ArtOfTest.WebAii.Core;

namespace AuScGen
{
    /// <summary>
    ///		Class Html Input Text Extension
    /// </summary>
    public static class HtmlInputTextExtension
    {
		/// <summary>
		/// Mouse_events the specified dw flags.
		/// </summary>
		/// <param name="flags">The dw flags.</param>
		/// <param name="dx">The dx.</param>
		/// <param name="dy">The dy.</param>
		/// <param name="buttons">The c buttons.</param>
		/// <param name="extraInfo">The dw extra information.</param>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void Mouse_event(int flags, int dx, int dy, int buttons, int extraInfo);

		/// <summary>
		/// Sets the cursor position.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

		/// <summary>
		/// The mouseevent f_ leftdown
		/// </summary>
        public const int MOUSEEVENTFLEFTDOWN = 0x02;
		/// <summary>
		/// The mouseevent f_ leftup
		/// </summary>
        public const int MOUSEEVENTFLEFTUP = 0x04;
		/// <summary>
		/// The mouseevent f_ rightdown
		/// </summary>
        public const int MOUSEEVENTFRIGHTDOWN = 0x08;
		/// <summary>
		/// The mouseevent f_ rightup
		/// </summary>
        public const int MOUSEEVENTFRIGHTUP = 0x10;

		/// <summary>
		/// Mouses the click.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
        private static void MouseClick(int x , int y)
        {
            //int x = 100;
            //int y = 100;
            SetCursorPos(x, y);
            Mouse_event(MOUSEEVENTFLEFTDOWN, x, y, 0, 0);
            Mouse_event(MOUSEEVENTFLEFTUP, x, y, 0, 0);
        }

		/// <summary>
		/// Types the text.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="text">The text.</param>
        public static void TypeText(HtmlControl control, string text)
        {
            if(control.TagName == "input")
            {
                HtmlInputText ctrl = new HtmlInputText(control.BaseElement);
                ctrl.Text = string.Empty;
                ctrl.Focus();
                ctrl.ExtendedMouseClick();
                ctrl.Text = text;
                ctrl.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
            else
            {
                control.Focus();
                control.ExtendedMouseClick();
                control.OwnerBrowser.Manager.Desktop.KeyBoard.TypeText(text);
                control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }    
        }
		/// <summary>
		/// Singles the tab click button.
		/// </summary>
		/// <param name="control">The control.</param>
        public static void SingleTabClickButton(this ArtOfTest.WebAii.Controls.Control control)
        {
            
            control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Enter);
        }
		/// <summary>
		/// Multis the tab click button.
		/// </summary>
		/// <param name="control">The control.</param>
        public static void MultipleTabClickButton(ArtOfTest.WebAii.Controls.Control control)
        {
            control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
            control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Enter);
        }
		/// <summary>
		/// Sets the text.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="text">The text.</param>
        public static void SetText(this HtmlControl control, string text)
        {
            if (control.TagName == "input")
            {
                HtmlInputText ctrl = new HtmlInputText(control.BaseElement);
                ctrl.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
                ctrl.Text = string.Empty;
                ctrl.Focus();
                ctrl.ExtendedMouseClick();
                ctrl.Text = text;
                ctrl.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Space);
                ctrl.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
            else
            {
                control.Focus();
                control.ExtendedMouseClick();
                control.OwnerBrowser.Manager.Desktop.KeyBoard.TypeText(text);
                control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Tab);
            }
        }

		/// <summary>
		/// Desks the top mouse click.
		/// </summary>
		/// <param name="control">The control.</param>
        public static void DesktopMouseClick(this HtmlControl control)
        {
            
            Rectangle rect = control.GetRectangle();
            int x = (rect.X + rect.Width)/2;
            int y = (rect.Y + rect.Height)/2;

            //control.OwnerBrowser.Manager.Desktop.Mouse.HoverOver(new Point(x, y));
            //control.OwnerBrowser.Manager.Desktop.Mouse.Click(MouseClickType.LeftClick, new Point(x, y));
            MouseClick(x, y);
        }

		/// <summary>
		/// Extendeds the mouse click.
		/// </summary>
		/// <param name="control">The control.</param>
        public static void ExtendedMouseClick(this HtmlControl control)
        {
            if(control.OwnerBrowser.BrowserType != BrowserType.Chrome)
            {
                control.MouseClick();
            }
            else
            {
                control.Focus();
                control.Click();
            }
        }
    }
}
