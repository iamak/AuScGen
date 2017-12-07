// ***********************************************************************
// <copyright file="HomeController.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>HomeController class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AuScGen.Web.Models;
using Newtonsoft.Json;

namespace AuScGen.Web.Controllers
{

    /// <summary>
    /// Home Controller class.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {

        /// <summary>
        /// Requirements this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Requirement()
        {
            

            return this.View();
        }

        /// <summary>
        /// Executions this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Execution()
        {
            return this.View();
        }

        /// <summary>
        /// Reports this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Report()
        {
            return this.View();
        }

        /// <summary>
        /// Generates the page class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ActionResult GeneratePageClass(string fileName)
        {
            string savePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["pageClassSavingPath"]);
            PageClassGenerator.Program.GeneratePageClass(fileName, savePath);
            return this.View("Requirement");
        }

        /// <summary>
        /// Gets the methods from the assembly.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        [HttpPost]
        public string GetMethodsFromTheAssembly(string filePath)
        {
            TreeViewModel obj = null;
            List<MethodModel> totalMethods = null;
            MethodModel methodObj = null;
            List<TreeViewModel> methodInfo = new List<TreeViewModel>();
            try
            {
                Assembly assembly = Assembly.LoadFile(filePath);
                var data = assembly.GetType().CustomAttributes.ToList();
                var types = assembly.GetTypes().Where(x => x.CustomAttributes.Any(y => y.AttributeType.Name == "TestClassAttribute" || y.AttributeType.Name == "TestFixtureAttribute")).ToList();
                foreach (Type type in types)
                {
                    var customAttrs = type.CustomAttributes.ToList();

                    if (type.IsClass && type.CustomAttributes.ToList().Any(x => x.AttributeType.Name == "TestClassAttribute" || x.AttributeType.Name == "TestFixtureAttribute"))
                    {
                        var method = type.GetMethods().Where(x => x.CustomAttributes.Any(y => y.AttributeType.Name == "TestMethodAttribute" || y.AttributeType.Name == "TestAttribute")).ToList();
                        totalMethods = new List<MethodModel>();
                        foreach (var x in method)
                        {
                            methodObj = new MethodModel();
                            methodObj.MethodName = x.Name;
                            methodObj.IsChecked = false;
                            totalMethods.Add(methodObj);
                        }
                    }
                    obj = new TreeViewModel();
                    obj.ClassName = type.Name;
                    obj.ClassMethods = totalMethods;
                    methodInfo.Add(obj);
                }

                HtmlReportGenerator.MethodInfoCount(totalMethods);
                return JsonConvert.SerializeObject(methodInfo);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        public string DisplayReports()
        {
            string path = Server.MapPath("~/Scripts/");
            string[] filePaths = Directory.GetFiles(path,"TestReport.html", SearchOption.AllDirectories);
            List<string> urlPaths = new List<string>();

            foreach (string file in filePaths)
            {
                string url = "file:///" + file;
                urlPaths.Add(url);
            }
            return JsonConvert.SerializeObject(urlPaths);
        }
    }
}