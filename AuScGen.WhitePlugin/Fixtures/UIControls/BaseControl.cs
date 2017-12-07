// ***********************************************************************
// <copyright file="BaseControl.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>BaseControl class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Scrolling;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	/// Class Search Type
	/// </summary>
    public enum SearchType
    {
		/// <summary>
		/// The automation identifier
		/// </summary>
        AutomationId,
		/// <summary>
		/// The name
		/// </summary>
        Name
    }

	/// <summary>
	/// 
	/// </summary>
    public enum ControlType
    {
		/// <summary>
		/// The UI item
		/// </summary>
        UIItem,

		/// <summary>
		/// The button
		/// </summary>
        Button,

		/// <summary>
		/// The edit box
		/// </summary>
        EditBox,

		/// <summary>
		/// The custom
		/// </summary>
        Custom
    }

	/// <summary>
	/// 
	/// </summary>
    public class BaseControl : IControl
    {
        #region Internal Properties

		/// <summary>
		/// Gets or sets my control access.
		/// </summary>
		/// <value>
		/// My control access.
		/// </value>
        protected internal ControlAccess MyControlAccess { get; set; }
		/// <summary>
		/// Gets or sets the control.
		/// </summary>
		/// <value>
		/// The control.
		/// </value>
        protected internal UIItem Control { get; set; }
		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
        public IList<BaseControl> Children 
        { 
            get
            {
                //return myControlAccess.Children;
                return GetChildren(AutomationElement);
            }
        }
        
        #endregion Internal Properties

        #region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseControl"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="controlAccess">The control access.</param>
        public BaseControl(AutomationElement control, ControlAccess controlAccess) 
        {
            MyControlAccess = controlAccess;
            AutomationElement = control;            
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseControl"/> class.
		/// </summary>
        public BaseControl()
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseControl"/> class.
		/// </summary>
		/// <param name="map">The guimap.</param>
		/// <param name="controlName">Name of the control.</param>
        public BaseControl(string map, string controlName)
        {
            //myControlAccess = new ControlAccess();
            this.MapPath = map;
            this.LogicalName = controlName;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseControl"/> class.
		/// </summary>
		/// <param name="map">The GUI map.</param>
		/// <param name="logicalName">Name of the logical.</param>
		/// <param name="controlAccess">The control access.</param>
        public BaseControl(string map, string logicalName, ControlAccess controlAccess)
            :this(map,logicalName)
        {
            MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.UIItem>(MapPath, logicalName);
            Control = MyControlAccess.UIControl;
        }

        #endregion Constructor

        #region Public Properties  
      
		/// <summary>
		/// Gets or sets the guimap path.
		/// </summary>
		/// <value>
		/// The guimap path.
		/// </value>
        protected string MapPath { get; set; }

		/// <summary>
		/// Gets or sets the name of the logical.
		/// </summary>
		/// <value>
		/// The name of the logical.
		/// </value>
        protected string LogicalName { get; set; }

		/// <summary>
		/// Gets the bounds.
		/// </summary>
		/// <value>
		/// The bounds.
		/// </value>
        public Rectangle Bounds
        {
            get
            {
                Rectangle rectangle = new Rectangle();
                rectangle.X = (int)Control.Bounds.X;
                rectangle.Y = (int)Control.Bounds.Y;
                return rectangle;
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
                return new Point((int)Control.ClickablePoint.X, (int)Control.ClickablePoint.Y);
            }
        }

		/// <summary>
		/// Should be used only if white doesn't support the feature you are looking for.
		/// Knowledge of UIAutomation would be required. It would better idea to also raise an issue if you are using it.
		/// </summary>
        public virtual AutomationElement AutomationElement
        {
            get 
            {
                return Control.AutomationElement;
            }

            private set
            {
                Control = new UIItem(value, MyControlAccess.Framework.AppWindow.ActionListener);
            }
        }

		/// <summary>
		/// Gets a value indicating whether this <see cref="BaseControl"/> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
        public virtual bool Enabled
        {
            get 
            {
                return Control.Enabled;
            }
        }

		/// <summary>
		/// Gets the framework.
		/// </summary>
		/// <value>
		/// The framework.
		/// </value>
        public virtual WindowsFramework Framework
        {
            get 
            {
                return Control.Framework;
            }
        }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
        public virtual string Name
        {
            get 
            {
                return Control.Name;
            }
        }

		/// <summary>
		/// Gets the access key.
		/// </summary>
		/// <value>
		/// The access key.
		/// </value>
        public virtual string AccessKey
        {
            get 
            {
                return Control.AccessKey;             
            }
        }

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
        public virtual string Id
        {
            get 
            {
                return Control.Id;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this <see cref="BaseControl"/> is visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if visible; otherwise, <c>false</c>.
		/// </value>
        public virtual bool Visible
        {
            get 
            {
                return Control.Visible;
            }
        }

		/// <summary>
		/// Gets the primary identification.
		/// </summary>
		/// <value>
		/// The primary identification.
		/// </value>
        public virtual string PrimaryIdentification
        {
            get 
            {
                return Control.PrimaryIdentification;
            }
        }

		/// <summary>
		/// Gets the action listener.
		/// </summary>
		/// <value>
		/// The action listener.
		/// </value>
        public virtual ActionListener ActionListener
        {
            get 
            {
                return Control.ActionListener;
            }
        }

		/// <summary>
		/// Gets the scroll bars.
		/// </summary>
		/// <value>
		/// The scroll bars.
		/// </value>
        public virtual IScrollBars ScrollBars
        {
            get 
            {
                return Control.ScrollBars;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is off screen.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is off screen; otherwise, <c>false</c>.
		/// </value>
        public virtual bool IsOffScreen
        {
            get 
            {
                return Control.IsOffScreen;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is focussed.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is focussed; otherwise, <c>false</c>.
		/// </value>
        public virtual bool IsFocussed
        {
            get 
            {
                return Control.IsFocussed;
            }
        }

		/// <summary>
		/// Gets the color of the border.
		/// </summary>
		/// <value>
		/// The color of the border.
		/// </value>
        public virtual Color BorderColor
        {
            get 
            {
                return Control.BorderColor;
            }
        }

		/// <summary>
		/// Gets the visible image.
		/// </summary>
		/// <value>
		/// The visible image.
		/// </value>
        public virtual Bitmap VisibleImage
        {
            get 
            {
                return Control.VisibleImage;
            }
        }

		/// <summary>
		/// Values the of equals.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <param name="compareTo">The compare to.</param>
		/// <returns></returns>
        public virtual bool ValueOfEquals(AutomationProperty property, object compareTo)
        {
            return Control.ValueOfEquals(property, compareTo);
        }

		/// <summary>
		/// Rights the click at.
		/// </summary>
		/// <param name="point">The point.</param>
        public virtual void RightClickAt(System.Windows.Point point)
        {
            Control.RightClickAt(point);
        }

		/// <summary>
		/// Rights the click.
		/// </summary>
        public virtual void RightClick()
        {
            Control.RightClick();
        }

		/// <summary>
		/// Focuses this instance.
		/// </summary>
        public virtual void Focus()
        {
            Control.Focus();
        }

		/// <summary>
		/// Visits the specified window control visitor.
		/// </summary>
		/// <param name="windowControlVisitor">The window control visitor.</param>
        public virtual void Visit(TestStack.White.WindowControlVisitor windowControlVisitor)
        {
            Control.Visit(windowControlVisitor);
        }

		/// <summary>
		/// Gets the bounds.
		/// </summary>
		/// <value>
		/// The bounds.
		/// </value>
        System.Windows.Rect IUIItem.Bounds
        {
            get 
            {
                return Control.Bounds;
            }
        }

		/// <summary>
		/// Performs mouse click at the center of this item
		/// </summary>
        public void Click()
        {
            Control.Click();
        }

		/// <summary>
		/// Gets the clickable point.
		/// </summary>
		/// <value>
		/// The clickable point.
		/// </value>
        System.Windows.Point IUIItem.ClickablePoint
        {
            get 
            {
                return Control.ClickablePoint;
            }
        }

		/// <summary>
		/// Performs mouse double click at the center of this item
		/// </summary>
        public void DoubleClick()
        {
            Control.DoubleClick();
        }

		/// <summary>
		/// Draws the highlight.
		/// </summary>
        public void DrawHighlight()
        {
            Control.DrawHighlight();
        }

		/// <summary>
		/// Enters the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
        public void Enter(string value)
        {
            Control.Enter(value);
        }

		/// <summary>
		/// Provides the Error on this UIItem. This would return Error object when this item has ErrorProvider displayed next to it.
		/// </summary>
		/// <param name="window"></param>
		/// <returns></returns>
        public string ErrorProviderMessage(TestStack.White.UIItems.WindowItems.Window window)
        {
           return Control.ErrorProviderMessage(window);
        }

		/// <summary>
		/// Uses the Raw View provided by UIAutomation to find elements within this UIItem. RawView sometimes contains extra AutomationElements. This is internal to
		/// white although made public. Should be used only if the standard approaches dont work. Also if you end up using it please raise an issue
		/// so that it can be fixed.
		/// Please understand that calling this method on any UIItem which has a lot of child AutomationElements might result in very bad performance.
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>
		/// null or found AutomationElement
		/// </returns>
        public AutomationElement GetElement(TestStack.White.UIItems.Finders.SearchCriteria searchCriteria)
        {
            return Control.GetElement(searchCriteria);
        }

		/// <summary>
		/// Internal to white and intended to be used for white recording
		/// </summary>
		/// <param name="eventListener"></param>
        public virtual void HookEvents(TestStack.White.Recording.UIItemEventListener eventListener)
        {
            Control.HookEvents(eventListener);
        }

		/// <summary>
		/// Perform keyboard action on this UIItem
		/// </summary>
		/// <param name="key"></param>
        public void KeyIn(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys key)
        {
            Control.KeyIn(key);
        }

		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		/// <exception cref="System.NotImplementedException"></exception>
        public System.Windows.Point Location
        {
            get;
            set;
        }

		/// <summary>
		/// Logs the structure.
		/// </summary>
        public void LogStructure()
        {
            Control.LogStructure();
        }

		/// <summary>
		/// Names the matches.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
        public bool NameMatches(string text)
        {
            return Control.NameMatches(text);
        }

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="value">The value.</param>
        public void SetValue(object value)
        {
            Control.SetValue(value);
        }

		/// <summary>
		/// Internal to white and intended to be used for white recording
		/// </summary>
        public virtual void UnHookEvents()
        {
            Control.UnHookEvents();
        }

		/// <summary>
		/// Actions the performed.
		/// </summary>
		/// <param name="action">The action.</param>
        public void ActionPerformed(TestStack.White.UIItems.Actions.Action action)
        {
            Control.ActionPerformed(action);
        }

		/// <summary>
		/// Actions the performing.
		/// </summary>
		/// <param name="uiItem">The UI item.</param>
        public void ActionPerforming(UIItem uiItem)
        {
            Control.ActionPerforming(uiItem);
        }
        
        #endregion Public Properties

        #region Private Methods

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <param name="element_in">The element_in.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">element_in</exception>
        internal List<BaseControl> GetChildren(AutomationElement element_in)
        {
            //Trace.TraceInformation(String.Format("{0}: Try to get MSUIA children from AutomationElement...", ""));
            if (null == element_in)
            {
                throw new ArgumentNullException("element_in");
            }
            List<AutomationElement> automationElementList = new List<AutomationElement>(0);
            AutomationElement automationElementChild = TreeWalker.RawViewWalker.GetFirstChild(element_in);

            while (null != automationElementChild)
            {
                automationElementList.Add(automationElementChild);
                automationElementChild = TreeWalker.RawViewWalker.GetNextSibling(automationElementChild);
            }
            //Trace.TraceInformation(String.Format("{0}: MSUIA children from AutomationElement found.", ""));
            //return aList;

            List<BaseControl> controls = new List<BaseControl>();
            automationElementList.ForEach(auto => 
            {
                controls.Add(new BaseControl(auto, MyControlAccess));
            });

            return controls;
        }

        #endregion Private Methods

    }
}
