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

		/// <summary>
		/// Saves the <see cref="Image"/> to the <see cref="FileSavePath"/> using the specified filename with a GUID string attached, and returns the new filename.
		/// </summary>
		/// <param name="image">The image to save.</param>
		/// <param name="fileName">The partial name of the image, with the file extension.</param>
		/// <returns>Returns the file name of the saved image.</returns>
		public string SaveImageToFile(Image image, string fileName);

		/// <summary>
		/// Saves a clone of the specified file in the <see cref="DefaultFileLoadPath"/> to the <see cref="FileSavePath"/> and returns the new file name.
		/// </summary>
		/// <param name="defaultFileName"></param>
		/// <returns></returns>
		public string SaveFileFromDefault(string defaultFileName);

		/// <summary>
		/// Deletes the file in the <see cref="FileSavePath"/> with the specified file name.
		/// </summary>
		/// <param name="fileName">The name of the file to delete.</param>
		/// <returns>Returns a boolean indicating successful deletion.</returns>
		public bool DeleteFile(string fileName);
	}
}
