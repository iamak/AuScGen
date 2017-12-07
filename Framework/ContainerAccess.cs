// ***********************************************************************
// <copyright file="ContainerAccess.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ContainerAccess class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    ///     Class ContainerAccess
    /// </summary>
    public class ContainerAccess
    {
        /// <summary>
        /// The comp container
        /// </summary>
        private ComponentContainer compContainer;

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <value>
        /// The plugins.
        /// </value>
        private IList<IPlugin> Plugins
        {
            get
            {
                if (compContainer == null)
                {
                    compContainer = new ComponentContainer();
                }

                compContainer.AssembleComponents();
                return compContainer.GetObjects;
            }
        }

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public IPlugin GetPlugin(string description)
        {
            foreach (IPlugin plugin in Plugins)
            {
                if (string.Equals(plugin.Description, description))
                {
                    return plugin;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="description">The description.</param>
        /// <returns>plugin</returns>
        public T GetPlugin<T>(string description) where T : IPlugin
        {
            foreach (IPlugin plugin in Plugins)
            {
                if (plugin.GetType().Equals(typeof(T)))
                {
                    if (string.Equals(plugin.Description, description))
                    {
                        return (T)plugin;
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <returns>plugin</returns>
        public T GetPlugin<T>() where T : IPlugin
        {
            foreach (IPlugin plugin in Plugins)
            {
                if (plugin.GetType().Equals(typeof(T)))
                {
                    return (T)plugin;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Sets the plugin desc.
        /// </summary>
        /// <param name="description">The desc.</param>
        /// <param name="newDescription">The new desc.</param>
        public void SetPluginDescription(string description, string newDescription)
        {
            GetPlugin(description).Description = newDescription;
        }
    }
}
