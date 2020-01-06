using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Extensions {
	public static class ImageExtensions {

		/// <summary>
		/// Resizes this <c>Image</c> so that the length of its largest side is <c>sideLength</c>.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="sideLength"></param>
		/// <returns></returns>
		public static Image ResizeImageToFitSquare(this Image image, int sideLength) {
			double aspectRatio = ((double)image.Height) / image.Width;

			int height;
			int width;

			if (image.Height > image.Width) {
				height = image.Height > sideLength ? sideLength : image.Height;
				width = (int)(height / aspectRatio);
			} else {
				width = image.Width > sideLength ? sideLength : image.Width;
				height = (int)(aspectRatio * width);
			}

			return image.ResizeImage(height, width);
		}

		/// <summary>
		/// This will dispose of the image being resized
		/// </summary>
		/// <param name="image"></param>
		/// <param name="newHeight"></param>
		/// <param name="newWidth"></param>
		/// <returns></returns>
		public static Image ResizeImage(this Image image, int newHeight, int newWidth) {
			Bitmap output = new Bitmap(newWidth, newHeight);
			Graphics g = Graphics.FromImage(output);
			g.InterpolationMode = InterpolationMode.High;
			g.DrawImage(image, 0, 0, newWidth, newHeight);
			image.Dispose();
			return output;
		}

	}
}
