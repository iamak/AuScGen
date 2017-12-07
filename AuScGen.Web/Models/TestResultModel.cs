// ***********************************************************************
// <copyright file="TestResultModel.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TreeViewModel class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuScGen.Web.Models
{
    /// <summary>
    /// Test Result Model Class
    /// </summary>
    public class TestResultModel
    {

        // public string  { get; set; }
        /// <summary>
        /// Gets or sets the state of the result.
        /// </summary>
        /// <value>
        /// The state of the result.
        /// </value>
        public string ResultState { get; set; }
        /// <summary>
        /// Gets or sets the failure site.
        /// </summary>
        /// <value>
        /// The failure site.
        /// </value>
        public string FailureSite { get; set; }
        /// <summary>
        /// Gets or sets the executed.
        /// </summary>
        /// <value>
        /// The executed.
        /// </value>
        public string Executed { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        public List<TestDetails> Test { get; set; }
        /// <summary>
        /// Gets or sets the is success.
        /// </summary>
        /// <value>
        /// The is success.
        /// </value>
        public string IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the is failure.
        /// </summary>
        /// <value>
        /// The is failure.
        /// </value>
        public string IsFailure { get; set; }
        /// <summary>
        /// Gets or sets the is error.
        /// </summary>
        /// <value>
        /// The is error.
        /// </value>
        public string IsError { get; set; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; set; }
        /// <summary>
        /// Gets or sets the assert count.
        /// </summary>
        /// <value>
        /// The assert count.
        /// </value>
        public string AssertCount { get; set; }
        /// <summary>
        /// Gets or sets the has results.
        /// </summary>
        /// <value>
        /// The has results.
        /// </value>
        public string HasResults { get; set; }
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public string Results { get; set; }

    }

    /// <summary>
    /// Test Details Class
    /// </summary>
    public class TestDetails
    {
        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        public List<TestNameDetails> TestName { get; set; }
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        public string MethodName { get; set; }
        /// <summary>
        /// Gets or sets the is suite.
        /// </summary>
        /// <value>
        /// The is suite.
        /// </value>
        public string IsSuite { get; set; }
        /// <summary>
        /// Gets or sets the type of the test.
        /// </summary>
        /// <value>
        /// The type of the test.
        /// </value>
        public string TestType { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the state of the run.
        /// </summary>
        /// <value>
        /// The state of the run.
        /// </value>
        public string RunState { get; set; }
        /// <summary>
        /// Gets or sets the ignore reason.
        /// </summary>
        /// <value>
        /// The ignore reason.
        /// </value>
        public string IgnoreReason { get; set; }
        /// <summary>
        /// Gets or sets the test count.
        /// </summary>
        /// <value>
        /// The test count.
        /// </value>
        public string TestCount { get; set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public string Parent { get; set; }
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<string> Categories { get; set; }
        /// <summary>
        /// Gets or sets the tests.
        /// </summary>
        /// <value>
        /// The tests.
        /// </value>
        public string Tests { get; set; }
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public List<string> Properties { get; set; }
    }

    /// <summary>
    /// Test Name Details Class
    /// </summary>
    public class TestNameDetails
    {
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        /// <value>
        /// The test identifier.
        /// </value>
        public List<string> TestID { get; set; }
        /// <summary>
        /// Gets or sets the runner identifier.
        /// </summary>
        /// <value>
        /// The runner identifier.
        /// </value>
        public string RunnerID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the name of the unique.
        /// </summary>
        /// <value>
        /// The name of the unique.
        /// </value>
        public string UniqueName { get; set; }

    }
}