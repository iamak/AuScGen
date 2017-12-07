// ***********************************************************************
// <copyright file="Guimap.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Guimap class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using log4net;
//using log4net.Config;

namespace UIAccess
{
    /// <summary>
    /// Class Guimap.
    /// </summary>
    public class Guimap
    {
        //private static log4net.ILog Log =
        //    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The now time stamp
        /// </summary>
        private DateTime nowTimeStamp;

        /// <summary>
        /// The identifier
        /// </summary>
        private string id;
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The xpath
        /// </summary>
        private string xpath;
        /// <summary>
        /// The class name
        /// </summary>
        private string className;
        /// <summary>
        /// The logical name
        /// </summary>
        private string logicalName;
        /// <summary>
        /// The tagname
        /// </summary>
        private string tagName;
        /// <summary>
        /// The identification type
        /// </summary>
        private string identificationType;
        /// <summary>
        /// The content
        /// </summary>
        private string content;
        /// <summary>
        /// The atribute
        /// </summary>
        private string attribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Guimap"/> class.
        /// </summary>
        public Guimap()
        {
            //log4net.ThreadContext.Properties["myContext"] = "Logging from GuiMap Class";
            nowTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the last used time.
        /// </summary>
        /// <value>The last used time.</value>
        public DateTime LastUsedTime
        {
            get
            {
                return nowTimeStamp;
            }
            set
            {
                nowTimeStamp = value;
            }
        }
        /// <summary>
        /// Gets or sets the type of the identification.
        /// </summary>
        /// <value>
        /// The type of the identification.
        /// </value>
        public string IdentificationType
        {
            get { return identificationType; }
            set { identificationType = value; }
        }
        /// <summary>
        /// Gets or sets the tagname.
        /// </summary>
        /// <value>
        /// The tagname.
        /// </value>
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

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
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the xpath.
        /// </summary>
        /// <value>
        /// The xpath.
        /// </value>
        public string XPath
        {
            get { return xpath; }
            set { xpath = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// Gets or sets the atribute.
        /// </summary>
        /// <value>
        /// The atribute.
        /// </value>
        public string Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }
    }
}
