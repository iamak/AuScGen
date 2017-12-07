// ***********************************************************************
// <copyright file="IControl.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IControl Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebDriverWrapper
{
	/// <summary>
	/// IControl
	/// </summary>
    public interface IControl
    {
		/// <summary>
		/// Gets a value indicating whether this <see cref="IControl" /> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
        bool Enabled { get; }

		/// <summary>
		/// Gets the clickable point.
		/// </summary>
		/// <value>
		/// The clickable point.
		/// </value>
        Point ClickablePoint { get; }

		/// <summary>
		/// Gets the bounding rectangle.
		/// </summary>
		/// <value>
		/// The bounding rectangle.
		/// </value>
        Rectangle BoundingRectangle { get; }

		/// <summary>
		/// Gets the get control image.
		/// </summary>
		/// <value>
		/// The get control image.
		/// </value>
        Image GetControlImage { get; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="IControl" /> is visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if visible; otherwise, <c>false</c>.
		/// </value>
        bool Visible { get; }

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
        string Text { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is checked.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
		/// </value>
        bool IsChecked { get; }

		/// <summary>
		/// Gets the name of the tag.
		/// </summary>
		/// <value>
		/// The name of the tag.
		/// </value>
        string TagName { get; }

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <value>
		/// The parent.
		/// </value>
        IControl Parent { get; }

		/// <summary>
		/// Scrolls to element.
		/// </summary>
		/// <returns></returns>
        Point ScrollToElement();

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <param name="locator">The locator.</param>
		/// <param name="locatorType">Type of a locator.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
        IList<IControl> GetChildren(string locator, LocatorType locatorType, ControlType controlType, ControlAccess access);

		/// <summary>
		/// Highlights the specified a browser.
		/// </summary>
		/// <param name="browser">a browser.</param>
        void Highlight(Browser browser);

		/// <summary>
		/// Clicks this instance.
		/// </summary>
        void Click();

		/// <summary>
		/// Submits this instance.
		/// </summary>
        void Submit();

		/// <summary>
		/// Sends the keys.
		/// </summary>
		/// <param name="text">The text.</param>
        void SendKeys(string text);

		/// <summary>
		/// Desktops the mouse click.
		/// </summary>
        void DesktopMouseClick();

		/// <summary>
		/// Desktops the mouse click.
		/// </summary>
		/// <param name="offsetX">The offset x.</param>
		/// <param name="offsetY">The offset y.</param>
        void DesktopMouseClick(int offsetX, int offsetY);

		/// <summary>
		/// Desktops the mouse drag.
		/// </summary>
		/// <param name="offsetX">The offset x.</param>
		/// <param name="offsetY">The offset y.</param>
        void DesktopMouseDrag(int offsetX, int offsetY);

		/// <summary>
		/// Gets the attribute from node.
		/// </summary>
		/// <param name="attribute">The attribute.</param>
		/// <returns></returns>
        string GetAttributeFromNode(string attribute);

		/// <summary>
		/// Executes the java script.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <param name="javaScript">The java script.</param>
		/// <returns></returns>
        object ExecuteJavaScript(Browser browser, string javaScript);

		/// <summary>
		/// Injects the js in browser.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <param name="javaScript">The java script.</param>
		/// <returns></returns>
        object InjectJSInBrowser(Browser browser, string javaScript);

		/// <summary>
		/// Determines whether this instance has children.
		/// </summary>
		/// <returns></returns>
        bool HasChildren();

		/// <summary>
		/// Determines whether [has children with xpath] [the specified xpath].
		/// </summary>
		/// <param name="xpath">The xpath.</param>
		/// <returns></returns>
        bool HasChildrenWithXpath(string xpath);

		/// <summary>
		/// Waits for children.
		/// </summary>
		/// <param name="maxTimeout">The maximum timeout.</param>
		/// <returns></returns>
        IList<IControl> WaitForChildren(int maxTimeout);

		/// <summary>
		/// Waits for children.
		/// </summary>
		/// <param name="xpath">The xpath.</param>
		/// <param name="maxTimeout">The maximum timeout.</param>
		/// <returns></returns>
        IList<IControl> WaitForChildren(string xpath, int maxTimeout);

		/// <summary>
		/// Inners the HTML.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <returns></returns>
        string InnerHtml(Browser browser);

		/// <summary>
		/// Outers the HTML.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <returns></returns>
        string OuterHtml(Browser browser);
    }
}