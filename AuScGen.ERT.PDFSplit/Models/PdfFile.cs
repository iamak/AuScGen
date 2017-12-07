// ***********************************************************************
// <copyright file="PdfFile.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>PdfFile class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.ERT.PDFSplit.Models
{
	/// <summary>
	///		Class PdfFile
	/// </summary>
    public class PdfFile
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="PdfFile"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
        public PdfFile(string source, string target)
        {
            SourcePath = source;
            TargetPath = target;
        }

		/// <summary>
		/// Gets or sets the source path.
		/// </summary>
		/// <value>
		/// The source path.
		/// </value>
        public string SourcePath { get; set; }

		/// <summary>
		/// Gets or sets the target path.
		/// </summary>
		/// <value>
		/// The target path.
		/// </value>
        public string TargetPath { get; set; }
    }
}
