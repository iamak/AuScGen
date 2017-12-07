using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDriverWrapper;
using System.Drawing;
using System.Collections.ObjectModel;

namespace UIAccess
{
    public class WebControl
    {
        private ControlAccess myControlAccess;

        internal IControl Control;

        //private Browser myBrowser;

        //private LocatorType myLocatorType;

        //private string myLocator;

        internal ControlType myControlType;

        #region ctor

        public WebControl(Browser aBrowser,LocatorType aLocatorType,string aLocator,ControlType aControlType)
        {
            myControlAccess = new ControlAccess();
            myControlAccess.Browser = aBrowser;
            myControlAccess.LocatorType = aLocatorType;
            myControlAccess.Locator = aLocator;
            myControlAccess.ControlType = aControlType;
            myControlAccess.IntializeControlAccess();
            //Control = myControlAccess.GetControl();
        }
        
        public WebControl(Browser aBrowser,Locator aLocator)
        {
            myControlAccess = new ControlAccess();
            myControlAccess.Browser = aBrowser;
            myControlAccess.LocatorType = aLocator.LocatorType;
            myControlAccess.Locator = aLocator.ControlLocator;
            myControlAccess.ControlType = ControlType.Custom;
            myControlAccess.IntializeControlAccess();
            //Control = myControlAccess.GetControl();
        }

        #endregion ctor

        #region Public Properties

        public bool Enabled 
        { 
            get 
            {
                return Control.Enabled;
            }
            
        }

        public Rectangle BoundingRectangle
        {
            get { return Control.BoundingRectangle; }
        }

        public Point ClickablePoint 
        {
            get { return Control.ClickablePoint; }
        }

        public bool Visible
        {
            get { return Control.Visible; }
               
        }

        public string Text 
        { 
            get { return Control.Text; }
        }

        public bool IsChecked
        {
            get { return Control.IsChecked; }
        }

        public string TagName
        {
            get { return Control.TagName; }
        }

        public Actions Action 
        { 
            get
            {
                return myControlAccess.Action;
            }
        
        }

        public IControl SeleniumControl 
        {
            get
            {

                return Control;
            }            
        }

        #endregion Public Properties

        #region Public Methods

        public Point ScrollToElement()
        {
            return Control.ScrollToElement();
        }

        public bool IsControlPresent()
        {
            return myControlAccess.IsElementPresent();
        }

        public void GetControl()
        {
            Control = myControlAccess.GetControl();
        }

        public ReadOnlyCollection<IControl> GetControls()
        {
            return myControlAccess.GetControls();
        }

        public List<WebControl> GetChildren(Locator aLocator,ControlType aControlType)
        {
            List<IControl> aIControlList = new List<IControl>();
            aIControlList = myControlAccess.GetChildren(aLocator.ControlLocator, aLocator.LocatorType, aControlType);

            return Utility.GetWebControlsFromIControlList(aIControlList, myControlAccess.Browser, aLocator, aControlType);
        }

        public void Highlight()
        {
            Control.Highlight(myControlAccess.Browser);
        }
        
        public void ClickAt()
        {
        	myControlAccess.ClickAt();
        }
        
        public void Click()
        {
        	Control.Click();
        }

        public void DesktopMouseClick()
        {
            Control.DesktopMouseClick();
        }

        public string GetAttribute(string AttributeName)
        {
            return Control.GetAttributeFromNode(AttributeName);
        }

        public Dictionary<string, object> GetAttributes()
        {
            return (Dictionary<string,object>)Executejavascript(@"var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;");
        }

        public object Executejavascript(string JavaScript)
        {
            return Control.ExecuteJavaScript(myControlAccess.Browser,JavaScript);
        }
        //public WebControl Parent 
        //{ 
        //    get
        //    {
        //        Utility.GetWebControlFromIContol(Parent,myControlAccess.Browser,)
        //        return Parent;
        //    }
        //}
        #endregion Public Methods      
      
    }

    public class WebButton : WebControl
    {
        public WebButton(Browser aBrowser, Locator aLocator)
            :base(aBrowser,aLocator.LocatorType,aLocator.ControlLocator,ControlType.Button)
        { }
        
        private IButton Button
        {
            get
            {
                return (IButton)Control;
            }
        }

        public new void Click()
        {
            Button.Click();
        }     
      
    }

    public class WebEditBox :  WebControl
    {
        public WebEditBox(Browser aBrowser, Locator aLocator)
            : base(aBrowser, aLocator.LocatorType,aLocator.ControlLocator, ControlType.EditBox)
        { }

        private IEditBox EditBox
        {
            get 
            {
                return (IEditBox)Control;
            }
        }

        public void SendKeys(string Text)
        {
            EditBox.SendKeys(Text);
        }

    } 
    

}
