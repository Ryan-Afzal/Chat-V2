using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2.Interfaces {
	public interface IFileOperationProvider {
		/// <summary>
		/// The save path for files
		/// </summary>
		public string FileSavePath { get; }

		/// <summary>
		/// The URL used to load files
		/// </summary>
		public string FileLoadURL { get; }

		/// <summary>
		/// The loading path for default files
		/// </summary>
		public string DefaultFileLoadPath { get; }

		/// <summary>
		/// The name of the default user profile image
		/// </summary>
		public string DefaultUserProfileImage { get; }

		/// <summary>
		/// The name of the default group image
		/// </summary>
		public string DefaultGroupImage { get; }

		public string SaveImageToFile(Image image, string fileName);
		public string SaveFileFromDefault(string defaultFileName);
		public bool DeleteFile(string fileName);
	}
}
