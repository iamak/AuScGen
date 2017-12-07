// ***********************************************************************
// <copyright file="SeleniumWebControls.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebControls class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium;

namespace WebDriverWrapper
{
    /// <summary>
    /// SeleniumWebControls
    /// </summary>
    public class SeleniumWebControls : IControl
    {
        #region Private Members

        /// <summary>
        /// a control type
        /// </summary>
        private ControlType controlType;

        #endregion Private Members

        #region Internal Members

        /// <summary>
        /// Gets or sets a web element.
        /// </summary>
        /// <value>
        /// a web element.
        /// </value>
        public IWebElement WebElement { get; set; }

        /// <summary>
        /// Gets or sets a control access.
        /// </summary>
        /// <value>
        /// a control access.
        /// </value>
        public ControlAccess ControlAccess { get; set; }

        #endregion Internal Members

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebControls"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        internal SeleniumWebControls(IWebElement webElement, ControlType controlType)
        {
            this.WebElement = webElement;
            this.controlType = controlType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebControls"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebControls(IWebElement webElement, ControlType controlType, ControlAccess access)
            : this(webElement, controlType)
        {
            this.ControlAccess = access;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="IControl" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
                return WebElement.Enabled;
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
            get
            {
                int X = (BoundingRectangle.X + BoundingRectangle.Width) / 2;
                int Y = (BoundingRectangle.Y + 60 + BoundingRectangle.Height) / 2;

                //System.Windows.Point
                return new Point(X, Y);
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
            get
            {
                return new Rectangle(ScrollToElement(), WebElement.Size);
            }
        }

        /// <summary>
        /// Gets the get control image.
        /// </summary>
        /// <value>
        /// The get control image.
        /// </value>
        public Image GetControlImage
        {
            get
            {
                byte[] imageArray = ((ITakesScreenshot)ControlAccess.Browser.BrowserHandle).GetScreenshot().AsByteArray;
                var ms = new MemoryStream(imageArray);
                return Image.FromStream(ms);
                //Rectangle controlBox = BoundingRectangle;
                //Bitmap bmpScreenCapture = new Bitmap(controlBox.Width, controlBox.Height);
                //using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                //{
                //    g.CopyFromScreen(controlBox.X + controlBox.Width,
                //                     controlBox.Y + controlBox.Height,
                //                     controlBox.X,
                //                     controlBox.Y,
                //                     controlBox.Size
                //                     );
                //}

                // Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                //                             Screen.PrimaryScreen.Bounds.Height);

                // using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                // {
                //     g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                //                         Screen.PrimaryScreen.Bounds.Y,
                //                         0, 0,
                //                         bmpScreenCapture.Size,
                //                         CopyPixelOperation.SourceCopy);
                // }

                //return bmpScreenCapture;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IControl" /> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool Visible
        {
            get
            {
                return WebElement.Displayed;
            }
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName
        {
            get
            {
                return WebElement.TagName;
            }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public IControl Parent
        {
            get
            {
                return Utility.GetControlFromWebElement(WebElement.FindElement(By.XPath("./..")), ControlType.Custom, this.ControlAccess);
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(WebElement.Text))
                {
                    WebDriverWait wait = new WebDriverWait(ControlAccess.Browser.BrowserHandle, TimeSpan.FromMinutes(2));
                    wait.Until(x => x.FindElement(Utility.GetByFromLocator(ControlAccess.LocatorType, ControlAccess.Locator)).Text.Length > 1);
                    return WebElement.Text;
                }
                else
                {
                    return WebElement.Text;
                }
            }
        }

        /// <summary>
        /// Outers the HTML.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <returns></returns>
        public string OuterHtml(Browser browser)
        {
            return (string)ExecuteJavaScript(browser, @"return arguments[0].outerHTML;");
        }

        /// <summary>
        /// Inners the HTML.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <returns></returns>
        public string InnerHtml(Browser browser)
        {
            return (string)ExecuteJavaScript(browser, @"return arguments[0].innerHTML;");
        }

        /// <summary>
        /// Gets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked
        {
            get
            {
                return WebElement.Selected;
            }
        }

        /// <summary>
        /// Scrolls to element.
        /// </summary>
        /// <returns></returns>
        public Point ScrollToElement()
        {
            //return ((ILocatable)aWebElement).Coordinates.LocationOnScreen;
            //ExecuteJavaScript(aControlAccess.Browser, "arguments[0].scrollIntoView(true);");
            return ((RemoteWebElement)WebElement).LocationOnScreenOnceScrolledIntoView;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="locatorType">Type of a locator.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        /// <returns></returns>
        public IList<IControl> GetChildren(string locator, LocatorType locatorType, ControlType controlType, ControlAccess access)
        {
            return Utility.GetChildren(locator, locatorType, controlType, WebElement, access);
        }

        /// <summary>
        /// Waits for visible.
        /// </summary>
        private void WaitForVisible()
        {
            WebDriverWait wait = new WebDriverWait(ControlAccess.Browser.BrowserHandle, TimeSpan.FromMinutes(2));
            wait.Until(ExpectedConditions.ElementIsVisible(Utility.GetByFromLocator(ControlAccess.LocatorType, ControlAccess.Locator)));
        }

        /// <summary>
        /// Clicks this instance.
        /// </summary>
        public void Click()
        {
            WaitForVisible();
            WebElement.Click();
        }

        /// <summary>
        /// Submits this instance.
        /// </summary>
        public void Submit()
        {
            WaitForVisible();
            WebElement.Submit();
        }

        /// <summary>
        /// Desktops the mouse click.
        /// </summary>
        public void DesktopMouseClick()
        {
            ScrollToElement();
            Win32.DeskTopMouseClick(this);
        }

        /// <summary>
        /// Desktops the mouse click.
        /// </summary>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        public void DesktopMouseClick(int offsetX, int offsetY)
        {
            ScrollToElement();
            Win32.DeskTopMouseClick(this, offsetX, offsetY);
        }

        /// <summary>
        /// Desktops the mouse drag.
        /// </summary>
        /// <param name="offsetX">The offset x.</param>
        /// <param name="offsetY">The offset y.</param>
        public void DesktopMouseDrag(int offsetX, int offsetY)
        {
            //ScrollToElement();
            Win32.DeskTopMouseDrag(this, offsetX, offsetY);
        }

        /// <summary>
        /// Sends the keys.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SendKeys(string text)
        {
            WaitForVisible();
            WebElement.SendKeys(text);
        }

        #endregion Public Methods

        /// <summary>
        /// Highlights the specified a browser.
        /// </summary>
        /// <param name="browser">a browser.</param>
        public void Highlight(Browser browser)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)browser.BrowserHandle;

            //object aStyle = aScriptExecutor.ExecuteScript("arguments[0].getAttribute('style');", WebElement);
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", WebElement,
                                          "border: 4px solid red;");

            //for (int i = 0; i < 5; i++)
            //{
            //    //aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", aWebElement,"color: red; border: 4px solid red;");

            //    object aStyle = aScriptExecutor.ExecuteScript("arguments[0].getAttribute('style');", aWebElement);
            //    aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", aWebElement,
            //                                  "border: 4px solid red;");
            //    //Thread.Sleep(50);

            //    //aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);",
            //    //    aWebElement, aStyle);
            //}
        }

        /// <summary>
        /// Executes the java script.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public object ExecuteJavaScript(Browser browser, string javaScript)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)browser.BrowserHandle;

            return scriptExecutor.ExecuteScript(javaScript, WebElement);
        }

        /// <summary>
        /// Injects the js in browser.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public object InjectJSInBrowser(Browser browser, string javaScript)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)browser.BrowserHandle;

            return scriptExecutor.ExecuteScript(javaScript);
        }

        /// <summary>
        /// Gets the attribute from node.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public string GetAttributeFromNode(string attribute)
        {
            return WebElement.GetAttribute(attribute);
        }

        /// <summary>
        /// Determines whether this instance has children.
        /// </summary>
        /// <returns></returns>
        public bool HasChildren()
        {
            return GetChildren("//*", LocatorType.Xpath, ControlType.Custom, ControlAccess).Count() > 0;
        }

        /// <summary>
        /// Determines whether [has children with xpath] [the specified xpath].
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public bool HasChildrenWithXpath(string xpath)
        {
            return GetChildren(xpath, LocatorType.Xpath, ControlType.Custom, ControlAccess).Count() > 0;
        }

        /// <summary>
        /// Waits for children.
        /// </summary>
        /// <param name="maxTimeout">The maximum timeout.</param>
        /// <returns></returns>
        public IList<IControl> WaitForChildren(int maxTimeout)
        {
            DateTime start;
            double timeElapsed = 0;
            //SelectElement element = new SelectElement(WebElement);

            start = DateTime.Now;

            while (!HasChildren() && timeElapsed < maxTimeout)
            {
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            return GetChildren(".//*", LocatorType.Xpath, ControlType.Custom, ControlAccess);
        }

        /// <summary>
        /// Waits for children.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        /// <param name="maxTimeout">The maximum timeout.</param>
        /// <returns></returns>
        public IList<IControl> WaitForChildren(string xpath, int maxTimeout)
        {
            DateTime start;
            double timeElapsed = 0;
           //SelectElement element = new SelectElement(WebElement);

            start = DateTime.Now;

            while (!HasChildren() && timeElapsed < maxTimeout)
            {
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            return GetChildren(xpath, LocatorType.Xpath, ControlType.Custom, ControlAccess);
        }
    }
}
