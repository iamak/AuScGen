// ***********************************************************************
// <copyright file="BaseFrameworkException.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>BaseFrameworkException class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using log4net;
using log4net.Config;

namespace Framework
{
    //[Serializable]// if this exception needs to be send to external application
    /// <summary>
    /// This Class is the Framework Exception Class which extends the Exception Class
    /// </summary>
    public class BaseFrameworkException : Exception
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
        public BaseFrameworkException()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public BaseFrameworkException(Exception innerException)
            : base()
        {
            Logger.Error(string.Format("Error occured at: {0}", innerException.TargetSite));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaseFrameworkException(string methodName, Exception innerException)
            : base(methodName, innerException)
        {
            Logger.Error(string.Format("Error occured in method : {0}", methodName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public BaseFrameworkException(string errorMessage)
            : base(errorMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public BaseFrameworkException(string methodName, string message, Exception innerException)
            : base(message, innerException)
        {
            Logger.Error(string.Format("Error occured in method: {0}", methodName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFrameworkException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BaseFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}