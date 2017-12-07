using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Threading;
using Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverWrapper
{
    public class SeleniumWebControls : IControl
    {
        #region Private Members

        private ControlType aControlType;

        #endregion Private Members

        #region Internal Members
               
        internal IWebElement aWebElement;        

        #endregion Internal Members

        #region ctor

        internal SeleniumWebControls(IWebElement aWebElement, ControlType aControlType)
        {
            this.aWebElement = aWebElement;
            this.aControlType = aControlType;            
        }

        #endregion

        #region Public Properties

        public bool Enabled
        {
            get
            {
                return aWebElement.Enabled;                
                
            }
        }

        public Point ClickablePoint
        {
            get
            {
                int X = BoundingRectangle.X + BoundingRectangle.Width / 2;
                int Y = BoundingRectangle.Y + 60 + BoundingRectangle.Height / 2;
                                
                //System.Windows.Point
                return new Point(X, Y);
            }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(ScrollToElement(), aWebElement.Size);
            }
        }

        public bool Visible
        {
            get
            {
                return aWebElement.Displayed;
            }
        }

        public string TagName
        {
            get
            {
                return aWebElement.TagName;
            }
        }

        public IControl Parent
        {
            get
            {
                return Utility.GetControlFromWebElement(aWebElement.FindElement(By.XPath("./parent::*")), ControlType.Custom);
            }
        }
                        
        public string Text 
        { 
            get
            {
                return aWebElement.Text;
            }
        }

        public bool IsChecked 
        {
            get
            {
                return aWebElement.Selected;
            }
        }

        public Point ScrollToElement()
        {
            return ((ILocatable)aWebElement).Coordinates.LocationOnScreen;
            //return ((RemoteWebElement)aWebElement).LocationOnScreenOnceScrolledIntoView;
        }
                
        #endregion Public Properties

        #region Public Methods        
        
        public List<IControl> GetChildren(string Locator, LocatorType aLocatorType, ControlType aControlType)
        {
            return Utility.GetChildren(Locator, aLocatorType, aControlType,aWebElement);            
        }

	 	public void Click()
        {
        	aWebElement.Click(); 	
        }
        
        public void DesktopMouseClick()
        {
            Win32.DeskTopMouseClick(this);
        }

        public void SendKeys(string text)
        {
            aWebElement.SendKeys(text);
        }

        #endregion Public Methods       
       
        public void Highlight(Browser aBrowser)
        {
            IJavaScriptExecutor aScriptExecutor = (IJavaScriptExecutor)aBrowser.BrowserHandle;

            for (int i = 0; i < 5; i++)
            {
                //aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", aWebElement,"color: red; border: 4px solid red;");

                object aStyle = aScriptExecutor.ExecuteScript("arguments[0].getAttribute('style');", aWebElement);
                aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", aWebElement,
                                              "border: 3px solid red;");
                Thread.Sleep(50);
                //aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);",
                //    aWebElement, "border: 0px solid red;");

                aScriptExecutor.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);",
                    aWebElement, aStyle);
            }
        }

        public object ExecuteJavaScript(Browser aBrowser, string JavaScript)
        {
            IJavaScriptExecutor aScriptExecutor = (IJavaScriptExecutor)aBrowser.BrowserHandle;

            return aScriptExecutor.ExecuteScript(JavaScript, aWebElement);
            
        }

        public string GetAttributeFromNode(string Attribute)
        {
            return aWebElement.GetAttribute(Attribute);            
        }
    }

    public class SeleniumWebButton : SeleniumWebControls , IButton
    {
        internal SeleniumWebButton(IWebElement aWebElement, ControlType aControlType)
            : base(aWebElement, aControlType)
        { }
        
    }

    public class SeleniumWebEditBox : SeleniumWebControls, IEditBox
    {
        internal SeleniumWebEditBox(IWebElement aWebElement, ControlType aControlType)
            : base(aWebElement, aControlType)
        { }

        public void SendKeys(string aText)
        {
            aWebElement.SendKeys(aText);
            //aWebElement.SendKeys(Keys.Escape);
        }        
    }
    
}
