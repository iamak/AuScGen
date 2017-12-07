// ***********************************************************************
// <copyright file="WhitePlugin.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>WhitePlugin class</summary>
// ***********************************************************************
using System.ComponentModel.Composition;
using Framework;

namespace AuScGen.WhiteFramework
{

    /// <summary>
    ///		Class White Plug In
    /// </summary>
    [Export(typeof(IPlugin))]
    public class WhitePlugin : IPlugin
    {
		/// <summary>
		/// a control access
		/// </summary>
        private ControlAccess controlAccess;
		/// <summary>
		/// Gets my control access.
		/// </summary>
		/// <value>
		/// My control access.
		/// </value>
        internal ControlAccess MyControlAccess 
        { 
            get
            {
                if(null == controlAccess)
                {
                    controlAccess = new ControlAccess();
                    controlAccess.ProcessName = ProcessName;
                    controlAccess.AppWindowName = AppWindowName;
                    controlAccess.InitializeAppWindow();
                }
                return controlAccess;
            }
        }

		/// <summary>
		/// Gets or sets the name of the process.
		/// </summary>
		/// <value>
		/// The name of the process.
		/// </value>
        public string ProcessName { get; set; }
		/// <summary>
		/// Gets or sets the name of the application window.
		/// </summary>
		/// <value>
		/// The name of the application window.
		/// </value>
        public string AppWindowName { get; set; }
		/// <summary>
		/// Gets or sets the GUI map path.
		/// </summary>
		/// <value>
		/// The GUI map path.
		/// </value>
        public string MapPath { get; set; }

		/// <summary>
		/// Bases the control.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public BaseControl BaseControl(string logicalName)
        {
            return new BaseControl(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Buttons the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Button Button(string logicalName)
        {
            return new Button(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Checkboxes the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Checkbox CheckBox(string logicalName)
        {
            return new Checkbox(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// RadioButtons the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public RadioButton RadioButton(string logicalName)
        {
            return new RadioButton(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Labels the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Label Label(string logicalName)
        {
            return new Label(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Dates the time picker.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public DateTimePicker DateTimePicker(string logicalName)
        {
            return new DateTimePicker(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Groups the box.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public GroupBox GroupBox(string logicalName)
        {
            return new GroupBox(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Hyperlinks the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Hyperlink Hyperlink(string logicalName)
        {
            return new Hyperlink(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Panels the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Panel Panel(string logicalName)
        {
            return new Panel(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Progresses the bar.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public ProgressBar ProgressBar(string logicalName)
        {
            return new ProgressBar(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Lists the item.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public ListItem ListItem(string logicalName)
        {
            return new ListItem(MapPath, logicalName, MyControlAccess);
        }

		/// <summary>
		/// Tabs the specified logical name.
		/// </summary>
		/// <param name="logicalName">Name of the logical.</param>
		/// <returns></returns>
        public Tab Tab(string logicalName)
        {
            return new Tab(MapPath, logicalName, MyControlAccess );
        }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
        public string Description
        {
            get
            {
                return "White Plugin";
            }
            set
            {
                Description = value;
            }
        }
    }
}
