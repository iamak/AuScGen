// ***********************************************************************
// <copyright file="ImageProcessor.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ImageProcessor class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AuScGen.Imaging
{
	/// <summary>
	///		Class Image Processor
	/// </summary>
	public class ImageProcessor
	{
		/// <summary>
		/// Gets or sets the image bitmap.
		/// </summary>
		/// <value>
		/// The image bitmap.
		/// </value>
		public Bitmap ImageBitmap { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageProcessor"/> class.
		/// </summary>
		public ImageProcessor()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageProcessor"/> class.
		/// </summary>
		/// <param name="image">The image.</param>
		public ImageProcessor(Bitmap image)
		{
			ImageBitmap = image;
		}

		/// <summary>
		/// Gets the edge points.
		/// </summary>
		/// <param name="blob">The BLOB.</param>
		/// <returns></returns>
		public IList<IntPoint> GetEdgePoints(Blob blob)
		{
			BlobCounter blobCounter = new BlobCounter();
			blobCounter.ProcessImage(ImageBitmap);
			return blobCounter.GetBlobsEdgePoints(blob);
		}

		/// <summary>
		/// Gets the get BLOB rectangles.
		/// </summary>
		/// <value>
		/// The get BLOB rectangles.
		/// </value>
		public IList<Rectangle> GetBlobRectangles
		{
			get
			{
				BlobCounter blobCounter = new BlobCounter();
				blobCounter.ProcessImage(ImageBitmap);
				return blobCounter.GetObjectsRectangles().ToList();
			}
		}

		/// <summary>
		/// Extracts the BLOB.
		/// </summary>
		/// <returns></returns>
		public IList<Blob> ExtractBlob()
		{
			// create instance of blob counter
			BlobCounter blobCounter = new BlobCounter();
			// process input image
			blobCounter.ProcessImage(ImageBitmap);

			// get information about detected objects   

			Blob[] blobArray = blobCounter.GetObjectsInformation();

			foreach (Blob blobdata in blobArray)
			{
				blobCounter.ExtractBlobsImage(ImageBitmap, blobdata, true);
			}

			return blobArray.ToList();
		}

		/// <summary>
		/// Extracts the BLOB.
		/// </summary>
		/// <param name="maxWidth">The maximum width.</param>
		/// <param name="maxHeight">The maximum height.</param>
		/// <param name="minWidth">The minimum width.</param>
		/// <param name="minHeight">The minimum height.</param>
		/// <returns></returns>
		public IList<Blob> ExtractBlob(int maxWidth, int maxHeight, int minWidth, int minHeight)
		{
			BlobCounter blobCounter = new BlobCounter();
			blobCounter.MinHeight = minHeight;
			blobCounter.MinWidth = minWidth;
			blobCounter.MaxWidth = maxWidth;
			blobCounter.MaxHeight = maxHeight;
			blobCounter.FilterBlobs = true;
			// process input image
			blobCounter.ProcessImage(ImageBitmap);

			// get information about detected objects   

			Blob[] blobArray = blobCounter.GetObjectsInformation();

			foreach (Blob blobdata in blobArray)
			{
				blobCounter.ExtractBlobsImage(ImageBitmap, blobdata, true);
			}

			return blobArray.ToList();
		}

		/// <summary>
		/// Converts to gray scale.
		/// </summary>
		/// <param name="colorRed">The cr.</param>
		/// <param name="colorGreen">The cg.</param>
		/// <param name="colorBlack">The cb.</param>
		/// <returns></returns>
		public Bitmap ConvertTOGrayscale(double colorRed, double colorGreen, double colorBlack)
		{
			Grayscale grayScaleFilter = new Grayscale(colorRed, colorGreen, colorBlack);
			Bitmap convertedImage = grayScaleFilter.Apply(ImageBitmap);
			ImageBitmap = convertedImage;
			return convertedImage;
		}

		/// <summary>
		/// Binarizes the image.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		/// <returns></returns>
		public Bitmap BinaryImage(int threshold)
		{
			Threshold filter = new Threshold(threshold);
			//ConvertTOGrayScale(0.2125, 0.7154, 0.0721);
			Bitmap convertedImage = filter.Apply(ImageBitmap);
			ImageBitmap = convertedImage;
			return convertedImage;
		}

		/// <summary>
		/// Saves the blobs to local.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="blobs">The blobs.</param>
		public void SaveBlobsToLocal(string path, IList<Blob> blobs)
		{
			UnmanagedImage image;
			foreach (Blob blob in blobs)
			{
				image = blob.Image;
				image.ToManagedImage().Save(path + @"\Image_" + Guid.NewGuid() + ".png");
			}
		}
	}
}
