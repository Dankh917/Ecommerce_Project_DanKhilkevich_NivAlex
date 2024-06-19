using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class User
    {
        protected string username;
        protected string password;
        protected Address address;

        public User() { }

        public User(string username, string password, Address address)
        {
            SetUsername(username);
            SetPassword(password);
            SetAddress(address);
        }
        public bool SetUsername(string username)
        {
            this.username = username;
            // the Adding validation will be here in the future
            return true;
        }

        public bool SetPassword(string password)
        {
            this.password = password;
            // the Adding validation will be here in the future
            return true;
        }

        public bool SetAddress(Address address)
        {
            this.address = address;
            // the Adding validation will be here in the future
            return true; 
        }

        public string GetUsername()
        {
            return username;
        }

        public string GetPassword()
        {
            return password;
        }

        public Address GetAddress()
        {
            return address;
        }
        public override string ToString()
        {
            return $"Username: {username}, Address: {address}";
        }
    }
}
