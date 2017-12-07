// ***********************************************************************
// <copyright file="FileSystemutil.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>FileSystemutil class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.ERT.PDFSplit.Utils
{
	/// <summary>
	///		Class File System Util
	/// </summary>
    public class FileSystemUtil
    {
		/// <summary>
		/// Gets the source location.
		/// </summary>
		/// <value>
		/// The source location.
		/// </value>
        public static string SourceLocation 
        { 
            get
            {
                return ConfigurationManager.AppSettings["SourceFolder"];
            }
        }

		/// <summary>
		/// Gets the target location.
		/// </summary>
		/// <value>
		/// The target location.
		/// </value>
        public static string TargetLocation
        {
            get
            {
                string output = ConfigurationManager.AppSettings["DestinationFolder"];
                CreateDirectory(output);
                return output;
            }
        }

		/// <summary>
		/// Gets a value indicating whether [delete split after extraction].
		/// </summary>
		/// <value>
		/// <c>true</c> if [delete split after extraction]; otherwise, <c>false</c>.
		/// </value>
        public static bool DeleteSplitAfterExtraction 
        { 
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["DeleteSplitAfterExtraction"]);
            }
        }

		/// <summary>
		/// Gets a value indicating whether [merge plain text in table].
		/// </summary>
		/// <value>
		/// <c>true</c> if [merge plain text in table]; otherwise, <c>false</c>.
		/// </value>
        public static bool MergePlaintextInTable
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["MergePlainTextInTable"]);
            }
        }

		/// <summary>
		/// Gets the get all files.
		/// </summary>
		/// <value>
		/// The get all files.
		/// </value>
        public IList<Models.PdfFile> GetAllFiles
        {
            get
            {
                List<Models.PdfFile> pdffiles = new List<Models.PdfFile>();
                DirectoryInfo di = new DirectoryInfo(SourceLocation);
                List<FileInfo> files = di.GetFiles("*.pdf").ToList();
                foreach(FileInfo file in files)
                {
                    
                    CreateDirectory(string.Format(@"{0}\{1}",TargetLocation,file.Name.Split('.')[0]));

                    pdffiles.Add(new Models.PdfFile(string.Format(@"{0}\{1}", SourceLocation, file.Name), string.Format(@"{0}\{1}", TargetLocation, file.Name.Split('.')[0])));
                }

                return pdffiles;
            }
        }

		/// <summary>
		/// Creates the directory.
		/// </summary>
		/// <param name="path">The path.</param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectorySecurity securityRules = new DirectorySecurity();
                DirectoryInfo dir = Directory.CreateDirectory(path);
                dir.SetAccessControl(securityRules);
                //securityRules.AddAccessRule(new FileSystemAccessRule(@"Domain\YourAppAllowedGroup", FileSystemRights.FullControl, AccessControlType.Allow));
                
            }
        }
    }
}
