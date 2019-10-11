using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_V2 {
	public class ImageTools {

		public static byte[] GetImageFromFile(FileInfo info) {
			long imageFileLength = info.Length;
			using FileStream fs = System.IO.File.OpenRead(info.FullName);
			using BinaryReader br = new BinaryReader(fs);

			return br.ReadBytes((int)imageFileLength);
		}

		public static FileInfo GetFileInfoFromFile(string filename, IWebHostEnvironment env) {
			return new FileInfo(Path.Combine(env.ContentRootPath, filename));
		}

	}
}
