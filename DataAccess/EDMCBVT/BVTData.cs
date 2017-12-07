// ***********************************************************************
// <copyright file="BVTData.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>BVTData class</summary>
// ***********************************************************************
using System.Collections.Generic;
using DataAccess.Entities;

namespace EDMC.DataAccess
{
	/// <summary>
	/// BVTData
	/// </summary>
	public class BVTData : BaseTestData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BVTData"/> class.
		/// </summary>
		/// <param name="dataFile">The data file.</param>
		public BVTData(string dataFile)
			: base(dataFile)
		{ }

		/// <summary>
		/// Gets or sets the get straight rif data.
		/// </summary>
		/// <value>
		/// The get straight rif data.
		/// </value>
		public IList<EMCBVT> GetStraightRifData
		{
			get
			{
				DataAccess.QueryString = SQL.Resource.GetStrightRIFData;
				return DataAccess.GetData<EMCBVT>();
			}
		}

		/// <summary>
		/// Gets the get invalid rif data.
		/// </summary>
		/// <value>
		/// The get invalid rif data.
		/// </value>
		public IList<EMCBVT> GetInvalidRifData
		{
			get
			{
				DataAccess.QueryString = SQL.Resource.GetInvalidRIFData;
				return DataAccess.GetData<EMCBVT>();
			}
		}

		/// <summary>
		/// Gets the get source code rif data.
		/// </summary>
		/// <value>
		/// The get source code rif data.
		/// </value>
		public IList<EMCBVT> GetSourceCodeRifData
		{
			get
			{
				DataAccess.QueryString = SQL.Resource.GetSourceCodeData;
				return DataAccess.GetData<EMCBVT>();
			}
		}
	}
}