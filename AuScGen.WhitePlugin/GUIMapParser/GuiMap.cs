// ***********************************************************************
// <copyright file="GuiMap.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>GuiMap class</summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.WhiteFramework
{
	/// <summary>
	///		Class Find By
	/// </summary>
	enum FindBy
	{
		/// <summary>
		/// The text
		/// </summary>
		Text,
		/// <summary>
		/// The automation identifier
		/// </summary>
		AutomationId,
		/// <summary>
		/// The class name
		/// </summary>
		ClassName,
		/// <summary>
		/// The control type
		/// </summary>
		ControlType,
		/// <summary>
		/// The framework identifier
		/// </summary>
		FrameworkId,
		/// <summary>
		/// The native property
		/// </summary>
		NativeProperty,
		/// <summary>
		/// The multiple
		/// </summary>
		Multiple
	}

	/// <summary>
	/// 
	/// </summary>
	class GuiMap
	{
		/// <summary>
		/// The identifier
		/// </summary>
		private string id;
		/// <summary>
		/// The class name
		/// </summary>
		private string className;
		/// <summary>
		/// The control type
		/// </summary>
		private string controlType;
		/// <summary>
		/// The framework
		/// </summary>
		private string framework;
		/// <summary>
		/// The native property
		/// </summary>
		private string nativeProperty;

		/// <summary>
		/// The logical name
		/// </summary>
		private string logicalName;
		/// <summary>
		/// The identification type
		/// </summary>
		private string identificationType;
		/// <summary>
		/// The mult identities
		/// </summary>
		private Dictionary<string, string> multIdentities = new Dictionary<string, string>();

		/// <summary>
		/// Gets or sets the mult identities.
		/// </summary>
		/// <value>
		/// The mult identities.
		/// </value>
		public Dictionary<string, string> MultIdentities
		{
			get { return multIdentities; }
			//set { multIdentities = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GuiMap"/> class.
		/// </summary>
		public GuiMap() { }

		/// <summary>
		/// Gets or sets the name of the logical.
		/// </summary>
		/// <value>
		/// The name of the logical.
		/// </value>
		public string LogicalName
		{
			get { return logicalName; }
			set { logicalName = value; }
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
        //protected internal string Id
        //{
        //    //get { return id; }
        //    set { id = value; }
        //}
		/// <summary>
		/// Gets or sets the name of the class.
		/// </summary>
		/// <value>
		/// The name of the class.
		/// </value>
		protected internal string ClassName
		{
			get { return className; }
			set { className = value; }
		}

		/// <summary>
		/// Gets or sets the type of the control.
		/// </summary>
		/// <value>
		/// The type of the control.
		/// </value>
		protected internal string ControlType
		{
			get { return controlType; }
			set { controlType = value; }
		}

		/// <summary>
		/// Gets or sets the native property.
		/// </summary>
		/// <value>
		/// The native property.
		/// </value>
		protected internal string NativeProperty
		{
			get { return nativeProperty; }
			set { nativeProperty = value; }
		}

		/// <summary>
		/// Gets or sets the framework.
		/// </summary>
		/// <value>
		/// The framework.
		/// </value>
		protected internal string Framework
		{
			get { return framework; }
			set { framework = value; }
		}

		/// <summary>
		/// Gets or sets the type of the identification.
		/// </summary>
		/// <value>
		/// The type of the identification.
		/// </value>
		protected internal string IdentificationType
		{
			get { return identificationType; }
			set { identificationType = value; }
		}
	}
}
