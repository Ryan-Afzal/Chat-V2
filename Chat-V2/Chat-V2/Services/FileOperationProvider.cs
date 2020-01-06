using Chat_V2.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Services {
	public class FileOperationProvider : IFileOperationProvider {

		public FileOperationProvider(IConfiguration configuration) {
			FileSavePath = configuration["FileSavePath"];
			DefaultFileLoadPath = configuration["DefaultFileLoadPath"];
			FileLoadURL = configuration["FileLoadURL"];
			DefaultUserProfileImage = configuration["DefaultUserProfileImage"];
			DefaultGroupImage = configuration["DefaultGroupImage"];
		}

		public string FileSavePath { get; }
		public string DefaultFileLoadPath { get; }
		public string FileLoadURL { get; }
		public string DefaultUserProfileImage { get; }
		public string DefaultGroupImage { get; }

		public string SaveImageToFile(Image image, string fileName) {
			fileName = Guid.NewGuid().ToString() + "_" + fileName + ".png";
			string filePath = FileSavePath + "/" + fileName;

			using FileStream stream = File.Create(filePath);
			image.Save(stream, ImageFormat.Png);

			return fileName;
		}

		public string SaveFileFromDefault(string defaultName) {
			var oldPath = DefaultFileLoadPath + "/" + defaultName;
			var fileName = Guid.NewGuid().ToString() + "_" + defaultName;
			var filePath = FileSavePath + "/" + fileName;
			File.Copy(oldPath, filePath);

			return fileName;
		}

		public bool DeleteFile(string fileName) {
			var filePath = FileSavePath + "/" + fileName;

			File.Delete(filePath);

			return true;
		}
	}
}
