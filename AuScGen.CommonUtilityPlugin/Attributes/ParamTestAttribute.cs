// ***********************************************************************
// <copyright file="ParamTestAttribute.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>ParamAttribute class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuScGen.CommonUtilityPlugin;
using NUnit.Framework;

namespace AuScGen
{
    /// <summary>
    /// Class ParamAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ParamTestAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamTestAttribute"/> class.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="parameterFileName">Name of the parameter file.</param>
        public ParamTestAttribute(string testName, string parameterFileName)
        {
            TestParameter.ParameterFileName = parameterFileName;
            TestParameter.TestName = testName;
        }
    }
}
