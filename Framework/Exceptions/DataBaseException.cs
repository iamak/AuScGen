// ***********************************************************************
// <copyright file="DataBaseException.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>DataBaseException class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.Common;
using System.Data.SqlClient;

namespace Framework
{
    /// <summary>
    /// This Exception Class handles the exceptions related to Database
    /// </summary>
    public class DataBaseException : BaseFrameworkException
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static log4net.ILog Logger =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()
                   .DeclaringType);
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.InvalidOperationException">Failed to create the DB Connection</exception>
        public DataBaseException(Exception innerException)
            : base()
        {
            //string exceptionString = innerException.GetType().ToString();
            //string methodName = innerException.TargetSite.Name;
            switch (innerException.GetType().Name)
            {
                case "InvalidOperationException ":
                    //string exceptionMessage = "";
                    throw new InvalidOperationException("Failed to create the DB Connection");
                case "SqlException":
                    string exceptionMessage = "Exception occured while returning DataReader Object";
                    Logger.Error(string.Concat(exceptionMessage, innerException.StackTrace));
                    break;

            }
            Logger.Debug("Inside DataBase Exception Class");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DataBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
            Logger.Error(message, innerException);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DataBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
