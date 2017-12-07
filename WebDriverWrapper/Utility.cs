
// ***********************************************************************
// <copyright file="Utility.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Utility class</summary>
// ***********************************************************************
using System.Collections.Generic;
using OpenQA.Selenium;

namespace WebDriverWrapper
{
	/// <summary>
	/// Utility
	/// </summary>
	public class Utility
	{
		/// <summary>
		/// Gets the control from web element.
		/// </summary>
		/// <param name="webElement">a web element.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
		internal static IControl GetControlFromWebElement(IWebElement webElement, ControlType controlType, ControlAccess access)
		{
			if (controlType == ControlType.Button)
			{
				return new SeleniumWebButton(webElement, controlType, access);
			}

			if (controlType == ControlType.EditBox)
			{
				return new SeleniumWebEditBox(webElement, controlType, access);
			}

			if (controlType == ControlType.Calender)
			{
				return new SeleniumWebCalender(webElement, controlType, access);
			}

			if (controlType == ControlType.Custom)
			{
				return new SeleniumWebControls(webElement, controlType, access);
			}

			if (controlType == ControlType.ComboBox)
			{
				return new SeleniumWebCombobox(webElement, controlType, access);
			}

			if (controlType == ControlType.CheckBox)
			{
				return new SeleniumWebCheckBox(webElement, controlType, access);
			}

			if (controlType == ControlType.Dialog)
			{
				return new SeleniumWebDialog(webElement, controlType, access);
			}

			if (controlType == ControlType.Frame)
			{
				return new SeleniumWebFrame(webElement, controlType, access);
			}

			if (controlType == ControlType.Image)
			{
				return new SeleniumWebImage(webElement, controlType, access);
			}

			if (controlType == ControlType.Label)
			{
				return new SeleniumWebLabel(webElement, controlType, access);
			}

			if (controlType == ControlType.Link)
			{
				return new SeleniumWebLink(webElement, controlType, access);
			}

			if (controlType == ControlType.ListBox)
			{
				return new SeleniumWebListBox(webElement, controlType, access);
			}

			if (controlType == ControlType.Page)
			{
				return new SeleniumWebPage(webElement, controlType, access);
			}

			if (controlType == ControlType.RadioButton)
			{
				return new SeleniumWebRadioButton(webElement, controlType, access);
			}

			if (controlType == ControlType.SpanArea)
			{
				return new SeleniumWebSpanArea(webElement, controlType, access);
			}

			if (controlType == ControlType.WebTable)
			{
				return new SeleniumWebTable(webElement, controlType, access);
			}

			if (controlType == ControlType.WebRow)
			{
				return new SeleniumWebRow(webElement, controlType, access);
			}

			if (controlType == ControlType.WebCell)
			{
				return new SeleniumWebCell(webElement, controlType, access);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <param name="locator">The locator.</param>
		/// <param name="locatorType">Type of a locator.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <param name="webElement">a web element.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
		internal static List<IControl> GetChildren(string locator, LocatorType locatorType, ControlType controlType, IWebElement webElement, ControlAccess access)
		{
			if (locatorType == LocatorType.Id)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.Id(locator)), controlType, access);
			}

			if (locatorType == LocatorType.Name)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.Name(locator)), controlType, access);
			}

			if (locatorType == LocatorType.TagName)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.TagName(locator)), controlType, access);
			}

			if (locatorType == LocatorType.Xpath)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.XPath(locator)), controlType, access);
			}

			if (locatorType == LocatorType.ClassName)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.ClassName(locator)), controlType, access);
			}

			if (locatorType == LocatorType.Css)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.CssSelector(locator)), controlType, access);
			}

			if (locatorType == LocatorType.LinkText)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.LinkText(locator)), controlType, access);
			}

			if (locatorType == LocatorType.PartialLinkText)
			{
				return Utility.GetControlsFromWebElements(webElement.FindElements(By.PartialLinkText(locator)), controlType, access);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the controls from web elements.
		/// </summary>
		/// <param name="webElements">a web elements.</param>
		/// <param name="controlType">Type of a control.</param>
		/// <param name="access">The access.</param>
		/// <returns></returns>
		internal static List<IControl> GetControlsFromWebElements(IEnumerable<IWebElement> webElements, ControlType controlType, ControlAccess access)
		{
			List<IControl> control = new List<IControl>();

			foreach (IWebElement webElement in webElements)
			{
				if (controlType == ControlType.Button)
				{
					control.Add(new SeleniumWebButton(webElement, controlType, access));
				}

				if (controlType == ControlType.EditBox)
				{
					control.Add(new SeleniumWebEditBox(webElement, controlType, access));
				}

				if (controlType == ControlType.Custom)
				{
					control.Add(new SeleniumWebControls(webElement, controlType, access));
				}

				if (controlType == ControlType.Calender)
				{
					control.Add(new SeleniumWebCalender(webElement, controlType, access));
				}

				if (controlType == ControlType.ComboBox)
				{
					control.Add(new SeleniumWebCombobox(webElement, controlType, access));
				}

				if (controlType == ControlType.CheckBox)
				{
					control.Add(new SeleniumWebCheckBox(webElement, controlType, access));
				}

				if (controlType == ControlType.Dialog)
				{
					control.Add(new SeleniumWebDialog(webElement, controlType, access));
				}

				if (controlType == ControlType.Frame)
				{
					control.Add(new SeleniumWebFrame(webElement, controlType, access));
				}

				if (controlType == ControlType.Image)
				{
					control.Add(new SeleniumWebImage(webElement, controlType, access));
				}

				if (controlType == ControlType.Label)
				{
					control.Add(new SeleniumWebLabel(webElement, controlType, access));
				}

				if (controlType == ControlType.Link)
				{
					control.Add(new SeleniumWebLink(webElement, controlType, access));
				}

				if (controlType == ControlType.ListBox)
				{
					control.Add(new SeleniumWebListBox(webElement, controlType, access));
				}

				if (controlType == ControlType.Page)
				{
					control.Add(new SeleniumWebPage(webElement, controlType, access));
				}

				if (controlType == ControlType.RadioButton)
				{
					control.Add(new SeleniumWebRadioButton(webElement, controlType, access));
				}

				if (controlType == ControlType.SpanArea)
				{
					control.Add(new SeleniumWebSpanArea(webElement, controlType, access));
				}

				if (controlType == ControlType.WebTable)
				{
					control.Add(new SeleniumWebTable(webElement, controlType, access));
				}

				if (controlType == ControlType.WebRow)
				{
					control.Add(new SeleniumWebRow(webElement, controlType, access));
				}

				if (controlType == ControlType.WebCell)
				{
					control.Add(new SeleniumWebCell(webElement, controlType, access));
				}
			}
			return control;
		}

		/// <summary>
		/// Gets the by from locator.
		/// </summary>
		/// <param name="locatorType">Type of the locator.</param>
		/// <param name="locator">The locator.</param>
		/// <returns></returns>
		internal static By GetByFromLocator(LocatorType locatorType, string locator)
		{
			switch (locatorType)
			{
				case LocatorType.ClassName:
					return By.ClassName(locator);

				case LocatorType.Css:
					return By.CssSelector(locator);

				case LocatorType.Id:
					return By.Id(locator);

				case LocatorType.LinkText:
					return By.LinkText(locator);

				case LocatorType.Name:
					return By.Name(locator);

				case LocatorType.PartialLinkText:
					return By.PartialLinkText(locator);

				case LocatorType.TagName:
					return By.TagName(locator);

				case LocatorType.Xpath:
					return By.XPath(locator);

				default:
					return By.XPath(locator);
			}
		}
	}
}