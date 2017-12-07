// ***********************************************************************
// <copyright file="WebComboBox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebComboBox class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverWrapper;
using WebDriverWrapper.IControlHierarchy;

namespace UIAccess.WebControls
{
    /// <summary>
    /// WebComboBox
    /// </summary>
    public class WebComboBox : WebControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebComboBox"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebComboBox(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.ComboBox)
        { }

        /// <summary>
        /// Gets the ComboBox.
        /// </summary>
        /// <value>
        /// The ComboBox.
        /// </value>
        private ICombobox ComboBox
        {
            get
            {
                return (ICombobox)ControlObject;
            }
        }

		/// <summary>
		/// Gets the get all options.
		/// </summary>
		/// <value>
		/// The get all options.
		/// </value>
		public ReadOnlyCollection<string> GetAllOptions
		{
			get
			{
				return this.ComboBox.GetAllOptions();
			}
		}

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="textOption">The text option.</param>
        public void SelectByText(string textOption)
        {
            this.ComboBox.SelectByText(textOption);
        }

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="textOption">The text option.</param>
        /// <param name="timeout">The time out.</param>
        public void SelectByText(string textOption, int timeout)
        {
            this.ComboBox.SelectByText(textOption, timeout);
        }

        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectByIndex(int index)
        {
            this.ComboBox.SelectByIndex(index);
        }

        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="timeout">The time out.</param>
        public void SelectByIndex(int index, int timeout)
        {
            this.ComboBox.SelectByIndex(index, timeout);
        }

        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SelectByValue(string value)
        {
            this.ComboBox.SelectByValue(value);
        }

        /// <summary>
        /// Deselects all.
        /// </summary>
        public void DeselectAll()
        {
            this.ComboBox.DeselectAll();
        }

        /// <summary>
        /// Deselects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void DeselectByIndex(int index)
        {
            this.ComboBox.DeselectByIndex(index);
        }

        /// <summary>
        /// Deselects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void DeselectByText(string text)
        {
           this.ComboBox.DeselectByText(text);
        }

        /// <summary>
        /// Deselects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void DeselectByValue(string value)
        {
            this.ComboBox.DeselectByValue(value);
        }

        /// <summary>
        /// Natives the select.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="childrenXPath">The children xpath.</param>
        /// <param name="timeout">The time out.</param>
        public void NativeSelect(string text, string childrenXPath, int timeout)
        {
            this.ComboBox.Click();

           // var test = this.ComboBox.WaitForChildren(childrenXpath, timeout);

            foreach (IControl option in this.ComboBox.WaitForChildren(childrenXPath, timeout))
            {
                if (!option.Text.Equals(text))
                {
                    this.ComboBox.SendKeys(WebDriverWrapper.Keys.KeyDown);
                }
                else
                {
                    break;
                }
            }
            if (childrenXPath.Contains("optgroup"))
            {
                this.ComboBox.SendKeys(WebDriverWrapper.Keys.KeyDown);
                this.ComboBox.SendKeys(WebDriverWrapper.Keys.Enter);
            }
            else
            {
                this.ComboBox.SendKeys(WebDriverWrapper.Keys.Enter);
            }
        }        
    }
}
