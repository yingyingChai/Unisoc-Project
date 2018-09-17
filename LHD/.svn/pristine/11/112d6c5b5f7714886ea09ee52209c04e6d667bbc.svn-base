using System;
namespace KaYi.Utilities
{
	public static class RandomPassword
	{
		public static string GetRandomEasyPassword(int length)
		{
			return RandomPassword.getPassword("ABCDEFGHIJKLMNOPQRSTUVWXYZ12367890", length);
		}
		public static string GetRandomStrongPassword(int length)
		{
			return RandomPassword.getPassword("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ12367890!@#$%^&*()", length);
		}
		private static string getPassword(string chars, int length)
		{
			string text = string.Empty;
			Random random = new Random();
			for (int i = 0; i < length; i++)
			{
				int index = random.Next(chars.Length);
				text += chars[index].ToString();
			}
			return text;
		}
	}
}
