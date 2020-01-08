using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Extensions {
	public static class ImageExtensions {

		/// <summary>
		/// Resizes this <see cref="Image"/> so that the length of its largest side is <paramref name="sideLength"/>.
		/// </summary>
		/// <remarks>
		/// This calls <see cref="ResizeImage(Image, int, int)"/> so it will dispose of the input image.
		/// </remarks>
		/// <param name="image">The <see cref="Image"/> to resize.</param>
		/// <param name="sideLength">The desired maximum side length.</param>
		/// <returns>Returns the resized <see cref="Image"/>.</returns>
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
		/// Resizes this <see cref="Image"/> to the specified height and width.
		/// </summary>
		/// <remarks>
		/// This will dispose of the input <see cref="Image"/>.
		/// </remarks>
		/// <param name="image">The <see cref="Image"/> to resize.</param>
		/// <param name="newHeight">The desired height.</param>
		/// <param name="newWidth">The desired width.</param>
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
