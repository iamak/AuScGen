// ***********************************************************************
// <copyright file="PDFReader.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>PDFReader class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Framework;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace AuScGen.PDFOperation
{
	/// <summary>
	///		Class Pdf Reader
	/// </summary>
	[Export(typeof(IPlugin))]
	public class PDFReader : IPlugin
	{
		/// <summary>
		/// Extracts the text from PDF.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public string ExtractTextFromPdf(string path)
		{
			using (PdfReader reader = new PdfReader(path))
			{
				StringBuilder text = new StringBuilder();

				for (int i = 1; i <= reader.NumberOfPages; i++)
				{
					text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
				}

				return text.ToString();
			}
		}

		/// <summary>
		/// Compares the image from PDF.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="testImage">The test image.</param>
		/// <returns></returns>
		public ReadOnlyCollection<double> CompareImageFromPdf(string path, string testImage)
		{
			List<double> match = new List<double>();
			IList<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();

			ImgList = GetImages(path);
			ImgList.ToList().ForEach(img => img.Save("Image_" + Guid.NewGuid() + ".bmp"));
			ImgList.ToList().ForEach(img => match.Add(ImageTest(testImage, img)));
			return match.AsReadOnly();
		}

		/// <summary>
		/// Gets the images.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public IList<System.Drawing.Image> GetImages(string path)
		{
			FileStream fs = File.OpenRead(path);
			byte[] data = new byte[fs.Length];
			fs.Read(data, 0, (int)fs.Length);

			//List<double> match = new List<double>();
			List<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();

			//iTextSharp.text.pdf.RandomAccessFileOrArray RAFObj = null;
			PdfReader PDFReaderObj = null;
			PdfObject PDFObj = null;
			PdfStream PDFStremObj = null;
			PdfDictionary dic;

			//RAFObj = new RandomAccessFileOrArray(data);
			PDFReaderObj = new PdfReader(path);

			for (int i = 0; i <= PDFReaderObj.XrefSize - 1; i++)
			{
				PDFObj = PDFReaderObj.GetPdfObject(i);

				string filter;

				if ((PDFObj != null) && PDFObj.IsStream())
				{
					PDFStremObj = (PdfStream)PDFObj;
					PdfObject subtype = PDFStremObj.Get(PdfName.SUBTYPE);

					if ((subtype != null) && subtype.ToString() == PdfName.IMAGE.ToString())
					{
						byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)PDFStremObj);

						if ((bytes != null))
						{
							dic = (PdfDictionary)PDFObj;
							MemoryStream ms = new System.IO.MemoryStream(bytes);
							filter = dic.Get(PdfName.FILTER).ToString();
							if (filter.Equals("/DCTDecode"))
							{
								ms.Position = 0;
								System.Drawing.Image ImgPDF = System.Drawing.Image.FromStream(ms);

								ImgList.Add(ImgPDF);
							}

						}
					}
				}
			}
			PDFReaderObj.Close();
			return ImgList;
		}

		/// <summary>
		/// Images the test.
		/// </summary>
		/// <param name="testBmpPath">The test BMP path.</param>
		/// <param name="actualBmp">The actual BMP.</param>
		/// <returns></returns>
		public double ImageTest(string testBmpPath, System.Drawing.Image actualBmp)
		{
			Bitmap myBitmap = new Bitmap(testBmpPath);
			Bitmap testBitmap = new Bitmap(actualBmp);
			double requiredColorPixel = 0.0;
			int width = myBitmap.Width <= testBitmap.Width ? myBitmap.Width : testBitmap.Width;
			int height = myBitmap.Height <= testBitmap.Height ? myBitmap.Height : testBitmap.Height;

			double totalNumberOfPixels = width * height;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Color pixelColor = myBitmap.GetPixel(x, y);
					Color testpixel = testBitmap.GetPixel(x, y);

					//if (pixelColor.G > 200 && pixelColor.R < 100 && pixelColor.B < 100)
					//{
					//    //Console.WriteLine("Green");
					//    requiredColorPixel++;
					//}

					if (pixelColor.A == testpixel.A && pixelColor.R == testpixel.R && pixelColor.G == testpixel.G && pixelColor.B == testpixel.B)
					{
						requiredColorPixel++;
					}
					// things we do with pixelColor
				}
			}

			return (requiredColorPixel / totalNumberOfPixels) * 100;

		}

		//
		// Event handler for extract images button
		//
		/// <summary>
		/// Extracts the image from PDF1.
		/// </summary>
		/// <param name="sourcePdf">The source PDF.</param>
		protected internal void ExtractImageFromPdf1(string sourcePdf)
		{
			MemoryStream memStream;
			byte[] bytes;

			// Imagecount is used to allow a unique key to be generated for each image - the key will be the imagecount itsself
			int imagecount = 0;

			//
			// Now we can start by loading the PDF document into our IText control
			//
			try
			{
				PdfReader rd = new PdfReader(sourcePdf);
				for (int i = 0; i < rd.XrefSize; i++)
				{
					PdfObject pdfObj = rd.GetPdfObject(i);
					if (pdfObj != null)
					{
						if (pdfObj.IsStream() == true)
						{

							PdfStream stream = (PdfStream)pdfObj;

							PdfObject subtype = stream.Get(PdfName.SUBTYPE);
							if (subtype != null)
							{
								if (subtype.ToString() == PdfName.IMAGE.ToString())
								{
									// Now we need to get the image stream
									bytes = PdfReader.GetStreamBytesRaw((PRStream)stream);

									if (bytes != null)
									{
										PdfDictionary dic = (PdfDictionary)pdfObj;

										//
										// Get the information about this image
										//
										string filter = dic.Get(PdfName.FILTER).ToString();
										string width = dic.Get(PdfName.WIDTH).ToString();
										string height = dic.Get(PdfName.HEIGHT).ToString();
										string bpp = dic.Get(PdfName.BITSPERCOMPONENT).ToString();

										try
										{
											BinaryWriter bw = null;
											FileStream fs = null;

											switch (filter)
											{
												case "/DCTDecode":
													memStream = new MemoryStream(bytes);
													memStream.Position = 0;

													// Create the image from the stream
													System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);

													// Save the image
													img.Save(imagecount + ".jpg", ImageFormat.Jpeg);

													break;

												case "/JBIG2Decode":

													//                               STANDARD HEADER                                    NO PAGES, SEQUENTIAL ORDER
													byte[] JBIGHeader = new byte[] { 0x97, 0x4a, 0x42, 0x32, 0x0d, 0x0a, 0x1a, 0x0a, 0x3 };
													fs = new FileStream(imagecount + ".jb2", FileMode.Create, FileAccess.ReadWrite);
													bw = new BinaryWriter(fs);
													bw.Write(JBIGHeader);
													bw.Write(bytes);

													bw.Close();

													break;

												case "/FlateDecode":

													PixelFormat pf = PixelFormat.Format24bppRgb;
													string fileext = ".png";
													ImageFormat imgformat = ImageFormat.Png;

													int actualw = int.Parse(width);
													int actualh = int.Parse(height);

													switch (int.Parse(bpp))
													{
														case 1:
															pf = PixelFormat.Format1bppIndexed;
															imgformat = ImageFormat.Gif;
															fileext = ".gif";
															break;
														case 8:
															pf = PixelFormat.Format8bppIndexed;
															imgformat = ImageFormat.Png;
															fileext = ".png";
															break;
														case 24:
															pf = PixelFormat.Format24bppRgb;
															break;
														default:
															Debug.Write("Unknown Pixel Format: " + bpp);
															break;
													}

													bytes = PdfReader.FlateDecode(bytes, true);

													//Prepare a working image buffer to recreate the PDF image
													Bitmap bmp = new Bitmap(actualw, actualh, pf);
													BitmapData bmd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, actualw, actualh), ImageLockMode.WriteOnly, pf);

													//Now the complex part of working out how to process the byte stream                                                    
													//We need the w,h,bpp, and component
													//gray/rgb/cmyk                                                 
													bmp.UnlockBits(bmd);
													bmp.Save(imagecount + fileext, imgformat);

													break;

												default:

													// Unhandled filter
													break;
											}

											// Increase our image
											imagecount++;

										}
										catch
										{
											// Unknown image type
											Debug.Write("Image error");
											throw;
											//MessageBox.Show("Unknown image or something!");

										}
									}
								}
							}
						}
					}
				}

			}
			catch
			{
				throw;
				//MessageBox.Show("Error reading the PDF document.");
			}
			// Show how many we actually extracted
			//MessageBox.Show("Processed " + imagecount + " images");

			//// Should we show the output folder?
			//ShowOutputFolder();
		}

		/// <summary>
		/// Extracts the pages.
		/// </summary>
		/// <param name="sourcePdfPath">The source PDF path.</param>
		/// <param name="outputPdfPath">The output PDF path.</param>
		/// <param name="startPage">The start page.</param>
		/// <param name="endPage">The end page.</param>
		public void ExtractPages(string sourcePdfPath, string outputPdfPath, int startPage, int endPage)
		{
			PdfReader reader = null;
			Document sourceDocument = null;
			PdfCopy pdfCopyProvider = null;
			PdfImportedPage importedPage = null;

			try
			{
				// Intialize a new PdfReader instance with the contents of the source Pdf file:
				reader = new PdfReader(sourcePdfPath);

				// For simplicity, I am assuming all the pages share the same size
				// and rotation as the first page:
				sourceDocument = new Document(reader.GetPageSizeWithRotation(startPage));

				// Initialize an instance of the PdfCopyClass with the source 
				// document and an output file stream:
				pdfCopyProvider = new PdfCopy(sourceDocument,
					new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

				sourceDocument.Open();

				// Walk the specified range and add the page copies to the output file:
				for (int i = startPage; i <= endPage; i++)
				{
					importedPage = pdfCopyProvider.GetImportedPage(reader, i);
					pdfCopyProvider.AddPage(importedPage);
				}
				sourceDocument.Close();
				reader.Close();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the number of pages.
		/// </summary>
		/// <param name="sourcePdfPath">The source PDF path.</param>
		/// <returns></returns>
		public int GetNumberOfPages(string sourcePdfPath)
		{
			PdfReader reader = null;

			try
			{
				reader = new PdfReader(sourcePdfPath);
				return reader.NumberOfPages;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Extracts the XML.
		/// </summary>
		/// <param name="sourcePdfPath">The source PDF path.</param>
		/// <param name="considerNonTabular">if set to <c>true</c> [consider non tabular].</param>
		public void ExtractXml(string sourcePdfPath, bool considerNonTabular)
		{
			// Convert PDF file to XML file.
			SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
			// This property is necessary only for registered version.
			//f.Serial = "XXXXXXXXXXX";
			// Let's convert only tables to XML and skip all textual data.
			f.XmlOptions.ConvertNonTabularDataToSpreadsheet = considerNonTabular;

			f.OpenPdf(sourcePdfPath);

			if (f.PageCount > 0)
			{

			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description
		{
			get
			{
				return "PDF Reader";
			}
			set
			{
				Description = value;
			}
		}
	}
}
