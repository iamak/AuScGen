// ***********************************************************************
// <copyright file="FilterConfig.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>FilterConfig Class</summary>
// ***********************************************************************

using System.Web;
using System.Web.Mvc;

namespace AuScGen.Web
{
	/// <summary>
	///		Class FilterConfig
	/// </summary>
    public class FilterConfig
    {
		/// <summary>
		/// Registers the global filters.
		/// </summary>
		/// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
