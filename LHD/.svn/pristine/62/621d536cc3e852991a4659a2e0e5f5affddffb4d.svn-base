using System;
namespace KaYi.Utilities
{
	public static class PagerUtility
	{
		public static int GetPageCount(int recordCount, int pageSize)
		{
			int num = recordCount / pageSize;
			if (recordCount % pageSize != 0)
			{
				num++;
			}
			return num;
		}
	}
}
