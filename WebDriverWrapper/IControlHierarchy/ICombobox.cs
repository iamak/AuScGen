// ***********************************************************************
// <copyright file="ICombobox.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ICombobox Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
    /// <summary>
    /// ICombobox Interface.
    /// </summary>
    /// <seealso cref="WebDriverWrapper.IControl" />
    public interface ICombobox : IControl
    {
        /// <summary>
        /// Gets all options.
        /// </summary>
        /// <returns>Get All Options.</returns>
        ReadOnlyCollection<string> GetAllOptions();
        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="option">The option.</param>
        void SelectByText(string option);
        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        void SelectByIndex(int index);
        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        void SelectByValue(string value);
        /// <summary>
        /// Deselects all.
        /// </summary>
        void DeselectAll();
        /// <summary>
        /// Deselects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        void DeselectByIndex(int index);
        /// <summary>
        /// Deselects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        void DeselectByText(string text);
        /// <summary>
        /// Deselects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        void DeselectByValue(string value);
        /// <summary>
        /// Selects the index of the by.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="maxTimeout">The maximum timeout.</param>
        void SelectByIndex(int index, int maxTimeout);
        /// <summary>
        /// Selects the by text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxTimeout">The maximum timeout.</param>
        void SelectByText(string text, int maxTimeout);
    }
}
