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

		private static string fileSavePath = "Files/";
		private static string defaultImagePath = "DefaultFiles/";

		private static string defaultUserProfileImage = "defaultProfileImage.png";
		private static string defaultGroupImage = "defaultGroupImage.png";

		public static string FileSavePath {
			get {
				return fileSavePath;
			}

			private set {
				fileSavePath = value;
			}
		}

		public static string DefaultImagePath {
			get {
				return defaultImagePath;
			}

			private set {
				defaultImagePath = value;
			}
		}

		public static string DefaultUserProfileImage {
			get {
				return defaultUserProfileImage;
			}

			private set {
				defaultUserProfileImage = value;
			}
		}

		public static string DefaultGroupImage {
			get {
				return defaultGroupImage;
			}

			private set {
				defaultGroupImage = value;
			}
		}

		internal static void LoadDataFromConfig(WebHostBuilderContext context) {
			FileSavePath = context.Configuration["FileTools:FileSavePath"];
			DefaultImagePath = context.Configuration["FileTools:DefaultImagePath"];
			DefaultUserProfileImage = context.Configuration["FileTools:DefaultUserProfileImage"];
			DefaultGroupImage = context.Configuration["FileTools:DefaultGroupImage"];
		}

		public static bool ValidateFileAsImage(this IFormFile formFile) {
			return formFile.ValidateFile(
					new string[] {
						"image/png",
						"image/jpg",
						"image/jpeg",
						"image/bmp"
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
			fileName = Guid.NewGuid().ToString() + "_" + fileName + ".png";
			string filePath = Path.Combine(FileSavePath, fileName);

			using FileStream stream = File.Create(filePath);
			image.Save(stream, ImageFormat.Png);

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
