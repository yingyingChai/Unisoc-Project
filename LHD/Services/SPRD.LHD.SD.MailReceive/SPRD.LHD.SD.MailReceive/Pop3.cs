using System.Net.Sockets;
using System.Collections;
using System.IO;
using System.Net;
using System;
using System.Net.Mail;

public class POP3
{
    string POPServer;
    string user;
    string pwd;
    NetworkStream ns;
    StreamReader sr;
    static int count;
    //调用Srfile类，写入文件内容和文件名,文件格式：.txt 读邮件信息
    // Srfile  Filesr = new Srfile(); 
    //对邮件标题 base64 解码
    /// <summary>
    /// base64 解码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private string DecodeBase64(string code)  //string code_type,
    {
        string decode = "";
        string st = code + "000";//
        string strcode = st.Substring(0, (st.Length / 4) * 4);
        byte[] bytes = Convert.FromBase64String(strcode);
        try
        {
            decode = System.Text.Encoding.GetEncoding("GBK").GetString(bytes);
        }
        catch
        {
            decode = code;
        }
        return decode;
    }

    //对邮件标题解码  quoted-printable
    /// <summary>
    ///  quoted-printable  解码 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private string DecodeQ(string code)
    {

        string[] textArray1 = code.Split(new char[] { '=' });
        byte[] buf = new byte[textArray1.Length];
        try
        {

            for (int i = 0; i < textArray1.Length; i++)
            {
                if (textArray1[i].Trim() != string.Empty)
                {

                    byte[] buftest = new byte[2];

                    buf[i] = (byte)int.Parse(textArray1[i].Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
        }
        catch
        {
            return null;
        }
        return System.Text.Encoding.Default.GetString(buf);
    }

    public POP3() { }
    #region
    /// <summary>
    /// 接收邮件服务器相关信息
    /// </summary>
    /// <param name="server">参数 pop邮件服务器地址  </param>
    /// <param name="user">参数 登录到pop邮件服务器的用户名  </param>
    /// <param name="pwd">参数  登录到pop邮件服务器的密码</param>
    /// <returns>无返回</returns>
    public POP3(string server, string _user, string _pwd)
    {
        POPServer = server;
        user = _user;
        pwd = _pwd;
    }
    #endregion
    //登陆服务器
    private void Connect()
    {
        TcpClient sender = new TcpClient(POPServer, 110);
        Byte[] outbytes;
        string input;
        string readuser = string.Empty;
        string readpwd = string.Empty;
        try
        {
            ns = sender.GetStream();
            sr = new StreamReader(ns);
            sr.ReadLine();
            //检查密码
            input = "user " + user + "\r\n";
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);
            readuser = sr.ReadLine();
            input = "pass " + pwd + "\r\n";
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);
            readpwd = sr.ReadLine();
            //Console.WriteLine(sr.ReadLine() );

        }
        catch(Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("用户名或密码错误");
        }
    }
    //为了读到数据流中的正确信息，重新建的一个方法（只是在读邮件详细信息是用到《即GetNewMessages（）方法中用到，这样就可以正常显示邮件正文的中英文》）
    private void Connecttest(TcpClient tcpc)
    {
        Byte[] outbytes;
        string input;
        string readuser = string.Empty;
        string readpwd = string.Empty;
        try
        {
            ns = tcpc.GetStream();
            sr = new StreamReader(ns);
            sr.ReadLine();
            //Console.WriteLine(sr.ReadLine() );
            input = "user " + user + "\r\n";
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);

            readuser = sr.ReadLine();

            input = "pass " + pwd + "\r\n";
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);
            readpwd = sr.ReadLine();
        }
        catch
        {
            System.Windows.Forms.MessageBox.Show("用户名或密码错误");
        }
    }
    //断开与服务器的连接
    private void Disconnect()
    {
        //"quit"  即表示断开连接
        string input = "quit" + "\r\n";
        Byte[] outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
        ns.Write(outbytes, 0, outbytes.Length);
        //关闭数据流
        ns.Close();
    }
    //获得新邮件数目
    # region
    /// <summary>
    /// 获取邮件数目
    /// </summary>
    /// <returns>返回  int  邮件数目</returns>
    public int GetNumberOfNewMessages()
    {
        Byte[] outbytes;
        string input;
        int countmail;
        try
        {
            Connect();
            //"stat"向邮件服务器 表示要取邮件数目
            input = "stat" + "\r\n";
            //将string input转化为7位的字符，以便可以在网络上传输
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);
            string thisResponse = sr.ReadLine();
            if (thisResponse.Substring(0, 4) == "-ERR")
            {
                return -1;
            }
            string[] tmpArray;
            //将从服务器取到的数据以“”分成字符数组
            tmpArray = thisResponse.Split(' ');
            //断开与服务器的连接
            Disconnect();
            //取到的表示邮件数目
            countmail = Convert.ToInt32(tmpArray[1]);
            count = countmail;
            return countmail;
        }
        catch
        {
            Console.WriteLine("Could not connect to mail server");
            return -1;//表示读邮件时  出错，将接收邮件的线程 阻塞5分钟
        }
    }
    # endregion

    //获取邮件
    # region
    /// <summary>
    /// 获取所有新邮件
    /// </summary>
    /// <returns>  返回 ArrayList</returns>
    public ArrayList GetNewMessages()   //public ArrayList  GetNewMessages(string subj)
    {

        bool blag = false;
        int newcount = GetNumberOfNewMessages();
        ArrayList newmsgs = new ArrayList();
        try
        {
            TcpClient tcpc = new TcpClient(POPServer, 110);
            Connecttest(tcpc);
            //   newcount = GetNumberOfNewMessages();

            for (int n = 1; n < newcount + 1; n++)
            {
                ArrayList msglines = GetRawMessage(tcpc, n);
                string msgsubj = GetMessageSubject(msglines).Trim();
                MailMessage msg = new MailMessage();
                msg.Subject = GetReadText(msgsubj);
                
                blag = false;
                //取发邮件者的邮件地址 
                //msg.From = GetMessageFrom(msglines);
                //取邮件正文
                string msgbody = GetMessageBody(msglines);
                string aas = GetMailText(msgbody);
                msg.Body = msgbody;
                newmsgs.Add(msg);

                //将收到的邮件保存到本地，调用另一个类的保存邮件方法，不使用此功能
                //    Filesr.Savefile("subject:"+msg.Subject+"\r\n"+"sender:"+msg.From+"\r\n"+"context:"+msg.Body,"mail"+n+".txt");
                //删除邮件，不使用
                //       DeleteMessage(n);
            }
            //断开与服务器的连接
            Disconnect();
            return newmsgs;
        }
        catch
        {
            Console.WriteLine("读取邮件出错，请重试");
            return newmsgs;
        }
    }
    #endregion
    //从服务器读邮件信息
    private ArrayList GetRawMessage(TcpClient tcpc, int messagenumber)
    {
        Byte[] outbytes;
        string input;
        string line = "";
        input = "retr " + messagenumber.ToString() + "\r\n";
        outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
        ns.Write(outbytes, 0, outbytes.Length);
        ArrayList msglines = new ArrayList();
        StreamReader srtext;
        srtext = new StreamReader(tcpc.GetStream(), System.Text.Encoding.Default);
        //每份邮件以英文“.”结束
        do
        {
            line = srtext.ReadLine();
            msglines.Add(line);
        } while (line != ".");
        msglines.RemoveAt(msglines.Count - 1);
        return msglines;
    }
    //获取邮件标题
    private string GetMessageSubject(ArrayList msglines)
    {
        IEnumerator msgenum = msglines.GetEnumerator();
        while (msgenum.MoveNext())
        {
            string line = (string)msgenum.Current;
            if (line.StartsWith("Subject:"))
            {
                return line.Substring(8, line.Length - 8);
            }
        }
        return "None";
    }
    //获取邮件的发送人地址
    private string GetMessageFrom(ArrayList msglines)
    {
        string[] tokens;
        IEnumerator msgenum = msglines.GetEnumerator();
        while (msgenum.MoveNext())
        {
            string line = (string)msgenum.Current;
            if (line.StartsWith("From"))
            {
                tokens = line.Split(new Char[] { ':' });
                return tokens[1].Trim(new Char[] { '<', '>', ' ' });
            }
        }
        return "None";
    }
    //邮件正文
    private string GetMessageBody(ArrayList msglines)
    {
        string body = "";
        string line = " ";
        IEnumerator msgenum = msglines.GetEnumerator();
        while (line.CompareTo("") != 0)
        {
            msgenum.MoveNext();
            line = (string)msgenum.Current;
        }
        while (msgenum.MoveNext())
        {
            body = body + (string)msgenum.Current + "\r\n";
        }
        return body;
    }
    //删除第几封邮件
    #region
    /// <summary>
    ///根据输入的数字，删除相应编号的邮件
    /// </summary>
    /// <param name="messagenumber">参数 删除第几封邮件  </param>
    /// <returns>返回  bool true成功；false  失败</returns>
    public bool DeleteMessage(int messagenumber)
    {
        Connect();
        Byte[] outbytes;
        string input;
        byte[] backmsg = new byte[25];
        string msg = string.Empty;

        try
        {
            input = "dele " + messagenumber.ToString() + "\r\n";
            outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
            ns.Write(outbytes, 0, outbytes.Length);
            ns.Read(backmsg, 0, 25);
            msg = System.Text.Encoding.Default.GetString(backmsg, 0, backmsg.Length);
            Disconnect();
            if (msg.Substring(0, 3) == "+OK")
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    public string GetReadText(string p_Text)
    {
        System.Text.RegularExpressions.Regex _Regex = new System.Text.RegularExpressions.Regex(@"(?<=\=\?).*?(\?\=)+");
        System.Text.RegularExpressions.MatchCollection _Collection = _Regex.Matches(p_Text);
        string _Text = p_Text;
        foreach (System.Text.RegularExpressions.Match _Match in _Collection)
        {
            string _Value = "=?" + _Match.Value;
            if (_Value[0] == '=')
            {
                string[] _BaseData = _Value.Split('?');
                if (_BaseData.Length == 5)
                {
                    System.Text.Encoding _Coding = System.Text.Encoding.GetEncoding(_BaseData[1]);
                    _Text = _Text.Replace(_Value, _Coding.GetString(Convert.FromBase64String(_BaseData[3])));
                }
            }
            else
            {
            }
        }
        return _Text;
    }

    #region 获取邮件正文 和 附件
    /// <summary> 
    /// 获取文字主体 
    /// </summary> 
    /// <param name="p_Mail"></param> 
    /// <returns></returns> 
    public string GetMailText(string p_Mail)
    {
        string _ConvertType = GetTextType(p_Mail, "\r\nContent-Type: ", ";");
        if (_ConvertType.Length == 0)
        {
            _ConvertType = GetTextType(p_Mail, "\r\nContent-Type: ", "\r");
        }
        string returnValue = "";
        int _StarIndex = -1;
        int _EndIndex = -1;
        string _ReturnText = "";
        string _Transfer = "";
        string _Boundary = "";
        string _EncodingName = GetTextType(p_Mail, "charset=\"", "\"").Replace("\"", "");
        System.Text.Encoding _Encoding = System.Text.Encoding.Default;
        if (_EncodingName != "") _Encoding = System.Text.Encoding.GetEncoding(_EncodingName);
        switch (_ConvertType)
        {
            case "text/html;":
                _Transfer = GetTextType(p_Mail, "\r\nContent-Transfer-Encoding: ", "\r\n").Trim();
                _StarIndex = p_Mail.IndexOf("\r\n\r\n");
                if (_StarIndex != -1) _ReturnText = p_Mail.Substring(_StarIndex, p_Mail.Length - _StarIndex);
                switch (_Transfer)
                {
                    case "8bit":

                        break;
                    case "quoted-printable":
                        _ReturnText = DecodeQuotedPrintable(_ReturnText, _Encoding);
                        break;
                    case "base64":
                        _ReturnText = DecodeBase64(_ReturnText, _Encoding);
                        break;
                }
                //MailTable.Rows.Add(new object[] { "text/html", _ReturnText });
                returnValue = _ReturnText;
                break;
            case "text/plain;":
                _Transfer = GetTextType(p_Mail, "\r\nContent-Transfer-Encoding: ", "\r\n").Trim();
                _StarIndex = p_Mail.IndexOf("\r\n\r\n");
                if (_StarIndex != -1) _ReturnText = p_Mail.Substring(_StarIndex, p_Mail.Length - _StarIndex);
                switch (_Transfer)
                {
                    case "8bit":

                        break;
                    case "quoted-printable":
                        _ReturnText = DecodeQuotedPrintable(_ReturnText, _Encoding);
                        break;
                    case "base64":
                        _ReturnText = DecodeBase64(_ReturnText, _Encoding);
                        break;
                }
                //MailTable.Rows.Add(new object[] { "text/plain", _ReturnText });
                returnValue = _ReturnText;
                break;
            //case "multipart/alternative;":
            //    _Boundary = GetTextType(p_Mail, "boundary=\"", "\"").Replace("\"", "");
            //    _StarIndex = p_Mail.IndexOf("--" + _Boundary + "\r\n");
            //    if (_StarIndex == -1) return;
            //    while (true)
            //    {
            //        _EndIndex = p_Mail.IndexOf("--" + _Boundary, _StarIndex + _Boundary.Length);
            //        if (_EndIndex == -1) break;
            //        GetMailText(p_Mail.Substring(_StarIndex, _EndIndex - _StarIndex));
            //        _StarIndex = _EndIndex;
            //    }
            //    break;
            //case "multipart/mixed;":
            //    _Boundary = GetTextType(p_Mail, "boundary=\"", "\"").Replace("\"", "");
            //    _StarIndex = p_Mail.IndexOf("--" + _Boundary + "\r\n");
            //    if (_StarIndex == -1) return;
            //    while (true)
            //    {
            //        _EndIndex = p_Mail.IndexOf("--" + _Boundary, _StarIndex + _Boundary.Length);
            //        if (_EndIndex == -1) break;
            //        GetMailText(p_Mail.Substring(_StarIndex, _EndIndex - _StarIndex));
            //        _StarIndex = _EndIndex;
            //    }
            //    break;
            //default:
            //    if (_ConvertType.IndexOf("application/") == 0)
            //    {
            //        _StarIndex = p_Mail.IndexOf("\r\n\r\n");
            //        if (_StarIndex != -1) _ReturnText = p_Mail.Substring(_StarIndex, p_Mail.Length - _StarIndex);
            //        _Transfer = GetTextType(p_Mail, "\r\nContent-Transfer-Encoding: ", "\r\n").Trim();
            //        string _Name = GetTextType(p_Mail, "filename=\"", "\"").Replace("\"", "");
            //        _Name = GetReadText(_Name);
            //        byte[] _FileBytes = new byte[0];
            //        switch (_Transfer)
            //        {
            //            case "base64":
            //                _FileBytes = Convert.FromBase64String(_ReturnText);
            //                break;
            //        }
            //        MailTable.Rows.Add(new object[] { "application/octet-stream", _FileBytes, _Name });

            //    }
            //    break;
        }
        return returnValue;
    }

    /// <summary> 
    /// 获取类型（正则） 
    /// </summary> 
    /// <param name="p_Mail">原始文字</param> 
    /// <param name="p_TypeText">前文字</param> 
    /// <param name="p_End">结束文字</param> 
    /// <returns>符合的记录</returns> 
    public string GetTextType(string p_Mail, string p_TypeText, string p_End)
    {
        System.Text.RegularExpressions.Regex _Regex = new System.Text.RegularExpressions.Regex(@"(?<=" + p_TypeText + ").*?(" + p_End + ")+");
        System.Text.RegularExpressions.MatchCollection _Collection = _Regex.Matches(p_Mail);
        if (_Collection.Count == 0) return "";
        return _Collection[0].Value;
    }

    /// <summary> 
    /// QuotedPrintable编码接码 
    /// </summary> 
    /// <param name="p_Text">原始文字</param> 
    /// <param name="p_Encoding">编码方式</param> 
    /// <returns>接码后信息</returns> 
    public string DecodeQuotedPrintable(string p_Text, System.Text.Encoding p_Encoding)
    {
        System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
        char[] _CharValue = p_Text.ToCharArray();
        for (int i = 0; i != _CharValue.Length; i++)
        {
            switch (_CharValue[i])
            {
                case '=':
                    if (_CharValue[i + 1] == '\r' || _CharValue[i + 1] == '\n')
                    {
                        i += 2;
                    }
                    else
                    {
                        try
                        {
                            _Stream.WriteByte(Convert.ToByte(_CharValue[i + 1].ToString() + _CharValue[i + 2].ToString(), 16));
                            i += 2;
                        }
                        catch
                        {
                            _Stream.WriteByte(Convert.ToByte(_CharValue[i]));
                        }
                    }
                    break;
                default:
                    _Stream.WriteByte(Convert.ToByte(_CharValue[i]));
                    break;
            }
        }
        return p_Encoding.GetString(_Stream.ToArray());
    }

    /// <summary> 
    /// 解码BASE64 
    /// </summary> 
    /// <param name="p_Text"></param> 
    /// <param name="p_Encoding"></param> 
    /// <returns></returns> 
    public string DecodeBase64(string p_Text, System.Text.Encoding p_Encoding)
    {
        if (p_Text.Trim().Length == 0) return "";
        byte[] _ValueBytes = Convert.FromBase64String(p_Text);
        return p_Encoding.GetString(_ValueBytes);
    }
    #endregion
}
