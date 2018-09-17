using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
namespace KaYi.Utilities
{
	public static class Image
	{
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
		{
			System.Drawing.Image image = null;
			if (!File.Exists(originalImagePath))
			{
				return;
			}
			try
			{
				image = System.Drawing.Image.FromFile(originalImagePath);
			}
			catch (Exception)
			{
				return;
			}
			int x = 0;
			int y = 0;
			int width2 = image.Width;
			int height2 = image.Height;
			double num;
			if (image.Width >= image.Height)
			{
				num = (double)image.Width / (double)width;
			}
			else
			{
				num = (double)image.Height / (double)height;
			}
			int num2;
			int num3;
			int x2;
			int y2;
			if (width2 <= width && height2 <= height)
			{
				num2 = image.Width;
				num3 = image.Height;
				x2 = Convert.ToInt32(((double)width - (double)width2) / 2.0);
				y2 = Convert.ToInt32(((double)height - (double)height2) / 2.0);
			}
			else
			{
				num2 = Convert.ToInt32((double)image.Width / num);
				num3 = Convert.ToInt32((double)image.Height / num);
				y2 = Convert.ToInt32(((double)height - (double)num3) / 2.0);
				x2 = Convert.ToInt32(((double)width - (double)num2) / 2.0);
			}
			System.Drawing.Image image2 = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(image2);
			graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(ColorTranslator.FromHtml("#F2F2F2"));
			graphics.DrawImage(image, new Rectangle(x2, y2, num2, num3), new Rectangle(x, y, width2, height2), GraphicsUnit.Pixel);
			try
			{
				string a = Path.GetExtension(originalImagePath).ToLower();
				if (!(a == ".gif"))
				{
					if (!(a == ".jpg"))
					{
						if (!(a == ".bmp"))
						{
							if (a == ".png")
							{
								image2.Save(thumbnailPath, ImageFormat.Png);
							}
						}
						else
						{
							image2.Save(thumbnailPath, ImageFormat.Bmp);
						}
					}
					else
					{
						image2.Save(thumbnailPath, ImageFormat.Jpeg);
					}
				}
				else
				{
					image2.Save(thumbnailPath, ImageFormat.Gif);
				}
			}
			catch (Exception arg_1E5_0)
			{
				throw arg_1E5_0;
			}
			finally
			{
				image.Dispose();
				image2.Dispose();
				graphics.Dispose();
			}
		}
		public static int GetRemoteImage(string url, string saveDirectory, string fileName)
		{
			string[] array = new string[]
			{
				".gif",
				".png",
				".jpg",
				".jpeg",
				".bmp"
			};
			int num = 3000;
			url = url.Replace("&amp;", "&");
			new ArrayList();
			WebClient webClient = new WebClient();
			string arg_58_0 = string.Empty;
			string text = string.Empty;
			string value = string.Empty;
			try
			{
				text = url;
				if (text.Substring(0, 7) != "http://")
				{
					int result = -1;
					return result;
				}
				int startIndex = text.LastIndexOf('.');
				value = text.Substring(startIndex).ToLower();
				if (Array.IndexOf<string>(array, value) == -1)
				{
					int result = -1;
					return result;
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)WebRequest.Create(text).GetResponse();
				if (httpWebResponse.ResponseUri.Scheme.ToLower().Trim() != "http")
				{
					int result = -1;
					return result;
				}
				if (httpWebResponse.ContentLength > (long)(num * 1024))
				{
					int result = -1;
					return result;
				}
				if (httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					int result = -1;
					return result;
				}
				if (httpWebResponse.ContentType.IndexOf("image") == -1)
				{
					int result = -1;
					return result;
				}
				httpWebResponse.Close();
				if (!Directory.Exists(saveDirectory))
				{
					Directory.CreateDirectory(saveDirectory);
				}
				webClient.DownloadFile(text, saveDirectory + fileName);
			}
			catch (Exception)
			{
				int result = -1;
				return result;
			}
			finally
			{
				webClient.Dispose();
			}
			return 0;
		}
	}
}
