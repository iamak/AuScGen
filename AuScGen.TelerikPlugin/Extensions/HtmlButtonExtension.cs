// ***********************************************************************
// <copyright file="HtmlButtonExtension.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>HtmlButtonExtension class</summary>
// ***********************************************************************
using System.Windows.Forms;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen
{
	/// <summary>
	///		Class HtmlButtonExtension
	/// </summary>
	public static class HtmlButtonExtension
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Initializes the <see cref="HtmlButtonExtension"/> class.
		/// </summary>
		static HtmlButtonExtension()
		{
			log4net.ThreadContext.Properties["myContext"] = "Logging from HtmlSelectExtension Class";
			Logger.Debug("Inside HtmlButtonExtension Constructor!");
		}

		/// <summary>
		/// Types the enter key.
		/// </summary>
		/// <param name="control">The control.</param>
		public static void TypeEnterKey(HtmlControl control)
		{
			control.Focus();
			control.OwnerBrowser.Manager.Desktop.KeyBoard.KeyPress(Keys.Enter);
		}
	}
}
