// ***********************************************************************
// <copyright file="Program.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Program class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace AuScGen.ERT.PDFSplit
{
	/// <summary>
	/// Class Program
	/// </summary>
	class Program
	{
		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		private static ContainerAccess Container
		{
			get
			{
				ContainerAccess container = new ContainerAccess();
				return container;
			}
		}

		/// <summary>
		/// Gets the PDF read plugin.
		/// </summary>
		/// <value>
		/// The PDF read plugin.
		/// </value>
		private static AuScGen.PDFOperation.PDFReader PDFReadPlugin
		{
			get
			{
				return Container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
			}
		}

		//private static StreamWriter indexFile;

		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		static void Main()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Starting extraction .....");
			Utils.FileSystemUtil fileUtil = new Utils.FileSystemUtil();

			IList<Models.PdfFile> files = fileUtil.GetAllFiles;
			//ExtractPages(files.LastOrDefault().SourcePath, files.LastOrDefault().TargetPath + "Page58.pdf", 58, 58);

			foreach (Models.PdfFile file in files)
			{
				int numberOfPages = PDFReadPlugin.GetNumberOfPages(file.SourcePath);

				for (int i = 1; i <= numberOfPages; i++)
				{
					string pagePath = string.Format(@"{0}\Page{1}", file.TargetPath, i);
					Utils.FileSystemUtil.CreateDirectory(pagePath);
					string targetpdf = string.Format(@"{0}\Page{1}.pdf", pagePath, i);
					ExtractPages(file.SourcePath, targetpdf, i, i);
					ExtractText(targetpdf);
					ExtractXml(targetpdf);
					if (Utils.FileSystemUtil.DeleteSplitAfterExtraction)
					{
						DeleteFile(targetpdf);
					}
				}

				EndOfExtraction(file.TargetPath);

				LogIndexFile(Path.GetDirectoryName(file.SourcePath), string.Format("Extracted [{0}/{1}] files: pdf name {2}", files.IndexOf(file) + 1, files.Count(), Path.GetFileName(file.SourcePath)));

			}

			//foreach (string value in PDFRead())
			//{
			//    Console.WriteLine(value);
			//}

			//foreach (Image image in GetImagesFromPdf())
			//{
			//    image.Save("Image_" + Guid.NewGuid() + ".bmp");
			//}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Extraction complete.....");
			Console.Read();
			//indexFile.Close();
		}

		/// <summary>
		/// PDFs the read.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		private static List<string> PDFRead(string path)
		{
			//ContainerAccess container = new ContainerAccess();
			//AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
			string pdfContent = PDFReadPlugin.ExtractTextFromPdf(path);
			List<string> values = pdfContent.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();

			return values;
		}

		/// <summary>
		/// Gets the images from PDF.
		/// </summary>
		/// <returns></returns>
		protected internal static IList<Image> GetImagesFromPdf()
		{
			//ContainerAccess container = new ContainerAccess();
			//AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
			IList<Image> images = PDFReadPlugin.GetImages(Directory.GetCurrentDirectory() + @"\TestPDF\Test3.pdf");
			return images;
		}

		/// <summary>
		/// Extracts the pages.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="targetPath">The target path.</param>
		/// <param name="startPageNumber">The start page number.</param>
		/// <param name="endPageNumber">The end page number.</param>
		private static void ExtractPages(string sourcePath, string targetPath, int startPageNumber, int endPageNumber)
		{
			//ContainerAccess container = new ContainerAccess();
			//AuScGen.PDFOperation.PDFReader pdfReader = container.GetPlugin<AuScGen.PDFOperation.PDFReader>();
			PDFReadPlugin.ExtractPages(sourcePath, targetPath, startPageNumber, endPageNumber);

		}

		/// <summary>
		/// Extracts the text.
		/// </summary>
		/// <param name="path">The path.</param>
		private static void ExtractText(string path)
		{
			string directory = Path.GetDirectoryName(path);
			string filename = Path.GetFileNameWithoutExtension(path);
			StreamWriter textfile = File.CreateText(string.Format(@"{0}\{1}.txt", directory, filename));
			List<string> textList = PDFRead(path);
			foreach (string text in textList)
			{
				textfile.WriteLine(text);
			}
			textfile.Close();
		}

		/// <summary>
		/// Extracts the XML.
		/// </summary>
		/// <param name="targetPdf">The target PDF.</param>
		private static void ExtractXml(string targetPdf)
		{
			PDFReadPlugin.ExtractXml(targetPdf, Utils.FileSystemUtil.MergePlaintextInTable);
		}

		/// <summary>
		/// Ends the of extraction.
		/// </summary>
		/// <param name="path">The path.</param>
		private static void EndOfExtraction(string path)
		{
			string directory = Path.GetDirectoryName(path);            
		}

		/// <summary>
		/// Logs the index file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="text">The text.</param>
		private static void LogIndexFile(string path, string text)
		{
			StreamWriter indexFile;
			if (!File.Exists(string.Format(@"{0}\Index.log", path)))
			{
				indexFile = File.CreateText(string.Format(@"{0}\Index.log", path));
			}
			else
			{
				indexFile = File.AppendText(string.Format(@"{0}\Index.log", path));
			}
			indexFile.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(text);
			indexFile.Close();
		}

		/// <summary>
		/// Deletes the file.
		/// </summary>
		/// <param name="path">The path.</param>
		private static void DeleteFile(string path)
		{
			File.Delete(path);
		}
	}
}
