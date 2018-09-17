using System;
using System.Security.Cryptography;
using System.Text;
namespace KaYi.Utilities
{
	public static class MD5Utilities
	{
		public static string GetMD5(string sDataIn)
		{
			MD5CryptoServiceProvider arg_11_0 = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(sDataIn);
			byte[] array = arg_11_0.ComputeHash(bytes);
			arg_11_0.Clear();
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("X").PadLeft(2, '0');
			}
			return text.ToLower();
		}
	}
}
