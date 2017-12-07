// ***********************************************************************
// <copyright file="SeleniumWebCombobox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebCombobox class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    ///     Class SeleniumWebCombobox
    /// </summary>
    public class SeleniumWebCombobox : SeleniumWebControls, ICombobox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebCombobox"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebCombobox(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        { }

        /// <summary>
        /// Gets all options.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<string> GetAllOptions()
        {
            SelectElement element = new SelectElement(WebElement);
            IList<IWebElement> options = element.Options;
            List<string> optionsToReturn = new List<string>();

            foreach (IWebElement option in options)
            {
                optionsToReturn.Add(option.Text);
            }
            return optionsToReturn.AsReadOnly();
        }

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="option">The text option.</param>
        public void SelectByText(string option)
        { 
            SelectElement element = new SelectElement(WebElement);
            element.SelectByText(option);
        }

        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SelectByIndex(int index)
        {
            SelectElement element = new SelectElement(WebElement);
            element.SelectByIndex(index);
        }

        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SelectByValue(string value)
        {
            SelectElement element = new SelectElement(WebElement);
            element.SelectByValue(value);
        }
        /// <summary>
        /// Deselects all.
        /// </summary>
        public void DeselectAll()
        {
            SelectElement element = new SelectElement(WebElement);
            element.DeselectAll();
        }

        /// <summary>
        /// Deselects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        public void DeselectByIndex(int index)
        {
            SelectElement element = new SelectElement(WebElement);
            element.DeselectByIndex(index);
        }

        /// <summary>
        /// Deselects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void DeselectByText(string text)
        {
            SelectElement element = new SelectElement(WebElement);
            element.DeselectByText(text);
        }

        /// <summary>
        /// Deselects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void DeselectByValue(string value)
        {
            SelectElement element = new SelectElement(WebElement);
            element.DeselectByValue(value);
        }

        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="maxTimeout">The maximum timeout.</param>
        public void SelectByIndex(int index, int maxTimeout)
        {
            DateTime start;
            double timeElapsed = 0;
            SelectElement element = new SelectElement(WebElement);

            start = DateTime.Now;

            while (element.Options.Count <= 1 && timeElapsed < maxTimeout)
            {
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            if (element.Options.Count() >= 1)
            {
                this.SelectByIndex(index);
                //Logger.Debug(string.Format("Inside HtmlSelectExtension , option available in {0}ms", timeElapsed));
            }
            else
            {
                //Logger.Debug(string.Format("Inside HtmlSelectExtension , option not available in {0}ms", timeElapsed));
            }          
        }

        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxTimeout">The maximum timeout.</param>
        public void SelectByText(string text, int maxTimeout)
        {
            DateTime start;
            double timeElapsed = 0;
            SelectElement element = new SelectElement(WebElement);

            start = DateTime.Now;

            while (element.Options.Where(option => option.Text.Equals(text)).Count() == 0 && timeElapsed < maxTimeout)
            {
                timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
            }

            if (element.Options.Where(option => option.Text.Equals(text)).Count() != 0)
            {
                this.SelectByText(text);
                //Logger.Debug(string.Format("Inside HtmlSelectExtension , option available in {0}ms", timeElapsed));
            }          
            else
            {
                // Logger.Debug(string.Format("Inside HtmlSelectExtension , option not available in {0}ms", timeElapsed));
            }
        }
    }
}   
