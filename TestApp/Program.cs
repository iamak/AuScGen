using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuScGen.FunctionalTest;
using UIAccess;
using Framework;
using System.Drawing;

namespace TestApp
{
	/// <summary>
	/// Class Program
	/// </summary>
    class Program : TestBase
    {
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            ExtractPages(Directory.GetCurrentDirectory() + @"\TestPDF\Test3.pdf", Directory.GetCurrentDirectory() + @"\TestPDF\Page58.pdf", 58, 58);

            foreach (string value in PDFRead())
            {
                Console.WriteLine(value);
            }

            foreach (Image image in GetImagesFromPdf())
            {
                image.Save("Image_" + Guid.NewGuid() + ".bmp");
            }
                        
            Console.Read();
        }

		/// <summary>
		/// PDFs the read.
		/// </summary>
		/// <returns></returns>
        public static List<string> PDFRead()
        {
            ContainerAccess container = new ContainerAccess();
            AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
            string pdfContent = pdfReader.ExtractTextFromPdf(Directory.GetCurrentDirectory() + @"\TestPDF\Test3.pdf");
            List<string> values = pdfContent.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();

            return values;
        }

		/// <summary>
		/// Gets the images from PDF.
		/// </summary>
		/// <returns></returns>
        public static IList<Image> GetImagesFromPdf()
        {
            ContainerAccess container = new ContainerAccess();
            AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
            IList<Image> images = pdfReader.GetImages(Directory.GetCurrentDirectory() + @"\TestPDF\Test3.pdf");
            return images;
        }

		/// <summary>
		/// Extracts the pages.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="targetPath">The target path.</param>
		/// <param name="startPageNumber">The start page number.</param>
		/// <param name="endPageNumber">The end page number.</param>
        public static void ExtractPages(string sourcePath, string targetPath, int startPageNumber, int endPageNumber)
        {
            ContainerAccess container = new ContainerAccess();
            AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
            pdfReader.ExtractPages(sourcePath, targetPath, startPageNumber, endPageNumber);
        }
        

    }

    
}
