using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Seller
    {
        private string seller_username;
        private string seller_password;
        private Address seller_address;
        private Product[] seller_products;
        private int logi_size = 0;

        public bool SetSellerUsername(string seller_username)
        {
            this.seller_username = seller_username;
            return true;
        }

        public bool SetSellerPassword(string seller_password)
        {
            this.seller_password = seller_password;
            return true;
        }

        public bool SetSellerAddress(Address seller_address)
        {
            this.seller_address = seller_address;
            return true;
        }

        public string GetSellerUsername()
        {
            return seller_username;
        }

        public string GetSellerPassword()
        {
            return seller_password;
        }

        public Address GetSellerAddress()
        {
            return seller_address;
        }

        public void AddToProductList(Product product)
        {
            if (logi_size < this.seller_products.Length)
            {
                this.seller_products[logi_size] = product;
                logi_size++;
            }
            else
            {
                Product[] temp = new Product[this.seller_products.Length * 2];
                seller_products.CopyTo(temp, 0);
                temp[logi_size] = product;
                logi_size++;
                this.seller_products = temp;
            }
        }
        
    }
}
