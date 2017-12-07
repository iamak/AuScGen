using AForge.Imaging;
using AuScGen.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuScGen.ImageBlobTest
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
            string imagepath = Directory.GetCurrentDirectory() + @"\Data";
            ImageProcessor imgProcessor = new ImageProcessor(new Bitmap(imagepath + @"\GoogleSample.jpg"));
            imgProcessor.ConvertTOGrayscale(0.2125, 0.7154, 0.0721);
            imgProcessor.ImageBitmap.Save(Directory.GetCurrentDirectory() + @"\Output\" + "test.bmp");
            imgProcessor.BinaryImage(100);
            imgProcessor.ImageBitmap.Save(Directory.GetCurrentDirectory() + @"\Output\" + "test2.bmp");
            IList<Blob> blobs = imgProcessor.ExtractBlob();
            imgProcessor.SaveBlobsToLocal(Directory.GetCurrentDirectory() + @"\Output\", imgProcessor.ExtractBlob());

            
            blobs.ToList().ForEach(blob => 
            {
                IList<Rectangle> rectangles = imgProcessor.GetBlobRectangles;
                
                foreach(Rectangle rect in rectangles)
                {
                    Highlight(rect);
                }
                
            });
            

            //Bitmap bmp1 = new Bitmap(imagepath + @"\Graph.png");
            //Bitmap grayImage = blob.ConvertTOGrayScale(0.2125, 0.7154, 0.0721, bmp1);
            ////grayImage.Save(Directory.GetCurrentDirectory() + @"\Output\" + "test.bmp");
            //Bitmap binarizedImage = blob.BinarizeImage(100, grayImage);
            ////binarizedImage.Save(Directory.GetCurrentDirectory() + @"\Output\" + "test2.bmp");

            //ImageProcessor blob2 = new ImageProcessor(binarizedImage);
            //var test2 = blob2.ExtractBlob(55, 130,0,0);
            //blob2.SaveBlobsToLocal(Directory.GetCurrentDirectory() + @"\Output", test2);
        }


		/// <summary>
		/// Highlights the specified rect.
		/// </summary>
		/// <param name="rect">The rect.</param>
        public static void Highlight(Rectangle rect)
        {
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);


            SolidBrush b = new SolidBrush(Color.Red);

            Pen p = new Pen(b, 3);
            g.DrawRectangle(p, (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

            //g.Dispose();
            //p.Dispose();
            //ReleaseDC(IntPtr.Zero, desktopPtr);
        }

		/// <summary>
		/// Gets the dc.
		/// </summary>
		/// <param name="hwnd">The HWND.</param>
		/// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

		/// <summary>
		/// Releases the dc.
		/// </summary>
		/// <param name="hwnd">The HWND.</param>
		/// <param name="dc">The dc.</param>
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc); 
    }
}
