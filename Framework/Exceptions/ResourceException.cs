// ***********************************************************************
// <copyright file="ResourceException.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ResourceException class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace Framework
{
	/// <summary>
	/// This Exception Classes Handles the File related to file Resources
	/// /// </summary>
	public class ResourceException : BaseFrameworkException
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
		/// <param name="methodName">Name of the method.</param>
		public ResourceException(string methodName)
			: base()
		{
			if (methodName.Equals("XlsxToTableData"))
			{
				string errorMessage = string.Concat("You Have Either Specified  wrong Sheet name", "or the specified sheet name does not have data for the specified columns");
				Logger.Error(errorMessage);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceException" /> class.
		/// </summary>
		/// <param name="innerException">The inner exception.</param>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		public ResourceException(Exception innerException)
			: base()
		{
			if (innerException.GetType().Name.Equals("FileNotFoundException"))
			{
				string errorMessage = "The specified Excel sheet not found. Please recheck the filename or File Path";
				Logger.Error(errorMessage);
				throw new FileNotFoundException(errorMessage, innerException.StackTrace);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataBaseException" /> class.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ResourceException(string methodName, string errorMessage, Exception innerException)
			: base(methodName, errorMessage, innerException)
		{
			//innerException.
			if (methodName.Equals("loadTestData")
				&& innerException.GetType().Name.Equals("NullReferenceException"))
			{
				Logger.Error(errorMessage);
				//throw new NullReferenceException(errorMessage);
			}
			else if (methodName.Equals("LoadGuiMap") &&
			   innerException.GetType().Name.Equals("FileNotFoundException"))
			{
				Logger.Error(errorMessage);
				throw new FileNotFoundException(errorMessage, innerException.StackTrace);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceException"/> class.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="innerException">The inner exception.</param>
		public ResourceException(string methodName, Exception innerException)
			: base(methodName, innerException)
		{
			//innerException.
			if (methodName.Equals("loadTestData")
				&& innerException.GetType().Name.Equals("NullReferenceException"))
			{
				string errorMessage = "No Such Sheet exists. Check your sheet Name! ";
				Logger.Error(errorMessage);
				//throw new NullReferenceException(errorMessage);
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DataBaseException" /> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		protected ResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}
	}
}