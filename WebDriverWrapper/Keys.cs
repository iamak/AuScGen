// ***********************************************************************
// <copyright file="Keys.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Keys class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper
{
    /// <summary>
    /// Keys
    /// </summary>
    public class Keys
    {
        /// <summary>
        /// Gets the add.
        /// </summary>
        /// <value>
        /// The add.
        /// </value>
        public static string Add 
        { 
            get
            {
                return OpenQA.Selenium.Keys.Add;
            }
        }

        /// <summary>
        /// Gets the enter.
        /// </summary>
        /// <value>
        /// The enter.
        /// </value>
        public static string Enter 
        { 
            get
            {
                return OpenQA.Selenium.Keys.Enter;
            }
        }

        /// <summary>
        /// Gets the key down.
        /// </summary>
        /// <value>
        /// The key down.
        /// </value>
        public static string KeyDown
        {
            get
            {
                return OpenQA.Selenium.Keys.ArrowDown;
            }
        }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public static string End
        {
            get
            {
                return OpenQA.Selenium.Keys.End;
            }
        }

        /// <summary>
        /// Gets the space.
        /// </summary>
        /// <value>
        /// The space.
        /// </value>
        public static string Space
        {
            get
            {
                return OpenQA.Selenium.Keys.Space;
            }
        }
    }
}
