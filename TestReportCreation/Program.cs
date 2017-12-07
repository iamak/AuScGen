using AuScGen.TestExecutionUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReportCreation
{
	/// <summary>
	/// Class Program
	/// </summary>
    class Program
    {
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            CreatePlayBack playback = new CreatePlayBack(Directory.GetCurrentDirectory() + @"\ReportLog\");
            playback.CreateReports();
        }
    }
}