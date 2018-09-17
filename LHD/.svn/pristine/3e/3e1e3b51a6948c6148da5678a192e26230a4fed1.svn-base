using System;
using System.Text;
using System.Text.RegularExpressions;
namespace KaYi.Utilities
{
	public class SearchEngineHelper
	{
		private string[][] _Enginers = new string[][]
		{
			new string[]
			{
				"google",
				"utf8",
				"q"
			},
			new string[]
			{
				"baidu",
				"gb2312",
				"wd"
			},
			new string[]
			{
				"yahoo",
				"utf8",
				"p"
			},
			new string[]
			{
				"yisou",
				"utf8",
				"search"
			},
			new string[]
			{
				"live",
				"utf8",
				"q"
			},
			new string[]
			{
				"tom",
				"gb2312",
				"word"
			},
			new string[]
			{
				"163",
				"gb2312",
				"q"
			},
			new string[]
			{
				"iask",
				"gb2312",
				"k"
			},
			new string[]
			{
				"soso",
				"gb2312",
				"w"
			},
			new string[]
			{
				"sogou",
				"gb2312",
				"query"
			},
			new string[]
			{
				"zhongsou",
				"gb2312",
				"w"
			},
			new string[]
			{
				"3721",
				"gb2312",
				"p"
			},
			new string[]
			{
				"openfind",
				"utf8",
				"q"
			},
			new string[]
			{
				"alltheweb",
				"utf8",
				"q"
			},
			new string[]
			{
				"lycos",
				"utf8",
				"query"
			},
			new string[]
			{
				"onseek",
				"utf8",
				"q"
			},
			new string[]
			{
				"mybu",
				"gb2312",
				"search"
			}
		};
		private string _EngineName = "";
		private string _Coding = "utf8";
		private string _RegexWord = "";
		private string _Regex = "(";
		public string EngineName
		{
			get
			{
				return this._EngineName;
			}
		}
		public string Coding
		{
			get
			{
				return this._Coding;
			}
		}
		public string RegexWord
		{
			get
			{
				return this._RegexWord;
			}
		}
		public void EngineRegEx(string myString)
		{
			int i = 0;
			int num = this._Enginers.Length;
			while (i < num)
			{
				if (myString.Contains(this._Enginers[i][0]))
				{
					this._EngineName = this._Enginers[i][0];
					this._Coding = this._Enginers[i][1];
					this._RegexWord = this._Enginers[i][2];
					this._Regex = string.Concat(new string[]
					{
						this._Regex,
						this._EngineName,
						"\\.+.*[?/&]",
						this._RegexWord,
						"[=:])(?<key>[^&]*)"
					});
					return;
				}
				i++;
			}
		}
		public string SearchKey(string myString)
		{
			this.EngineRegEx(myString.ToLower());
			if (this._EngineName != "")
			{
				myString = new Regex(this._Regex, RegexOptions.IgnoreCase).Match(myString).Groups["key"].Value;
				myString = myString.Replace("+", " ");
				if (this._Coding == "gb2312")
				{
					myString = this.GetUTF8String(myString);
				}
				else
				{
					myString = Uri.UnescapeDataString(myString);
				}
			}
			return myString;
		}
		public string GetUTF8String(string myString)
		{
			MatchCollection matchCollection = new Regex("(?<key>%..%..)", RegexOptions.IgnoreCase).Matches(myString);
			int i = 0;
			int count = matchCollection.Count;
			while (i < count)
			{
				string text = matchCollection[i].Groups["key"].Value.ToString();
				myString = myString.Replace(text, this.GB2312ToUTF8(text));
				i++;
			}
			return myString;
		}
		public string GB2312ToUTF8(string myString)
		{
			string[] array = myString.Split(new char[]
			{
				'%'
			});
			byte[] array2 = new byte[]
			{
				Convert.ToByte(array[1], 16),
				Convert.ToByte(array[2], 16)
			};
			Encoding arg_45_0 = Encoding.GetEncoding("GB2312");
			Encoding uTF = Encoding.UTF8;
			array2 = Encoding.Convert(arg_45_0, uTF, array2);
			char[] array3 = new char[uTF.GetCharCount(array2, 0, array2.Length)];
			uTF.GetChars(array2, 0, array2.Length, array3, 0);
			return new string(array3);
		}
		public string isCrawler(string SystemInfo)
		{
			string[] array = new string[]
			{
				"Google",
				"Baidu",
				"MSN",
				"Yahoo",
				"TMCrawler",
				"iask",
				"Sogou",
				"3721",
				"cn.msn",
				"zhongsou",
				"sohu",
				"mybu",
				"soso"
			};
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (SystemInfo.ToLower().Contains(text.ToLower()))
				{
					return text;
				}
			}
			return null;
		}
		public bool IsSearchEnginesGet(string str)
		{
			string[] array = new string[]
			{
				"Google",
				"Baidu",
				"MSN",
				"Yahoo",
				"TMCrawler",
				"iask",
				"Sogou",
				"3721",
				"cn.msn",
				"zhongsou",
				"sohu",
				"mybu",
				"soso"
			};
			str = str.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (str.IndexOf(array[i].ToLower()) >= 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
