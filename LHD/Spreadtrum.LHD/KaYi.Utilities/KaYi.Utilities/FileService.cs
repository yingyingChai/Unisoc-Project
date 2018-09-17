using System;
using System.IO;
namespace KaYi.Utilities
{
	public static class FileService
	{
		public static bool DoesFileExsit(string filename)
		{
			return File.Exists(filename);
		}
		public static string GetExtendFileName(string fileName)
		{
			string text = string.Empty;
			for (int i = fileName.Length - 1; i >= 0; i--)
			{
				string text2 = fileName.Substring(i, 1);
				if (text2 == ".")
				{
					break;
				}
				text = text2 + text;
			}
			return text;
		}
		public static string GetFileNameWithoutExtendFileName(string fileName)
		{
			string extendFileName = FileService.GetExtendFileName(fileName);
			return fileName.Substring(0, fileName.Length - extendFileName.Length);
		}
		public static string GetFileNameWithoutExtendFileNameAndDot(string fileName)
		{
			string extendFileName = FileService.GetExtendFileName(fileName);
			return fileName.Substring(0, fileName.Length - extendFileName.Length - 1);
		}
		public static string AutoCorrectFilename(string originalFileName)
		{
			string extendFileName = FileService.GetExtendFileName(originalFileName);
			string text = originalFileName.Substring(0, originalFileName.Length - extendFileName.Length - 1);
			int num = 0;
			while (File.Exists(text + "." + extendFileName))
			{
				text = string.Format("{0}({1})", text, ++num);
			}
			return text + "." + extendFileName;
		}
		public static string GetFileSizeDescription(long size)
		{
			if (size < 1024L)
			{
				return size + "字节";
			}
			if (size >= 1024L && size < 1048576L)
			{
				return StringHelper.FormatDecimal((float)size / 1024f, 0) + " KB";
			}
			if (size >= 1048576L && size < 1073741824L)
			{
				return StringHelper.FormatDecimal((float)(size / 1024L) / 1024f, 1) + " MB";
			}
			if (size >= 1073741824L)
			{
				return StringHelper.FormatDecimal((float)(size / 1024L / 1024L) / 1024f, 1) + " GB";
			}
			return "N/A";
		}
		public static bool CheckFileNameHasInvalidChars(string filename)
		{
			return filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || filename == "." || filename == "..";
		}
		public static void WriteTextFile(string destFileName, string content)
		{
			FileStream expr_07 = new FileStream(destFileName, FileMode.Create);
			StreamWriter expr_0D = new StreamWriter(expr_07);
			expr_0D.Write(content);
			expr_0D.Flush();
			expr_0D.Close();
			expr_07.Close();
		}
		public static string ReadTextFile(string filename)
		{
			return File.ReadAllText(filename);
		}
	}
}
