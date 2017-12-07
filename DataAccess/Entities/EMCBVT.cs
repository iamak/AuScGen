// ***********************************************************************
// <copyright file="EMCBVT.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>EMDCBVT class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    /// <summary>
    /// Class EMDCBVT.
    /// </summary>
    /// <seealso cref="DataAccess.Entities.BaseEntity" />
    public class EMCBVT : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EMCBVT"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="repCode">The rep code.</param>
        /// <param name="portal">The portal.</param>
        public EMCBVT(string firstName, string lastName, string email, string zipCode, string repCode, string portal)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ZipCode = zipCode;
            this.RepCode = repCode;
            this.Portal = portal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EMCBVT"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="portal">The portal.</param>
        public EMCBVT(string firstName, string lastName, string email, string zipCode, string cityName, string stateName, string portal)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ZipCode = zipCode;
            this.CityName = cityName;
            this.StateName = stateName;
            this.Portal = portal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EMCBVT"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="portal">The portal.</param>
        public EMCBVT(string firstName, string lastName, string email, string zipCode, string portal)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ZipCode = zipCode;
            this.Portal = portal;
        }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }
        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string StateName { get; set; }
        /// <summary>
        /// Gets or sets the rep code.
        /// </summary>
        /// <value>
        /// The rep code.
        /// </value>
        public string RepCode { get; set; }
    }
}
