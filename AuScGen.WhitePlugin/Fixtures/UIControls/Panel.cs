// ***********************************************************************
// <copyright file="Panel.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Panel class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestStack.White.UIItems;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Panel
	/// </summary>
    public class Panel : BaseControl , IPanel
    {
		/// <summary>
		/// The control container
		/// </summary>
        private UIItemContainer controlContainer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Panel"/> class.
		/// </summary>
        /// <param name="map">The GUI map.</param>
        /// <param name="logicalName">Name of the logical.</param>
        /// <param name="controlAccess">The control access.</param>
        public Panel(string map, string logicalName, ControlAccess controlAccess)
            : base(map, logicalName)
        {
            this.MyControlAccess = controlAccess;
            MyControlAccess.InitializeControl<TestStack.White.UIItems.Panel>(this.MapPath, logicalName);
            this.Control = MyControlAccess.UIControl;
            this.controlContainer = (UIItemContainer)Control;
        }

		/// <summary>
		/// Gets the panel.
		/// </summary>
		/// <value>
		/// The panel.
		/// </value>
        protected internal TestStack.White.UIItems.Panel PanelControl
        {
            get
            {
                return (TestStack.White.UIItems.Panel)Control;
            }
        }

		/// <summary>
		/// Gets the panel container.
		/// </summary>
		/// <value>
		/// The panel container.
		/// </value>
        private TestStack.White.UIItems.Panel PanelContainer
        {
            get
            {
                return (TestStack.White.UIItems.Panel)this.controlContainer;
            }
        }

		/// <summary>
		/// Finds UIItem which matches specified type and searchCriteria using the default BusyTimeout. Look at documentation of SearchCriteria for details on it.
		/// </summary>
		/// <param name="searchCriteria">Criteria provided to search IUIItem</param>
		/// <returns>
		/// First items matching the criteria
		/// </returns>
        public IUIItem Get(TestStack.White.UIItems.Finders.SearchCriteria searchCriteria)
        {
            return this.PanelContainer.Get(searchCriteria);
        }

		/// <summary>
		/// Finds UIItem which matches specified type and searchCriteria. Type supplied need not be supplied again in SearchCondition.
		/// <!--e.g. in Get<Button>(SearchCriteria.ByAutomationId("OK").ByControlType(typeof(Button)).Indexed(1) the ByControlType(typeof(Button)) part
		/// is redundant. Look at documentation of SearchCriteria for details on it.-->
		/// </summary>
        /// <typeparam name="T">IUIItem</typeparam>
		/// <param name="searchCriteria">Criteria provided to search UIItem</param>
		/// <returns>
		/// First items matching the type and criteria
		/// </returns>
		/// <code></code>
        public T Get<T>(TestStack.White.UIItems.Finders.SearchCriteria searchCriteria) where T : IUIItem
        {
            return this.PanelContainer.Get<T>(searchCriteria);
        }

		/// <summary>
		/// Finds UIItem which matches specified type and identification.
		/// In case of multiple items of this type the first one found would be returned which cannot be guaranteed to be the same across multiple
		/// invocations. For managed applications this is name given to controls in the application code.
		/// For unmanaged applications this is text of the control or label next to it if it doesn't have well defined text.
		/// <!--e.g. TextBox doesn't have any predefined text of its own as it can be changed at runtime by user, hence is identified by the label next to it.
		/// If there is no label then Get<T> or Get<T>(SearchCriteria) method can be used.-->
		/// </summary>
		/// <typeparam name="T">IUIItem implementation</typeparam>
		/// <param name="primaryIdentification">For managed application this is the name provided in application code and unmanaged application this is
		/// the text or label next to it based identification</param>
		/// <returns>
		/// First item of supplied type and identification
		/// </returns>
        public T Get<T>(string primaryIdentification) where T : IUIItem
        {
            return this.PanelContainer.Get<T>(primaryIdentification);
        }

		/// <summary>
		/// Finds UIItem which matches specified type. Useful for non managed applications where controls are not identified by AutomationId, like in
		/// Managed applications. In case of multiple items of this type the first one found would be returned which cannot be guaranteed to be the same
		/// across multiple invocations.
		/// </summary>
		/// <typeparam name="T">IUIItem type e.g. Button, TextBox</typeparam>
		/// <returns>
		/// First item of supplied type
		/// </returns>
        public T Get<T>() where T : IUIItem
        {
            return this.PanelContainer.Get<T>();
        }

		/// <summary>
		/// Gets the multiple.
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
        public IUIItem[] GetMultiple(TestStack.White.UIItems.Finders.SearchCriteria criteria)
        {
            return this.PanelContainer.GetMultiple(criteria);
        }

		/// <summary>
		/// Gets the tool tip on.
		/// </summary>
		/// <param name="uiItem">The UI item.</param>
		/// <returns></returns>
        public ToolTip GetToolTipOn(UIItem uiItem)
        {
            return this.PanelContainer.GetToolTipOn(uiItem);
        }

		/// <summary>
		/// Gets the tool tip.
		/// </summary>
		/// <value>
		/// The tool tip.
		/// </value>
        public ToolTip ToolTip
        {
            get 
            {
                return this.PanelContainer.ToolTip;
            }
        }
    }
}
