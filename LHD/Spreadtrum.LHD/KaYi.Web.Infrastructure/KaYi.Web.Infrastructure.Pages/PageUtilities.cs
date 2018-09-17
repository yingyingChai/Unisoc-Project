using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
namespace KaYi.Web.Infrastructure.Pages
{
	public static class PageUtilities
	{
		public static string MakeMoviePlayer(string url, int width, int height, string thumbnail)
		{
			string text = Guid.NewGuid().ToString();
			return string.Format("<div id=\"{0}\"><strong>您的Flash Player版本过低，请先升级Adobe Flash Player</strong></div><script type=\"text/javascript\">var so = new SWFObject(\"VideoPlayer.swf\", \"{0}\", \"{2}\", \"{3}\", \"9\", \"#000000\");so.addParam(\"allowfullscreen\", \"true\");so.addParam(\"allowscriptaccess\", \"always\");so.addParam(\"wmode\", \"opaque\");so.addParam(\"quality\", \"high\");so.addParam(\"salign\", \"lt\");so.addVariable(\"CuPlayerFile\", \"{1}\");so.addVariable(\"CuPlayerImage\", \"{4}\");so.addVariable(\"CuPlayerShowImage\", \"true\");so.addVariable(\"CuPlayerWidth\", \"{2}\");so.addVariable(\"CuPlayerHeight\", \"{3}\");so.addVariable(\"CuPlayerAutoPlay\", \"false\");so.addVariable(\"CuPlayerAutoRepeat\", \"false\");so.addVariable(\"CuPlayerShowControl\", \"true\");so.addVariable(\"CuPlayerAutoHideControl\", \"true\");so.addVariable(\"CuPlayerAutoHideTime\", \"6\");so.addVariable(\"CuPlayerVolume\", \"80\");so.write(\"{0}\");</script>", new object[]
			{
				text,
				url,
				width,
				height,
				thumbnail
			});
		}
		private static int FindIndex(byte[] array, byte[] array2)
		{
			int num = 0;
			while (num < array.Length && num + array2.Length <= array.Length)
			{
				int num2 = 0;
				while (num2 < array2.Length && array[num + num2] == array2[num2])
				{
					num2++;
				}
				if (num2 == array2.Length)
				{
					return num;
				}
				num++;
			}
			return -1;
		}
		public static IList<byte[]> GetHttpPostFiles(byte[] data, string separator)
		{
			byte[] bytes = Encoding.Default.GetBytes(separator);
			IList<byte[]> list = new List<byte[]>();
			while (data.Length != 0)
			{
				int num = PageUtilities.FindIndex(data, bytes);
				if (num < 0)
				{
					list.Add(data);
					break;
				}
				byte[] array = new byte[num];
				Array.Copy(data, 0, array, 0, num);
				list.Add(array);
				byte[] array2 = new byte[data.Length - num];
				Array.Copy(data, num + bytes.Length + 1, array2, 0, array2.Length - bytes.Length - 1);
				data = array2;
			}
			return list;
		}
		public static byte[] GetHttpPostFile(HttpRequest request, string fileControlName, out string fileName, out string extendFileName)
		{
			int totalBytes = request.TotalBytes;
			byte[] arg_15_0 = request.BinaryRead(totalBytes);
			byte[] array = new byte[totalBytes];
			int num = PageUtilities.FindIndex(arg_15_0, Encoding.Default.GetBytes("\r\n"));
			byte[] array2 = new byte[num];
			Array.Copy(arg_15_0, 0, array2, 0, num);
			int num2 = PageUtilities.FindIndex(arg_15_0, Encoding.Default.GetBytes(fileControlName));
			Array.Copy(arg_15_0, num2, array, 0, totalBytes - num2);
			int num3 = PageUtilities.FindIndex(array, Encoding.Default.GetBytes("filename=\""));
			Array.Copy(array, num3 + 10, array, 0, array.Length - num3 - 10);
			int num4 = PageUtilities.FindIndex(array, Encoding.Default.GetBytes("\"\r\n"));
			byte[] array3 = new byte[num4];
			Array.Copy(array, 0, array3, 0, num4);
			fileName = Encoding.UTF8.GetString(array3);
			string[] array4 = fileName.Split(new char[]
			{
				'.'
			});
			extendFileName = array4[array4.Length - 1];
			int num5 = PageUtilities.FindIndex(array, Encoding.Default.GetBytes("\r\n\r\n")) + 4;
			Array.Copy(array, num5, array, 0, array.Length - num5);
			int num6 = PageUtilities.FindIndex(array, array2) - 2;
			byte[] array5 = new byte[num6];
			Array.Copy(array, 0, array5, 0, num6);
			return array5;
		}
		public static void SaveDataToFile(byte[] data, string fileName)
		{
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			FileStream expr_16 = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			expr_16.Write(data, 0, data.Length);
			expr_16.Close();
		}
	}
}
