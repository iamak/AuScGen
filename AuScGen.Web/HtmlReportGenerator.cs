// ***********************************************************************
// <copyright file="HtmlReportGenerator.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>TreeViewModel class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using AuScGen.TestExecutionUtil;
using AuScGen.Web.Models;
using Newtonsoft.Json;

namespace AuScGen.Web
{
    /// <summary>
    /// Html report generator class
    /// </summary>
    public class HtmlReportGenerator
    {

        /// <summary>
        /// The HTML path
        /// </summary>
        private static string path = HttpContext.Current.Server.MapPath("~/Scripts/Reports/");
        /// <summary>
        /// The report path
        /// </summary>
        private static string reportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["htmlReportPath"]);
        /// <summary>
        /// The report saving path
        /// </summary>
        private static string reportSavingPath = ConfigurationManager.AppSettings["htmlReportSavingPath"];
        /// <summary>
        /// The errors
        /// </summary>
        private static List<ErrorItem> errors;
        /// <summary>
        /// The sucess
        /// </summary>
        private static List<SuccessItem> sucess;

        /// <summary>
        /// The test methods count
        /// </summary>
        private static int testMethodsCount;

        /// <summary>
        /// Methods the information count.
        /// </summary>
        /// <param name="testMethods">The test methods.</param>
        public static void MethodInfoCount(List<MethodModel> testMethods)
        {
            testMethodsCount = testMethods.Count();
        }

        /// <summary>
        /// Displays the reports.
        /// </summary>
        /// <returns></returns>
        public string DisplayReports()
        {
            string[] filePaths = Directory.GetFiles(path);
            //List<string> urlPaths = new List<string>();

            //foreach (string file in filePaths)
            //{
            //    string url = "file:///" + file;
            //    urlPaths.Add(url);
            //}
            return JsonConvert.SerializeObject(filePaths);
        }

        /// <summary>
        /// Creates the report.
        /// </summary>
        /// <param name="testRes">The test resource.</param>
        public static void CreateReport(List<TestResultModel> testRes)
        {
            double totalTestcases;
            double totalPassed;
            double totalFailed;
            double totalError;
            errors = new List<ErrorItem>();
            sucess = new List<SuccessItem>();
            ErrorItem objErrors;
            SuccessItem objSucess;
            //string path = HttpContext.Current.Server.MapPath(@"~/Scripts/Reports/");
            string htmlPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["htmlReportPath"]);
            var testSuccessCount = testRes.Where(x => x.Executed == "True" && x.IsSuccess == "True").Count();
            var testFailureCount = testRes.Where(x => x.Executed == "True" && x.IsFailure == "True" && x.IsError == "False").Count();
            var testErrorCount = testRes.Where(x => x.Executed == "True" && x.IsError == "True").Count();
            var testSuccess = testRes.FindAll(x => x.IsSuccess == "True");
            var testFailure = testRes.FindAll(x => x.IsFailure == "True");
            CreatePlayBack playback = new CreatePlayBack(path);
            playback.CreateReports();
            totalTestcases = Convert.ToDouble(testMethodsCount);
            totalFailed = Convert.ToDouble(testFailureCount);
            totalError = Convert.ToDouble(testErrorCount);
            totalPassed = totalTestcases - totalFailed - totalError;
            totalFailed = totalFailed + totalError;

