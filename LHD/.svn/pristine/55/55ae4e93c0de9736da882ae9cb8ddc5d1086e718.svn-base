using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace KaYi.Utilities
{
	public class TelnetClient
	{
		private readonly char IAC = Convert.ToChar(255);
		private readonly char DO = Convert.ToChar(253);
		private readonly char DONT = Convert.ToChar(254);
		private readonly char WILL = Convert.ToChar(251);
		private readonly char WONT = Convert.ToChar(252);
		private readonly char SB = Convert.ToChar(250);
		private readonly char SE = Convert.ToChar(240);
		private const char IS = '0';
		private const char SEND = '1';
		private const char INFO = '2';
		private const char VAR = '0';
		private const char VALUE = '1';
		private const char ESC = '2';
		private const char USERVAR = '3';
		private byte[] m_byBuff = new byte[100000];
		private ArrayList m_ListOptions = new ArrayList();
		private string m_strResp;
		private Socket s;
		private IPEndPoint iep;
		private string address;
		private int port;
		private int timeout;
		private string strWorkingData = "";
		private string strFullLog = "";
		private string strWorkingDataX = "";
		public string WorkingData
		{
			get
			{
				return this.strWorkingDataX;
			}
		}
		public string SessionLog
		{
			get
			{
				return this.strFullLog;
			}
		}
		public TelnetClient(string Address, int Port, int CommandTimeout)
		{
			this.address = Address;
			this.port = Port;
			this.timeout = CommandTimeout;
		}
		public bool Connect()
		{
			IPAddress iP = TelnetClient.GetIP(this.address);
			this.s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.iep = new IPEndPoint(iP, this.port);
			bool result;
			try
			{
				this.s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.s.Connect(this.iep);
				AsyncCallback callback = new AsyncCallback(this.OnRecievedData);
				this.s.BeginReceive(this.m_byBuff, 0, this.m_byBuff.Length, SocketFlags.None, callback, this.s);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		private void OnRecievedData(IAsyncResult ar)
		{
			try
			{
				Socket socket = (Socket)ar.AsyncState;
				int num = socket.EndReceive(ar);
				if (num > 0)
				{
					string text = "";
					for (int i = 0; i < num; i++)
					{
						char c = Convert.ToChar(this.m_byBuff[i]);
						if (c != '\n')
						{
							if (c == '\r')
							{
								text += Convert.ToString("\r\n");
							}
							else
							{
								text += Convert.ToString(c);
							}
						}
					}
					try
					{
						int length = text.Length;
						if (length == 0)
						{
							text = Convert.ToString("\r\n");
						}
						byte[] array = new byte[length];
						for (int j = 0; j < length; j++)
						{
							array[j] = Convert.ToByte(text[j]);
						}
						string text2 = this.ProcessOptions(array);
						text2 = this.ConvertToGB2312(text2);
						this.strWorkingDataX = text2;
						if (text2 != "")
						{
							this.strWorkingData = text2;
							this.strFullLog += text2;
						}
						this.RespondToOptions();
						goto IL_120;
					}
					catch (Exception ex)
					{
						throw new Exception("接收数据的时候出错了! " + ex.Message);
					}
				}
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
				IL_120:;
			}
			catch
			{
			}
		}
		private void RespondToOptions()
		{
			try
			{
				for (int i = 0; i < this.m_ListOptions.Count; i++)
				{
					string strOption = (string)this.m_ListOptions[i];
					this.ArrangeReply(strOption);
				}
				this.DispatchMessage(this.m_strResp);
				this.m_strResp = "";
				this.m_ListOptions.Clear();
			}
			catch (Exception ex)
			{
				Console.WriteLine("出错了,在回发数据的时候 " + ex.Message);
			}
		}
		private string ProcessOptions(byte[] m_strLineToProcess)
		{
			string text = "";
			string text2 = "";
			string result = "";
			bool flag = false;
			try
			{
				for (int i = 0; i < m_strLineToProcess.Length; i++)
				{
					char value = Convert.ToChar(m_strLineToProcess[i]);
					text2 += Convert.ToString(value);
				}
				while (!flag)
				{
					int length = text2.Length;
					int num = text2.IndexOf(Convert.ToString(this.IAC));
					if (num > length)
					{
						num = text2.Length;
					}
					if (num != -1)
					{
						text += text2.Substring(0, num);
						char c = text2[num + 1];
						if (c == this.DO || c == this.DONT || c == this.WILL || c == this.WONT)
						{
							string value2 = text2.Substring(num, 3);
							this.m_ListOptions.Add(value2);
							text += text2.Substring(0, num);
							text2 = text2.Substring(num + 3);
						}
						else
						{
							if (c == this.IAC)
							{
								text = text2.Substring(0, num);
								text2 = text2.Substring(num + 1);
							}
							else
							{
								if (c == this.SB)
								{
									text = text2.Substring(0, num);
									int num2 = text2.IndexOf(Convert.ToString(this.SE));
									string value2 = text2.Substring(num, num2);
									this.m_ListOptions.Add(value2);
									text2 = text2.Substring(num2);
								}
							}
						}
					}
					else
					{
						text += text2;
						flag = true;
					}
				}
				result = text;
			}
			catch (Exception ex)
			{
				throw new Exception("解析传入的字符串错误:" + ex.Message);
			}
			return result;
		}
		private static IPAddress GetIP(string import)
		{
			return Dns.GetHostEntry(import).AddressList[0];
		}
		private void ArrangeReply(string strOption)
		{
			try
			{
				bool flag = false;
				if (strOption.Length >= 3)
				{
					char c = strOption[1];
					char c2 = strOption[2];
					if (c2 == '\u0001' || c2 == '\u0003')
					{
						flag = true;
					}
					this.m_strResp += this.IAC.ToString();
					if (flag)
					{
						if (c == this.DO)
						{
							char c3 = this.WILL;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.DONT)
						{
							char c3 = this.WONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.WILL)
						{
							char c3 = this.DO;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.WONT)
						{
							char c3 = this.DONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.SB && strOption[3] == '1')
						{
							char c3 = this.SB;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
							this.m_strResp += "0";
							this.m_strResp += this.IAC.ToString();
							this.m_strResp += this.SE.ToString();
						}
					}
					else
					{
						if (c == this.DO)
						{
							char c3 = this.WONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.DONT)
						{
							char c3 = this.WONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.WILL)
						{
							char c3 = this.DONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
						if (c == this.WONT)
						{
							char c3 = this.DONT;
							this.m_strResp += c3.ToString();
							this.m_strResp += c2.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("解析参数时出错:" + ex.Message);
			}
		}
		private void DispatchMessage(string strText)
		{
			try
			{
				byte[] array = new byte[strText.Length];
				for (int i = 0; i < strText.Length; i++)
				{
					byte b = Convert.ToByte(strText[i]);
					array[i] = b;
				}
				IAsyncResult asyncResult = this.s.BeginSend(array, 0, array.Length, SocketFlags.None, delegate(IAsyncResult ar)
				{
					Socket socket = (Socket)ar.AsyncState;
					if (socket.Connected)
					{
						AsyncCallback callback = new AsyncCallback(this.OnRecievedData);
						socket.BeginReceive(this.m_byBuff, 0, this.m_byBuff.Length, SocketFlags.None, callback, socket);
					}
				}, this.s);
				this.s.EndSend(asyncResult);
			}
			catch (Exception ex)
			{
				Console.WriteLine("出错了,在回发数据的时候:" + ex.Message);
			}
		}
		public int WaitFor(string DataToWaitFor)
		{
			long ticks = DateTime.Now.AddSeconds((double)this.timeout).Ticks;
			while (this.strWorkingData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1)
			{
				if (DateTime.Now.Ticks > ticks)
				{
					throw new Exception("Timed Out waiting for : " + DataToWaitFor);
				}
				Thread.Sleep(1);
			}
			this.strWorkingData = "";
			return 0;
		}
		public void Send(string message)
		{
			this.DispatchMessage(message);
			this.DispatchMessage("\r\n");
		}
		private string ConvertToGB2312(string str_origin)
		{
			char[] array = str_origin.ToCharArray();
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num = (int)array[i];
				array2[i] = (byte)num;
			}
			return Encoding.GetEncoding("GB2312").GetString(array2);
		}
	}
}
