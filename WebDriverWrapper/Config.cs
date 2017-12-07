// ***********************************************************************
// <copyright file="Config.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Config class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebDriverWrapper
{
	/// <summary>
	/// Config
	/// </summary>
	class Config
	{
		/// <summary>
		/// Gets or sets the driver server path.
		/// </summary>
		/// <value>
		/// The driver server path.
		/// </value>
		public static string DriverServerPath
		{
			get
			{
				return driverServerPath;
			}
		}

		/// <summary>
		/// Gets or sets the native selenium driver.
		/// </summary>
		/// <value>
		/// The native selenium driver.
		/// </value>
		public static string NativeSeleniumDriver
		{
			get
			{
				return nativeSeleniumDriver;
			}		
		}

		/// <summary>
		/// Gets or sets my property.
		/// </summary>
		/// <value>
		/// My property.
		/// </value>
		public string MyProperty { get; set; }
		//public const string IEDriverServerPath = @"D:\MyData\MyWork\WebDriver_Prototype\WebDriver_Test\Drivers";

		/// <summary>
		/// The driver server path
		/// </summary>
		private static string driverServerPath = Directory.GetCurrentDirectory() + @"\Drivers";
		/// <summary>
		/// The native selenium driver
		/// </summary>
		private static string nativeSeleniumDriver = Directory.GetCurrentDirectory() + @"\Drivers\NativeSelenium";
	}
}
