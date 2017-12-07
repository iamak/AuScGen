// ***********************************************************************
// <copyright file="TestController.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TestController class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuScGen.Web.Models;
using Newtonsoft.Json;
using NUnit.Core;
using NUnit.Core.Filters;
using NUnit.Framework;

namespace AuScGen.Web.Controllers
{
    /// <summary>
    ///		Class TestController
    /// </summary>
    public class TestController : Controller
    {
        // GET: Test
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View("Execution");
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="selectedTestCases">The selected test cases.</param>
        /// <returns></returns>
        public string RunTests(string filePath, List<string> selectedTestCases)
        {
            try
            {
                CoreExtensions.Host.InitializeService();
                TestPackage testPackage = new TestPackage(filePath);
                RemoteTestRunner remoteTestRunner = new RemoteTestRunner();
                remoteTestRunner.Load(testPackage);
                SimpleNameFilter filter = new SimpleNameFilter();

                foreach (string data in selectedTestCases)
                {
                    filter.Add(data);
                }

                TestResult testResult = remoteTestRunner.Run(new NullListener(), TestFilter.Empty, false, LoggingThreshold.Error);
                return JsonConvert.SerializeObject(testResult);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="testdata">The testdata.</param>
        /// <returns></returns>
        [HttpPost]
        public string GenerateReport(List<TestResultModel> testdata)
        {
            try
            {
                HtmlReportGenerator.CreateReport(testdata);
                return null;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}