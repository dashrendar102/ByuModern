using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authentication
{
    public class InvalidCredentialsException : System.Exception
    {
        private String netID;
        private String password;

        public InvalidCredentialsException(String netID, String password) : base()
        {
            this.netID = netID;
            this.password = password;
        }

        public InvalidCredentialsException(String netID, String password, String message) : base(message)
        {
            this.netID = netID;
            this.password = password;
        }

        public String NetID
        {
            get
            {
                return netID;
            }
        }

        public String Password
        {
            get
            {
                return password;
            }
        }
    }
}
