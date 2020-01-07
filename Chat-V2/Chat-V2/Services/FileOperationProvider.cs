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
			FileSavePath = configuration["FileOperations:FileSavePath"];
			DefaultFileLoadPath = configuration["FileOperations:DefaultFileLoadPath"];
			FileLoadURL = configuration["FileOperations:FileLoadURL"];
			DefaultUserProfileImage = configuration["FileOperations:DefaultUserProfileImage"];
			DefaultGroupImage = configuration["FileOperations:DefaultGroupImage"];
		}

		public string FileSavePath { get; }
		public string DefaultFileLoadPath { get; }
		public string FileLoadURL { get; }
		public string DefaultUserProfileImage { get; }
		public string DefaultGroupImage { get; }

		public string SaveImageToFile(Image image, string fileName) {
			string newFileName = Guid.NewGuid().ToString() + "_" + fileName + ".png";
			string filePath = this.GetFilePathFromComponents(FileSavePath, "/", newFileName);

			using FileStream stream = File.Create(filePath);
			image.Save(stream, ImageFormat.Png);

			return newFileName;
		}

		public string SaveFileFromDefault(string defaultName) {
			string oldPath = this.GetFilePathFromComponents(DefaultFileLoadPath, "/", defaultName);
			string fileName = Guid.NewGuid().ToString() + "_" + defaultName;
			string filePath = this.GetFilePathFromComponents(FileSavePath, "/", fileName);
			File.Copy(oldPath, filePath);

			return fileName;
		}

		public bool DeleteFile(string fileName) {
			string filePath = this.GetFilePathFromComponents(FileSavePath, "/", fileName);
			
			File.Delete(filePath);

			return true;
		}

		private string GetFilePathFromComponents(params string[] input) {
			string path = Path.Join(input);

			if (Path.IsPathRooted(path)) {
				return path;
			} else {
				return Path.Combine(Directory.GetCurrentDirectory(), path);
			}
		}
	}
}
