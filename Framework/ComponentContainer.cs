// ***********************************************************************
// <copyright file="ComponentContainer.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ComponentContainer class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Framework
{
	/// <summary>
	/// ComponentContainer
	/// </summary>
	public class ComponentContainer
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		//protected static log4net.ILog Logger([CallerFilePath] string fileName = "") => log4net.LogManager.GetLogger(fileName);

		/// <summary>
		/// Gets or sets the plugins.
		/// </summary>
		/// <value>
		/// The plugins.
		/// </value>
		[ImportMany(typeof(IPlugin))]
		public IEnumerable<IPlugin> Plugins { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentContainer"/> class.
		/// </summary>
		public ComponentContainer()
		{
			log4net.ThreadContext.Properties["myContext"] = "Logging from Framework Class";
			//Logger.Debug("Inside Framework Constructor!");

			Logger.Debug("Inside Framework Constructor!");
		}

		/// <summary>
		/// Assembles the components.
		/// </summary>
		public void AssembleComponents()
		{
			SafeDirectoryCatalog sdc = new SafeDirectoryCatalog(Directory.GetCurrentDirectory());

			try
			{
				var catalog = new AggregateCatalog();
				//catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));                
				//catalog.Catalogs.Add(new DirectoryCatalog("."));
				catalog.Catalogs.Add(sdc);
				var container = new CompositionContainer(catalog);
				container.ComposeParts(this);
				foreach (ComposablePartDefinition part in sdc)
				{
					Logger.Info(part.ToString());
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Unable to assemble all logins error: {0}", ex);
				throw;
			}
		}

		/// <summary>
		/// Gets the get objects.
		/// </summary>
		/// <value>
		/// The get objects.
		/// </value>
		public IList<IPlugin> GetObjects
		{
			get
			{
				return Plugins.ToList<IPlugin>();
			}
		}
	}

	/// <summary>
	/// Class SafeDirectoryCatalog.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />
	public class SafeDirectoryCatalog : ComposablePartCatalog
	{
		/// <summary>
		/// The _catalog
		/// </summary>
		private readonly AggregateCatalog _catalog;
		/// <summary>
		/// The logger
		/// </summary>
		private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		//protected static log4net.ILog Logger([CallerFilePath] string fileName = "") => log4net.LogManager.GetLogger(fileName);

		/// <summary>
		/// Initializes a new instance of the <see cref="SafeDirectoryCatalog"/> class.
		/// </summary>
		public SafeDirectoryCatalog()
		{
			log4net.ThreadContext.Properties["myContext"] = "Logging from Framework Class";
			Logger.Debug("Inside Directory Catalog Constructor!");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SafeDirectoryCatalog"/> class.
		/// </summary>
		/// <param name="directory">The directory.</param>
		public SafeDirectoryCatalog(string directory)
		{
			var files = Directory.EnumerateFiles(directory, "*.dll", SearchOption.AllDirectories);

			this._catalog = new AggregateCatalog();

			foreach (var file in files)
			{
				try
				{
					var asmCat = new AssemblyCatalog(file);

					//Force MEF to load the plugin and figure out if there are any exports
					// good assemblies will not throw the RTLE exception and can be added to the catalog
					if (asmCat.Parts.ToList().Count > 0)
						this._catalog.Catalogs.Add(asmCat);
				}
				catch (ReflectionTypeLoadException ex)
				{
					Logger.Info(ex.LoaderExceptions.FirstOrDefault());
				}
				catch (BadImageFormatException ex)
				{
					if (!ex.Message.Contains("AutoItX3.dll")) // Suppress logger if it relates to AutoItX3.dll, this will not load as it is a COM dll
					{
						Logger.Info(ex);
					}
				}
			}
		}
		/// <summary>
		/// Gets the part definitions that are contained in the catalog.
		/// </summary>
		public override IQueryable<ComposablePartDefinition> Parts
		{
			get { return this._catalog.Parts; }
		}
	}
}