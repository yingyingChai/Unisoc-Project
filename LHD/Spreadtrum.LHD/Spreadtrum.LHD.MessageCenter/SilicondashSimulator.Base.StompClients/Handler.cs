namespace SilicondashSimulator.Base.StompClients
{
    using System;

    public class Handler
    {
        private string _password;
        private string _uri;
        private string _username;

        public Handler()
        {
        }

        public Handler(string uri, string username, string password)
        {
            this._uri = uri;
            this._username = username;
            this._password = password;
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string URI
        {
            get
            {
                return this._uri;
            }
            set
            {
                this._uri = value;
            }
        }

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

