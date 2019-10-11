using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2 {
	public static class Extensions {

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
