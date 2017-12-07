// ***********************************************************************
// <copyright file="WebControl.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebControl class</summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using WebDriverWrapper;

namespace UIAccess.WebControls
{
	/// <summary>
	///         Class WebControl
	/// </summary>
	public class WebControl
	{
		/// <summary>
		/// My control access
		/// </summary>
		private ControlAccess myControlAccess;

		/// <summary>
		/// Gets or sets the control.
		/// </summary>
		/// <value>
		/// The control.
		/// </value>
		public IControl ControlObject { get; set; }
		/// <summary>
		/// Gets or sets the type of my control.
		/// </summary>
		/// <value>
		/// The type of my control.
		/// </value>
		public ControlType MyControlType { get; set; }

		//private Browser myBrowser;

		//private LocatorType myLocatorType;

		//private string myLocator;

		#region ctor

		/// <summary>
		/// Initializes a new instance of the <see cref="WebControl"/> class.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <param name="locatorType">Type of a locator.</param>
		/// <param name="locator">a locator.</param>
		/// <param name="controlType">Type of a control.</param>
		public WebControl(Browser browser, LocatorType locatorType, string locator, ControlType controlType)
		{
			myControlAccess = new ControlAccess();
			myControlAccess.Browser = browser;
			myControlAccess.LocatorType = locatorType;
			myControlAccess.Locator = locator;
			myControlAccess.ControlType = controlType;
			myControlAccess.IntializeControlAccess();
			//ControlObject = myControlAccess.GetControl();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebControl"/> class.
		/// </summary>
		/// <param name="browser">a browser.</param>
		/// <param name="locator">a locator.</param>
		public WebControl(Browser browser, Locator locator)
		{
			myControlAccess = new ControlAccess();
			myControlAccess.Browser = browser;
			myControlAccess.LocatorType = locator.LocatorType;
			myControlAccess.Locator = locator.ControlLocator;
			myControlAccess.ControlType = ControlType.Custom;
			myControlAccess.IntializeControlAccess();
			//ControlObject = myControlAccess.GetControl();
		}

		#endregion ctor

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether this <see cref="WebControl"/> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		public bool Enabled
		{
			get
			{
				return ControlObject.Enabled;
			}
		}

		/// <summary>
		/// Gets the bounding rectangle.
		/// </summary>
		/// <value>
		/// The bounding rectangle.
		/// </value>
		public Rectangle BoundingRectangle
		{
			get { return ControlObject.BoundingRectangle; }
		}

		/// <summary>
		/// Gets the get screen image.
		/// </summary>
		/// <value>
		/// The get screen image.
		/// </value>
		public Image GetScreenImage
		{
			get
			{
				return ControlObject.GetControlImage;
			}
		}

		/// <summary>
		/// Gets the clickable point.
		/// </summary>
		/// <value>
		/// The clickable point.
		/// </value>
		public Point ClickablePoint
		{
			get { return ControlObject.ClickablePoint; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="WebControl"/> is visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if visible; otherwise, <c>false</c>.
		/// </value>
		public bool Visible
		{
			get { return ControlObject.Visible; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text
		{
			get { return ControlObject.Text; }
		}

		/// <summary>
		/// Gets the inner HTML.
		/// </summary>
		/// <value>
		/// The inner HTML.
		/// </value>
		public string InnerHtml
		{
			get
			{
				return ControlObject.InnerHtml(myControlAccess.Browser);
			}
		}

		/// <summary>
		/// Gets the outer HTML.
		/// </summary>
		/// <value>
		/// The outer HTML.
		/// </value>
		public string OuterHtml
		{
			get
			{
				return ControlObject.OuterHtml(myControlAccess.Browser);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is checked.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
		/// </value>
		public bool IsChecked
		{
			get { return ControlObject.IsChecked; }
		}

		/// <summary>
		/// Gets the name of the tag.
		/// </summary>
		/// <value>
		/// The name of the tag.
		/// </value>
		public string TagName
		{
			get { return ControlObject.TagName; }
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public Actions Action
		{
			get
			{
				return new Actions(myControlAccess);
			}
		}

		/// <summary>
		/// Gets the selenium control.
		/// </summary>
		/// <value>
		/// The selenium control.
		/// </value>
		public IControl SeleniumControl
		{
			get
			{
				return ControlObject;
			}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Scrolls to element.
		/// </summary>
		/// <returns></returns>
		public Point ScrollToElement()
		{
			return ControlObject.ScrollToElement();
		}

		/// <summary>
		/// Determines whether [is control present].
		/// </summary>
		/// <returns></returns>
		public bool IsControlPresent()
		{
			return myControlAccess.IsElementPresent();
		}

		/// <summary>
		/// Gets the control.
		/// </summary>
		public void GetControl()
		{
			ControlObject = myControlAccess.GetControl();
		}

		/// <summary>
		/// Gets the get controls.
		/// </summary>
		/// <value>
		/// The get controls.
		/// </value>
		public ReadOnlyCollection<IControl> GetControls
		{
			get { return myControlAccess.GetControls; }
		}

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <param name="locator">a locator.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <returns></returns>
		public IList<WebControl> GetChildren(Locator locator, ControlType controlType)
		{
			IList<IControl> controlList = new List<IControl>();
			controlList = myControlAccess.GetChildren(locator.ControlLocator, locator.LocatorType, controlType);

			return Utility.GetWebControlsFromIControlList(controlList, myControlAccess.Browser, locator, controlType);
		}

		/// <summary>
		/// Highlights this instance.
		/// </summary>
		public void Highlight()
		{
			ControlObject.Highlight(myControlAccess.Browser);
		}

		/// <summary>
		/// Clicks at.
		/// </summary>
		public void ClickAt()
		{
			myControlAccess.ClickAt();
		}

		/// <summary>
		/// Clicks this instance.
		/// </summary>
		public void Click()
		{
			//ControlObject.ExecuteJavaScript(myControlAccess.Browser, "arguments[0].hidden = false");
			ControlObject.Click();
		}

		/// <summary>
		/// Submits this instance.
		/// </summary>
		public void Submit()
		{
			ControlObject.Submit();
		}

		/// <summary>
		/// Sends the keys.
		/// </summary>
		/// <param name="keys">The keys.</param>
		public void SendKeys(string keys)
		{
			ControlObject.SendKeys(keys);
		}

		/// <summary>
		/// Desktops the mouse click.
		/// </summary>
		public void DesktopMouseClick()
		{
			ControlObject.DesktopMouseClick();
		}

		/// <summary>
		/// Desktops the mouse click.
		/// </summary>
		/// <param name="offsetX">The offset x.</param>
		/// <param name="offsetY">The offset y.</param>
		public void DesktopMouseClick(int offsetX, int offsetY)
		{
			ControlObject.DesktopMouseClick(offsetX, offsetY);
		}

		/// <summary>
		/// Desktops the mouse drag.
		/// </summary>
		/// <param name="offsetX">The offset x.</param>
		/// <param name="offsetY">The offset y.</param>
		public void DesktopMouseDrag(int offsetX, int offsetY)
		{
			ControlObject.DesktopMouseDrag(offsetX, offsetY);
		}

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public string GetAttribute(string attributeName)
		{
			return ControlObject.GetAttributeFromNode(attributeName);
		}

		/// <summary>
		/// Gets the get attributes.
		/// </summary>
		/// <value>
		/// The get attributes.
		/// </value>
		public Dictionary<string, object> GetAttributes
		{
			get
			{
				return (Dictionary<string, object>)ExecuteJavaScript(@"var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;");
			}
		}

		/// <summary>
		/// Executejavascripts the specified java script.
		/// </summary>
		/// <param name="javaScript">The java script.</param>
		/// <returns></returns>
		public object ExecuteJavaScript(string javaScript)
		{
			return ControlObject.ExecuteJavaScript(myControlAccess.Browser, javaScript);
		}

		/// <summary>
		/// Injects the js in browser.
		/// </summary>
		/// <param name="javaScript">The java script.</param>
		/// <returns></returns>
		public object InjectJSInBrowser(string javaScript)
		{
			return ControlObject.InjectJSInBrowser(myControlAccess.Browser, javaScript);
		}

		/// <summary>
		/// Determines whether this instance has children.
		/// </summary>
		/// <returns></returns>
		public bool HasChildren()
		{
			return ControlObject.HasChildren();
		}

		/// <summary>
		/// Determines whether [has children with xpath] [the specified xpath].
		/// </summary>
		/// <param name="path">The xpath.</param>
		/// <returns></returns>
		public bool HasChildrenWithxPath(string path)
		{
			return ControlObject.HasChildrenWithXpath(path);
		}

		/// <summary>
		/// Waits for children.
		/// </summary>
		/// <param name="maxTimeout">The maximum timeout.</param>
		/// <returns></returns>
		public IList<WebControl> WaitForChildren(int maxTimeout)
		{
			IList<IControl> controlList = new List<IControl>();
			controlList = ControlObject.WaitForChildren(maxTimeout);
			return Utility.GetWebControlsFromIControlList(controlList, myControlAccess.Browser, new Locator(".//*", LocatorType.Xpath), ControlType.Custom);
		}

		/// <summary>
		/// Waits for children.
		/// </summary>
		/// <param name="path">The xpath.</param>
		/// <param name="maximumTimeout">The maximum timeout.</param>
		/// <returns></returns>
		public IList<WebControl> WaitForChildren(string path, int maximumTimeout)
		{
			IList<IControl> controlList = new List<IControl>();
			controlList = ControlObject.WaitForChildren(path, maximumTimeout);
			return Utility.GetWebControlsFromIControlList(controlList, myControlAccess.Browser, new Locator(path, LocatorType.Xpath), ControlType.Custom);
		}
		#endregion Public Methods
	}
}
