// ***********************************************************************
// <copyright file="Startup.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Startup class</summary>
// ***********************************************************************
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuScGen.Web.Startup))]
namespace AuScGen.Web
{
	/// <summary>
	///		Class Startup
	/// </summary>
    public partial class Startup
    {
		/// <summary>
		/// Configurations the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
