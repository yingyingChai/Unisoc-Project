using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace KaYi.Utilities
{
	public class FcEncode
	{
		private static byte[] GetLegalKey(SymmetricAlgorithm pCryptoService, string Key)
		{
			string text = Key;
			pCryptoService.GenerateKey();
			int num = pCryptoService.Key.Length;
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			else
			{
				if (text.Length < num)
				{
					text = text.PadRight(num, ' ');
				}
			}
			return Encoding.ASCII.GetBytes(text);
		}
		public string Decrypting(string Source, string Key)
		{
			string text = string.Empty;
			try
			{
				byte[] array = Convert.FromBase64String(Source);
				Stream arg_3A_0 = new MemoryStream(array, 0, array.Length);
				DESCryptoServiceProvider expr_1C = new DESCryptoServiceProvider();
				byte[] legalKey = FcEncode.GetLegalKey(expr_1C, Key);
				expr_1C.Key = legalKey;
				expr_1C.IV = legalKey;
				ICryptoTransform transform = expr_1C.CreateDecryptor();
				text = new StreamReader(new CryptoStream(arg_3A_0, transform, CryptoStreamMode.Read)).ReadToEnd();
			}
			catch
			{
				string result = Guid.NewGuid().ToString();
				return result;
			}
			if (text.Length > 0)
			{
				string[] array2 = text.Split(new char[]
				{
					','
				});
				byte[] array3 = new byte[array2.Length - 1];
				for (int i = 0; i < array2.Length - 1; i++)
				{
					array3[i] = (byte)Convert.ToInt32(array2[i], 16);
				}
				return Encoding.Unicode.GetString(array3);
			}
			return Guid.NewGuid().ToString();
		}
		public string Encrypting(string Source, string Key)
		{
			string result;
			try
			{
				byte[] arg_11_0 = Encoding.Unicode.GetBytes(Source);
				string text = string.Empty;
				byte[] array = arg_11_0;
				for (int i = 0; i < array.Length; i++)
				{
					byte value = array[i];
					text = text + Convert.ToString(value, 16) + ",";
				}
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				MemoryStream memoryStream = new MemoryStream();
				DESCryptoServiceProvider expr_59 = new DESCryptoServiceProvider();
				byte[] legalKey = FcEncode.GetLegalKey(expr_59, Key);
				expr_59.Key = legalKey;
				expr_59.IV = legalKey;
				ICryptoTransform cryptoTransform = expr_59.CreateEncryptor();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				byte[] buffer = memoryStream.GetBuffer();
				int num = 0;
				while (num < buffer.Length && buffer[num] != 0)
				{
					num++;
				}
				memoryStream.Close();
				cryptoStream.Close();
				cryptoTransform.Dispose();
				result = Convert.ToBase64String(buffer, 0, num);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
