// ***********************************************************************
// <copyright file="IRadioButton.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>IRadioButton Interface</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverWrapper.IControlHierarchy
{
	/// <summary>
	/// Interface IRadioButton
	/// </summary>
	/// <seealso cref="WebDriverWrapper.IControl" />
    public interface IRadioButton : IControl
    {
		/// <summary>
		/// Checks this instance.
		/// </summary>
        void Check();
    }
}