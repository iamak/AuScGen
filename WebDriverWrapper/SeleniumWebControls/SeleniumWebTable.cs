// ***********************************************************************
// <copyright file="SeleniumWebTable.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>SeleniumWebTable class</summary>
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
    /// SeleniumWebTable
    /// </summary>
    public class SeleniumWebTable : SeleniumWebControls, IWebTable
    {
        /// <summary>
        /// The control access
        /// </summary>
        private ControlAccess controlAccess;
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumWebTable"/> class.
        /// </summary>
        /// <param name="webElement">a web element.</param>
        /// <param name="controlType">Type of a control.</param>
        /// <param name="access">The access.</param>
        internal SeleniumWebTable(IWebElement webElement, ControlType controlType, ControlAccess access)
            : base(webElement, controlType, access)
        {
            this.controlAccess = access;
        }

        /// <summary>
        /// Gets all rows.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<SeleniumWebRow> GetAllRows()
        {
            return Utility.GetControlsFromWebElements(this.WebElement.FindElements(By.TagName("tr")), ControlType.WebRow, this.controlAccess).Cast<SeleniumWebRow>().ToList().AsReadOnly();
        }
    }
}
