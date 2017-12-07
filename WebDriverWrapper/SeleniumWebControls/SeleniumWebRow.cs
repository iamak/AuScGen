// ***********************************************************************
// <copyright file="SeleniumWebRow.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebRow class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebDriverWrapper.IControlHierarchy;

namespace WebDriverWrapper
{
    /// <summary>
    /// SeleniumWebRow
    /// </summary>
    public class SeleniumWebRow : SeleniumWebControls, IWebRow
    {
        /// <summary>
        /// The control access
        /// </summary>
        private ControlAccess controlAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebRow"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebRow(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        {
            this.controlAccess = access;
        }

        /// <summary>
        /// Gets all cells.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<SeleniumWebCell> GetAllCells()
        {
            return Utility.GetControlsFromWebElements(this.WebElement.FindElements(By.TagName("td")), ControlType.WebCell, this.controlAccess).Cast<SeleniumWebCell>().ToList().AsReadOnly();
        }
    }
}