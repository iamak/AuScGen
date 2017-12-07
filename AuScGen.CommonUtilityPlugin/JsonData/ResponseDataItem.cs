// ***********************************************************************
// <copyright file="ResponseDataItem.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ResponseDataItem class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.CommonUtilityPlugin
{
	/// <summary>
	///		Class ResponseDataItem
	/// </summary>
	public class ResponseDataItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResponseDataItem"/> class.
		/// </summary>
		public ResponseDataItem()
		{

		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public Dictionary<string, string> Data
		{
			get
			{
				return new Dictionary<string, string>();
			}
		}
	}
}