            foreach (TestResultModel data in testSuccess)
            {
                string description = GenerateDescription(data.Name);
                objSucess = new SuccessItem();
                objSucess.Name = data.Name;
                objSucess.Message = data.Message;
                objSucess.Description = description;
                objSucess.Result = "Success";
                objSucess.Time = Math.Round(Decimal.Parse(data.Time), 4).ToString();
                objSucess.StackTrace = data.StackTrace;
                sucess.Add(objSucess);

            }
            foreach (TestResultModel data in testFailure)
            {
                objErrors = new ErrorItem();
                objErrors.Name = data.Name;
                objErrors.Message = data.Message;
                objErrors.StackTrace = data.StackTrace;
                objErrors.Time = Math.Round(Decimal.Parse(data.Time), 4).ToString();
                errors.Add(objErrors);

            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Html>");
            CreateHtmlHeader(sb);
            sb.AppendLine("<body background=\"./img/background.jpg\" > ");
            CreateHeadline(sb);
            CreateSummary(sb, "AuScGen Framework", totalTestcases, totalPassed, totalFailed);
            CreateBody(sb, sucess);
            CreateChartScript(sb, totalFailed, totalPassed);
            EndHtml(sb);
            FileStream f = new FileStream(htmlPath, FileMode.Create, FileAccess.Write);
            using (StreamWriter s = new StreamWriter(f))
                s.WriteLine(sb.ToString());

            if (File.Exists(htmlPath))
            {
                var x = HttpContext.Current.Server.MapPath(@"~/Scripts/Reports") ;
                copyDirectory(HttpContext.Current.Server.MapPath(@"~/Scripts/Reports"), reportSavingPath + @"\Reports_" + Guid.NewGuid());
                if (System.IO.File.Exists(reportPath))
                {
                    System.IO.File.Delete(reportPath);
                }
            }
        }

        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="Src">The source.</param>
        /// <param name="Dst">The DST.</param>
        public static void copyDirectory(string Src, string Dst)
        {
            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                // Files in directory
                else
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
            }
        }

        private static string GenerateDescription(string text)
        {
            var result = new StringBuilder(text.Length);
            for (var i = 0; i < text.Length - 1; i++)
            {
                result.Append(text[i]);
                if (text[i] != ' ' && text[i] != '-' && (char.IsUpper(text[i + 1]) || !char.IsDigit(text[i]) && char.IsDigit(text[i + 1])))
                    result.Append(' ');
            }
            result.Append(text[text.Length - 1]);
            return result.ToString();
        }

        /// <summary>
        /// Creates the HTML header.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void CreateHtmlHeader(StringBuilder sb)
        {
            sb.AppendLine("<Head>");
            sb.AppendLine("<script src=\"./js/Chart.js\"></script>");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"./js/bootstrap.min.css\">");
            sb.AppendLine("</Head>");
            sb.AppendLine("<style>");
            sb.AppendLine("body {");
            sb.AppendLine("background-color: #cccccc;");
            sb.AppendLine("}");
            sb.AppendLine("</style>");
        }

        /// <summary>
        /// Creates the headline.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void CreateHeadline(StringBuilder sb)
        {
            sb.AppendLine("<table width ='100%' style='background-color:#26466d;font-family:Verdana;font-size:16;font-weight:800;color:White'>");
            sb.AppendLine("<tr><td><img src=\"./img/AuScGenLogo.png\" style='opacity: 1;background-color:#26466d'></td>");
            sb.AppendLine("<td align='center' style='color:Orange;font-size:25;font-weight:Bold'>Report Generation</td>");
            sb.AppendLine("<td><img src=\"./img/Alliancelogo.png\" align='right' style='opacity: 1;background-color:#26466d'></td>");
            sb.AppendLine("</tr></table>");
            sb.AppendLine("<table width ='100%'><tr>&nbsp;</tr></table>");
        }

        /// <summary>
        /// Creates the summary.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="totalTestcases">The total testcases.</param>
        /// <param name="passed">The passed.</param>
        /// <param name="failed">The failed.</param>
        private static void CreateSummary(StringBuilder sb, string projectName, double totalTestcases, double passed, double failed)
        {
            sb.AppendLine("<table border='0' style='width:100%;height:;background-color:#26466d;font-family:Verdana;color:White;border-color:white'><tr>");
            sb.AppendLine("<td style='width:50%; height=150%'><table border='1' style='width:100%;font-family:Verdana;font-size:14;color:white;border-color:white;font-weight:Bold'>");
            sb.AppendLine(string.Format("<tr><td style='width:50%'>Project</td><td style='width:50%;text-align:center;font-size:13;word-wrap:break-word;color:Orange;font-weight:bold'>{0}</td></tr>", projectName));
            sb.AppendLine(string.Format("<tr><td style='width:50%'>Date of Execution</td><td align='center' style='width:50%;text-align:center;font-size:13;word-wrap:break-word;color:Orange;font-weight:bold'>{0}</td></tr>", System.DateTime.Now));
            sb.AppendLine(string.Format("<tr><td style='width:50%'>Total Testcase Executed</td><td style='width:50%;text-align:center'>{0}</td></tr>", totalTestcases));
            sb.AppendLine(string.Format("<tr><td style='width:50%;'>Total Testcase Passed</td><td style='width:50%;text-align:center'>{0}</td>", passed));
            sb.AppendLine(string.Format("</tr><tr><td style='width:50%'>Total Testcase Failed</td><td style='width:50%;text-align:center'>{0}</td></tr></table></td>", failed));
            sb.AppendLine("<td align='left' style='width:30%;background-color:White'>");
            sb.AppendLine("<canvas align ='left' id=\"chart-area\" height=\"200\" width=\"400\"></canvas>");
            sb.AppendLine("<span><img style='width:45%;padding-left:30%' src='./img/Legends.png' /></span>");
            sb.AppendLine("</td>");
            sb.AppendLine(string.Format("<td align='Center' style='font-size:30;color:green;font-weight:bold;background-color:White'>Pass: {0}%</td>", Math.Round((passed / totalTestcases) * 100, 2)));
            sb.AppendLine("</tr></table>");
            sb.AppendLine("<table width ='100%'><tr>&nbsp;</tr></table>");
            sb.AppendLine("<table>");
            sb.AppendLine("<div class=\"container\">");
            sb.AppendLine("<div class=\"panel-group\" id=\"accordion\">");
            sb.AppendLine("<table border ='1' style='width:100%;table-layout:fixed;overflow-x:hidden'>");
            sb.AppendLine("<tr style='background-color:#26466d;font-family:Verdana;font-size:14;font-weight:600;color:white'>");
            sb.AppendLine("<td width='35%'>Test Case Name</td><td width='15%'>Description</td><td width='10%'>Status</td><td width='20%'>Comments</td><td width='10%'>Elapsed Time(s)</td><td width='10%'>Replay</td>");
            sb.AppendLine("</tr>");
        }

        /// <summary>
        /// Creates the body.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="sucess">The sucess.</param>
        private static void CreateBody(StringBuilder sb, List<SuccessItem> sucess)
        {
            if (sucess.Count > 0)
            {
                for (int I = 0; I < sucess.Count; I++)
                {
                    sb.AppendLine("<tr style='background-color:#bcd2ee;font-family:Verdana;color:Black'>");
                    sb.AppendLine("<td width='35%' style='font-size:13;word-wrap:break-word'>");
                    sb.AppendLine(string.Format("<a data-toggle=\"collapse\" style='font-weight:Bold' data-parent=\"#accordion\" href=\"#collapse{0}\">{1}</a>", I + 1, sucess[I].Name.Replace("(System.String[])", string.Empty).Trim()));
                    sb.AppendLine(string.Format("<div id=\"collapse{0}\" class=\"panel-collapse collapse\">", I + 1));
                    sb.AppendLine("<div class=\"panel-body\">");
                    sb.AppendLine("<table border='1'>");
                    //sb.AppendLine("<tr style='font-size:12'><td style='color:blue;font-weight:Bold'>Step:1</td><td>Lorem ipsum dolor sit amet, consectetur adipisicing elit,</td></tr>");
                    //sb.AppendLine("<tr style='font-size:12'><td style='color:blue;font-weight:Bold'>Step:2</td><td>quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</td></tr>");
                    //sb.AppendLine("<tr style='font-size:12'><td style='color:blue;font-weight:Bold'>Step:3</td><td>sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,</td></tr>");
                    CreateStepMessages(sb, sucess[I]);
                    sb.AppendLine("</table>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</td>");

                    if (sucess[I].Result == "Success")
                    {

                        if (sucess[I].Description != null)
                        {
                            string[] arr = sucess[I].Description.Split(':', '_', ';');
                            sucess[I].Description = string.Empty;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (IsOdd(i))
                                {
                                    if (arr[i] != string.Empty)
                                    {
                                        sucess[I].Description += arr[i].Replace(arr[i], "<font color='Blue'>" + arr[i] + ";" + "</font>");
                                    }
                                }
                                if (IsEven(i))
                                {
                                    sucess[I].Description += arr[i] + " - ";
                                }
                            }
                            sucess[I].Description = sucess[I].Description.Replace(";", "<br style=\"mso-data-placement:same-cell;\">");
                            sb.AppendLine("<td width='15%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Description + "</td>");
                        }
                        else
                        {
                            sb.AppendLine("<td width='15%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Description + "</td>");
                        }
                        sb.AppendLine("<td width='10%' style='font-family:Verdana;font-size:11;font-weight:Bold;color:DarkGreen'>" + "PASS" + "</td>");
                        sb.AppendLine("<td width='20%' style='font-size:13;word-wrap:break-word'>" + "Verification Successful" + "</td>");
                        sb.AppendLine("<td width='10%' style='font-size:13'>" + sucess[I].Time + "</td>");
                        //sb.AppendLine("<td width='10%' style='font-size:13'>"+"</td>");
                        //sb.AppendLine(string.Format("<td align='Center' width='10%'><a href=\".\\{0}\\Replay.html\"><img src=\"./img/replay.png\" target=\"_blank\"/></a></td>", sucess[I].Name));
                        sb.AppendLine(string.Format("<td align='Center' width='10%'><a href=# onClick=\"window.open('./{0}/Replay.html', '_blank')\"><img src=\"./img/replay.png\" target=\"_blank\"/></a></td>", sucess[I].Name));
                        sb.AppendLine("</tr>");
                    }
                    else
                    {
                        //sb.AppendLine("<tr style='background-color:#bcd2ee;font-family:Verdana;color:Black'>");
                        //sb.AppendLine("<td width='40%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Name.Replace("(System.String[])", "").Trim() + "</td>");
                        if (sucess[I].Description != null)
                        {
                            string[] arr = sucess[I].Description.Split(':', ';');
                            sucess[I].Description = string.Empty;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (IsOdd(i))
                                {
                                    if (arr[i] != string.Empty)
                                    {
                                        sucess[I].Description += arr[i].Replace(arr[i], "<font color='Blue'>" + arr[i] + ";" + "</font>");
                                    }
                                }
                                if (IsEven(i))
                                {
                                    sucess[I].Description += arr[i] + " - ";
                                }
                            }
                            sucess[I].Description = sucess[I].Description.Replace(";", "<br style=\"mso-data-placement:same-cell;\">");
                            sb.AppendLine("<td width='20%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Description + "</td>");
                        }
                        else
                        {
                            sb.AppendLine("<td width='20%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Description + "</td>");
                        }
                        sb.AppendLine("<td width='10%' style='font-family:Verdana;font-size:11;font-weight:Bold;color:Red'>" + "FAIL" + "</td>");
                        sb.AppendLine("<td width='20%' style='font-size:13;word-wrap:break-word'>" + sucess[I].Message + "</td>");
                        sb.AppendLine("<td width='10%' style='font-size:13'>" + sucess[I].Time + "</td>");
                        sb.AppendLine(string.Format("<td align='Center' width='10%'><a href=# onClick=\"window.open('./{0}/Replay.html', '_blank')\"><img src=\"./img/replay.png\" target=\"_blank\"/></a></td>", sucess[I].Name));
                        sb.AppendLine("</tr>");
                    }
                }

            }
        }

        /// <summary>
        /// Creates the step messages.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="item">The item.</param>
        private static void CreateStepMessages(StringBuilder sb, SuccessItem item)
        {
            //TestExecute test = new TestExecute();
            ////ReadOnlyCollection<StepMessage> messages = test.GetMessages(string.Format(@"{0}\Reports\{1}",Directory.GetCurrentDirectory(),item.Name));

            //XmlDocument XmlDoc = new XmlDocument();
            //XmlDoc.Load(string.Format(@"{0}\Reports\{1}\message.xml", Directory.GetCurrentDirectory(), item.Name));
            //XmlNodeList messages = XmlDoc.SelectNodes("/StepMessage/Message");
            //XmlNodeList stepnames = XmlDoc.SelectNodes("/StepMessage/StepName");
            if (File.Exists(string.Format(@"{0}\Reports\{1}\message.csv", Directory.GetCurrentDirectory(), item.Name)))
            {
                string[] lines = File.ReadAllLines(string.Format(@"{0}\Reports\{1}\message.csv", Directory.GetCurrentDirectory(), item.Name));
                for (int i = 1; i <= lines.Length; i++)
                {
                    //List<string> line.Split(',')
                    sb.AppendLine(string.Format("<tr style='font-size:12'><td style='color:blue;font-weight:Bold'>Step:{0}</td><td>{1}</td></tr>", i, lines[i - 1]));
                }
            }

            //foreach (StepMessage message in messages)
            //{
            //    sb.AppendLine(string.Format("<tr style='font-size:12'><td style='color:blue;font-weight:Bold'>{0}</td><td>{1}</td></tr>", message.StepName, message.Message));
            //}
        }

        /// <summary>
        /// Creates the chart script.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="failResultsCount">The fail results count.</param>
        /// <param name="passResultsCount">The pass results count.</param>
        private static void CreateChartScript(StringBuilder sb, double failResultsCount, double passResultsCount)
        {
            sb.AppendLine("<script>");
            sb.AppendLine("var pieData = [");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("value: {0},", failResultsCount.ToString()));
            sb.AppendLine("color:\"#FF0000\",");
            sb.AppendLine("highlight: \"#FF5A5E\",");
            sb.AppendLine("label: \"FAIL\"");
            sb.AppendLine("},");
            sb.AppendLine("{");
            sb.AppendLine(string.Format("value: {0},", passResultsCount));
            sb.AppendLine("color: \"#2CF808\",");
            sb.AppendLine("highlight: \"#34E555\",");
            sb.AppendLine("label: \"PASS\"");
            sb.AppendLine("},");
            sb.AppendLine("];");
            sb.AppendLine("window.onload = function(){");
            sb.AppendLine("var ctx = document.getElementById(\"chart-area\").getContext(\"2d\");");
            sb.AppendLine("window.myPie = new Chart(ctx).Pie(pieData);");
            sb.AppendLine("};");
            sb.AppendLine("</script>");
        }

        /// <summary>
        /// Ends the HTML.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void EndHtml(StringBuilder sb)
        {
            sb.AppendLine("<table width ='100%'><tr><td align='Center'>&nbsp;</td></tr></table>");
            sb.AppendLine("</table>");
            sb.AppendLine(" </div>");
            sb.AppendLine(" </div>");
            sb.AppendLine("<script src=\"./js/jquery-1.9.1.min.js\"></script>");
            sb.AppendLine("<script src=\"./js/bootstrap.min.js\"></script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</Html>");
        }

        /// <summary>
        /// Determines whether the specified value is odd.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Determines whether the specified value is even.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Loads the test results.
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        private static void LoadTestResults(XmlElement rootNode)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node is XmlElement && node.LocalName == "test-suite")
                    LoadTestSuite((XmlElement)node);
            }
        }

        /// <summary>
        /// Loads the test suite.
        /// </summary>
        /// <param name="suiteNode">The suite node.</param>
        private static void LoadTestSuite(XmlElement suiteNode)
        {
            foreach (XmlNode node in suiteNode.ChildNodes)
            {
                if (node is XmlElement && node.LocalName == "results")
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode is XmlElement)
                        {
                            if (subNode.LocalName == "test-suite")
                            {
                                LoadTestSuite((XmlElement)subNode);
                            }
                            else if (subNode.LocalName == "test-case")
                            {
                                LoadTestCase((XmlElement)subNode);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Loads the test case.
        /// </summary>
        /// <param name="caseNode">The case node.</param>
        private static void LoadTestCase(XmlElement caseNode)
        {

            LoadTestSuccess((XmlElement)caseNode);

            //if (caseNode.ChildNodes.Count>0)
            //{
            //    foreach (XmlNode node in caseNode.ChildNodes)
            //    {
            //        if (node is XmlElement && node.LocalName == "failure")
            //        {
            //            LoadTestFailure((XmlElement)node);
            //        }
            //    }
            //}
            //else
            //{
            //    LoadTestSuccess((XmlElement)caseNode);
            //}
        }

        /// <summary>
        /// Loads the test success.
        /// </summary>
        /// <param name="successNode">The success node.</param>
        private static void LoadTestSuccess(XmlElement successNode)
        {
            SuccessItem Pass = new SuccessItem();
            if (successNode.Attributes["result"].Value == "Failure" || successNode.Attributes["result"].Value == "Error")
            {
                foreach (XmlNode node in successNode.ChildNodes)
                {
                    if (node is XmlElement && node.LocalName == "failure")
                    {
                        LoadTestFailure((XmlElement)node);
                    }
                }
            }
            else
            {
                Pass.Name = successNode.Attributes["name"].Value;
                if (successNode.Attributes["description"] != null)
                {
                    if (successNode.Attributes["description"].Value != string.Empty || successNode.Attributes["description"].Value != null)
                    {
                        Pass.Description = successNode.Attributes["description"].Value;
                    }
                }
                Pass.Result = successNode.Attributes["result"].Value;
                Pass.Time = successNode.Attributes["time"].Value;
                sucess.Add(Pass);
            }

        }

        /// <summary>
        /// Loads the test failure.
        /// </summary>
        /// <param name="failureNode">The failure node.</param>
        private static void LoadTestFailure(XmlElement failureNode)
        {
            SuccessItem error = new SuccessItem();

            error.Name = failureNode.ParentNode.Attributes["name"].Value;
            if (failureNode.ParentNode.Attributes["description"] != null)
            {
                if (failureNode.ParentNode.Attributes["description"].Value != string.Empty || failureNode.ParentNode.Attributes["description"].Value != null)
                {
                    error.Description = failureNode.ParentNode.Attributes["description"].Value;
                }
            }

            error.Time = failureNode.ParentNode.Attributes["time"].Value;
            error.Result = failureNode.ParentNode.Attributes["result"].Value;
            foreach (XmlNode node in failureNode.ChildNodes)
            {
                if (node.LocalName == "message")
                {
                    error.Message = node.FirstChild.Value;
                }
                else if (node.LocalName == "stack-trace")
                {
                    if (node.FirstChild != null)
                    {
                        error.StackTrace = node.FirstChild.Value;
                    }

                }
            }
            sucess.Add(error);
        }
    }
}
