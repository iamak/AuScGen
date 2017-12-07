// ***********************************************************************
// <copyright file="Win32.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Win32 class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;

namespace WebDriverWrapper
{
    /// <summary>
    /// Win32
    /// </summary>
    public class Win32
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
        private static extern void Mouse_event(int flags, int dx, int dy, int buttons, int extraInfo);

        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern long SetCursorPos(int x, int y);

        /// <summary>
        /// The mouseevent f_ leftdown
        /// </summary>
        private const int MOUSEEVENTFLEFTDOWN = 0x02;
        /// <summary>
        /// The mouseevent f_ leftup
        /// </summary>
        private const int MOUSEEVENTFLEFTUP = 0x04;
        /// <summary>
        /// The mouseevent f_ rightdown
        /// </summary>
        private const int MOUSEEVENTFRIGHTDOWN = 0x08;
        /// <summary>
        /// The mouseevent f_ rightup
        /// </summary>
        private const int MOUSEEVENTFRIGHTUP = 0x10;
        /// <summary>
        /// The middledown
        /// </summary>
        private const int MIDDLEDOWN = 0x00000020;
        /// <summary>
        /// The middleup
        /// </summary>
        private const int MIDDLEUP = 0x00000040;
        /// <summary>
        /// The move
        /// </summary>
        private const int MOVE = 0x00000001;
        /// <summary>
        /// The absolute
        /// </summary>
        private const int ABSOLUTE = 0x00008000;

        /// <summary>
        /// Mouses the click.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private static void MouseClick(int x, int y)
        {
            //int x = 100;
            //int y = 100;
            SetCursorPos(x, y);
            Mouse_event(MOUSEEVENTFLEFTDOWN, x, y, 0, 0);
            Mouse_event(MOUSEEVENTFLEFTUP, x, y, 0, 0);
        }

        /// <summary>
        /// Mouses the drag.
        /// </summary>
        /// <param name="startX">The start x.</param>
        /// <param name="startY">The start y.</param>
        /// <param name="endX">The end x.</param>
        /// <param name="endY">The end y.</param>
        private static void MouseDrag(int startX, int startY, int endX, int endY)
        {
            SetCursorPos(startX, startX);
            Mouse_event(MOUSEEVENTFLEFTDOWN, startX, startY, 0, 0);
            Mouse_event(ABSOLUTE, endX, endX, 0, 0);
            //SetCursorPos(endX, endX);
            Mouse_event(MOUSEEVENTFLEFTUP, endX, endY, 0, 0);
        }

        /// <summary>
        /// Desks the top mouse click.
        /// </summary>
        /// <param name="control">The control.</param>
        internal static void DeskTopMouseClick(SeleniumWebControls control)
        {
            //Rectangle rect = control.BoundingRectangle;
            //double x = rect.X + rect.Width / 2;
            //double y = rect.Y + rect.Height / 2;

            //control.OwnerBrowser.Manager.Desktop.Mouse.HoverOver(new Point(x, y));
            //control.OwnerBrowser.Manager.Desktop.Mouse.Click(MouseClickType.LeftClick, new Point(x, y));

            MouseClick(control.ClickablePoint.X, control.ClickablePoint.Y);

            //MouseClick((int)x, (int)y);
        }

        /// <summary>
        /// Desks the top mouse click.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        internal static void DeskTopMouseClick(SeleniumWebControls control, int offsetX, int offsetY)
        {
            MouseClick(control.ClickablePoint.X + offsetX, control.ClickablePoint.Y + offsetY);
        }

        /// <summary>
        /// Desks the top mouse drag.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        internal static void DeskTopMouseDrag(SeleniumWebControls control, int offsetX, int offsetY)
        {
            MouseDrag(control.ClickablePoint.X, control.ClickablePoint.Y, control.ClickablePoint.X + offsetX, control.ClickablePoint.Y + offsetY);
        }
    }
}
