using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Extensions {
	public static class FileExtensions {

		/// <summary>
		/// Ensures that the provided <see cref="IFormFile"/> is an image file type.
		/// </summary>
		/// <param name="formFile"></param>
		/// <returns></returns>
		public static bool ValidateFileTypeAsImage(this IFormFile formFile) {
			return formFile.ValidateFileType(
					new string[] {
						"image/png",
						"image/jpg",
						"image/jpeg",
						"image/bmp"
					}
				);
		}

		private static bool ValidateFileType(this IFormFile formFile, string[] types) {
			foreach (var i in types) {
				if (formFile.ContentType.Equals(i)) {
					return true;
				}
			}

			return false;
		}

	}
}
