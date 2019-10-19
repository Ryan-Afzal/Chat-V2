using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2 {
	public static class FileTools {

		public static readonly string FileSavePath = "~/Images/";
		public static readonly string DefaultImagePath = "~/DefaultImages/";

		public static readonly string DefaultUserProfileImage = "defaultProfileImage.png";
		public static readonly string DefaultGroupImage = "defaultGroupImage.png";

		[Obsolete]
		public static byte[] GetImageFromFile(FileInfo info) {
			long imageFileLength = info.Length;
			using FileStream fs = System.IO.File.OpenRead(info.FullName);
			using BinaryReader br = new BinaryReader(fs);

			return br.ReadBytes((int)imageFileLength);
		}

		[Obsolete]
		public static FileInfo GetFileInfoFromFile(string filename, IWebHostEnvironment env) {
			return new FileInfo(Path.Combine(env.ContentRootPath, filename));
		}

		public static bool ValidateFileAsImage(this IFormFile formFile) {
			return formFile.ValidateFile(
					new string[] {
						"png",
						"jpg",
						"jpeg",
						"bmp",
						"gif"
					}
				);
		}

		private static bool ValidateFile(this IFormFile formFile, string[] types) {
			foreach (var i in types) {
				if (formFile.ContentType.Equals(i)) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Saves the given file to a random name, and returns the file name
		/// </summary>
		/// <param name="formFile"></param>
		/// <returns>Returns the saved file name</returns>
		public static string SaveFile(Image image, string fileName) {
			fileName = Guid.NewGuid().ToString() + "_" + fileName;
			var filePath = Path.Combine(FileSavePath, fileName);

			using FileStream stream = File.Create(filePath);
			image.Save(stream, image.RawFormat);

			return fileName;
		}

		public static string SaveFileFromDefault(string defaultName) {
			var oldPath = Path.Combine(DefaultImagePath, defaultName);
			var fileName = Guid.NewGuid().ToString() + "_" + defaultName;
			var filePath = Path.Combine(FileSavePath, fileName);
			File.Copy(oldPath, filePath);

			return fileName;
		}

		public static bool DeleteFile(string fileName) {
			var filePath = Path.Combine(FileSavePath, fileName);

			File.Delete(filePath);

			return true;
		}

	}
}
