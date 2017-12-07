using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuScGen.TestExecutionUtil
{
    public class CreatePlayBack
    {
        public string RootReportFolderPath { get; set; }

        public CreatePlayBack()
        { }

        public CreatePlayBack(string reportRootPath)
        {
            RootReportFolderPath = reportRootPath;
        }

        private List<string> ReportPathList
        {
            get
            {
                return Directory.GetDirectories(RootReportFolderPath).ToList()
                                                    .Where(directory => !Path.GetFileName(directory).Equals("img")).ToList()
                                                    .Where(directory => !Path.GetFileName(directory).Equals("js")).ToList();
            }
        }

        public void CreateReports()
        {
            List<string> reportPaths = ReportPathList;
            //CopyScripts(RootReportFolderPath);
            foreach (string reportPath in reportPaths)
            {
                string testname = Path.GetFileName(reportPath).Split('.').LastOrDefault();
                StringBuilder sb = CreateHeader(testname);
                CreateBody(sb, reportPath, testname);
                using (FileStream fs = new FileStream(string.Format(@"{0}\Replay.html", reportPath), FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {

                        w.WriteLine(sb);
                    }
                }
            }
        }

        private StringBuilder CreateHeader(string testName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\">");
            sb.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            sb.AppendLine(string.Format("<title>{0} Replay</title>", testName));
            sb.AppendLine("<link href=\"../js/bootstrap.min.css\" rel=\"stylesheet\" />");
            sb.AppendLine("</head>");
            return sb;
        }

        private void CreateBody(StringBuilder sb, string path, string testName)
        {
            sb.AppendLine("<body background=\"../img/background.jpg\">");
            sb.AppendLine("<table width ='100%' style='background-color:#26466d;font-family:Verdana;font-size:16;font-weight:800;color:White'>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td><img src=\"../img/AuScGenLogo.png\" align='left' style='opacity: 1;background-color:#26466d'></td>");
            sb.AppendLine(string.Format("<td align='Center'>{0} Replay</td>", testName));
            sb.AppendLine("<td><img src=\"../img/Alliancelogo.png\" align='right' style='opacity: 1;background-color:#26466d'></td>");
            //sb.AppendLine("<td style=\"top: 10px\"></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<table><tr>");
            sb.AppendLine("<td>");
            sb.AppendLine("<div id=\"slider1_container\" style=\"display: none; position: relative; margin: 0 auto; top: 8px; left: 0px; width: 900px; height: 400px; overflow: hidden\">");

            CreateSlides(sb, path);

            sb.AppendLine("<link href=\"../js/SliderBulletCSS.css\" type=\"text/css\" rel=\"stylesheet\" />");
            sb.AppendLine("<link href=\"../js/NavArrows.css\" type=\"text/css\" rel=\"stylesheet\" />");
            sb.AppendLine("<div u=\"navigator\" class=\"jssorb21\" style=\"bottom: 26px; right: 6px;\">");
            sb.AppendLine("<div u=\"prototype\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<span u=\"arrowleft\" class=\"jssora21l\" style=\"top: 123px; left: 8px;\">");
            sb.AppendLine("</span>");
            sb.AppendLine("<span u=\"arrowright\" class=\"jssora21r\" style=\"top: 123px; right: 8px;\">");
            sb.AppendLine("</span>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr></table>");
            sb.AppendLine("<script src=\"../js/jquery-1.9.1.min.js\"></script>");
            sb.AppendLine("<script src=\"../js/bootstrap.min.js\"></script>");
            sb.AppendLine("<script src=\"docs.min.js\"></script>");
            sb.AppendLine("<script src=\"../js/ie10-viewport-bug-workaround.js\"></script>");
            sb.AppendLine("<script type=\"text/javascript\" src=\"../js/jssor.slider.mini.js\"></script>");
            sb.AppendLine("<script src=\"../js/SliderConfig.js\"></script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

        }

        private void CreateSlides(StringBuilder sb, string path)
        {
            List<string> imageFiles = GetImageFilesNames(path);
            sb.AppendLine("<div u=\"slides\" style=\"cursor: move; position: absolute; left: 0px; top: 0px; width: 900px; height: 400px; overflow: hidden\">");
            foreach (string images in imageFiles)
            {
                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine(string.Format("<img style=\"width: 100%; height: 100%\" src=\"{0}\" />", images));
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");
        }

        private List<string> GetImageFilesNames(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<FileInfo> fileInfos = di.GetFiles().OrderBy(p => p.CreationTime).ToList();
            List<string> files = new List<string>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                files.Add(fileInfo.Name);
            }

            List<string> returnFiles = new List<string>();
            string[] val = { ".png", ".jpg" };

            foreach (string file in files)
            {
                foreach (string x in val)
                {
                    if (Path.GetExtension(file).EndsWith(x))
                    {
                        returnFiles.Add(Path.GetFileName(file));
                    }
                }
            }

            return returnFiles;
        }

        private void CopyScripts(string path)
        {
            Directory.Move(Directory.GetCurrentDirectory() + @"\Reports\img\", path);
            Directory.Move(Directory.GetCurrentDirectory() + @"\Reports\js\", path);
        }
    }
}
