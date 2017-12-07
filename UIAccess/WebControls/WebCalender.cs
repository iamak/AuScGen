// ***********************************************************************
// <copyright file="WebCalender.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>WebCalender class</summary>
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
    ///     Class WebCalender
    /// </summary>
    public class WebCalender : WebControl
    {
        /// <summary>
        /// The browser
        /// </summary>
        private Browser browser;
        /// <summary>
        /// The locator
        /// </summary>
        private Locator locator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebCalender"/> class.
        /// </summary>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        public WebCalender(Browser browser, Locator locator)
            : base(browser, locator.LocatorType, locator.ControlLocator, ControlType.Calender)
        {
            this.browser = browser;
            this.locator = locator;
        }

        /// <summary>
        /// Gets the calender.
        /// </summary>
        /// <value>
        /// The calender.
        /// </value>
        private ICalender Calender
        {
            get
            {
                return (ICalender)ControlObject;
            }
        }

        /// <summary>
        /// The calender header
        /// </summary>
        private Locator calendarHeader;
        /// <summary>
        /// Gets or sets the calender header locator.
        /// </summary>
        /// <value>
        /// The calender header locator.
        /// </value>
        public Locator CalendarHeaderLocator
        {
            get
            {
                if (null == this.calendarHeader)
                {
                    return new Locator(string.Format("{0}/div", this.locator), LocatorType.Xpath);
                }
                else
                {
                    return this.calendarHeader;
                }

            }
            set
            {
                this.calendarHeader = value;
            }
        }

        /// <summary>
        /// The calender month year
        /// </summary>
        private Locator calendarMonthYear;
        /// <summary>
        /// Gets or sets the calender month year locator.
        /// </summary>
        /// <value>
        /// The calender month year locator.
        /// </value>
        public Locator CalendarMonthYearLocator
        {
            get
            {
                if (null == this.calendarMonthYear)
                {
                    return new Locator(string.Format("{0}/div", this.CalendarHeaderLocator), LocatorType.Xpath);
                }
                else
                {
                    return this.calendarMonthYear;
                }

            }
            set
            {
                this.calendarMonthYear = value;
            }
        }

        /// <summary>
        /// Gets the calender header.
        /// </summary>
        /// <value>
        /// The calender header.
        /// </value>
        public WebControl CalendarHeader
        {
            get
            {
                return new WebControl(this.browser, this.locator);
            }
        }

        /// <summary>
        /// Gets the get month and year.
        /// </summary>
        /// <value>
        /// The get month and year.
        /// </value>
        public WebControl GetMonthAndYear
        {
            get
            {
                return Utility.GetWebControlFromIContol(this.Calender.GetMonthAndYear(this.CalendarMonthYearLocator.ControlLocator, this.CalendarMonthYearLocator.LocatorType, this.CalendarHeaderLocator.ControlLocator, this.CalendarHeaderLocator.LocatorType), this.browser, this.locator, ControlType.Custom);
            }
        }

        /// <summary>
        /// Gets the get calender header.
        /// </summary>
        /// <value>
        /// The get calender header.
        /// </value>
        public WebControl GetCalendarHeader
        {
            get
            {
                return Utility.GetWebControlFromIContol(this.Calender.GetCalenderHeader(this.CalendarHeaderLocator.ControlLocator, this.CalendarHeaderLocator.LocatorType), this.browser, this.locator, ControlType.Custom);
            }
        }
    }
}