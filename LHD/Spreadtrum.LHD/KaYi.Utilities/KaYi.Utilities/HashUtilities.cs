using System;
using System.Security.Cryptography;
using System.Text;
namespace KaYi.Utilities
{
	public static class HashUtilities
	{
		public static string SHA1_Hash(string str_sha1_in)
		{
			HashAlgorithm arg_12_0 = new SHA1CryptoServiceProvider();
			byte[] bytes = Encoding.Default.GetBytes(str_sha1_in);
			return BitConverter.ToString(arg_12_0.ComputeHash(bytes)).Replace("-", "").ToLower();
		}
	}
}
