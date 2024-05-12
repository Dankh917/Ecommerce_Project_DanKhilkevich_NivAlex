using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Buyer
    {
        private string buyer_username;
        private string buyer_password;
        private Address buyer_address;
        private Product[] shopping_cart;
        private Order[] past_purchases;
        private int logi_size = 0;
        

        public Buyer(string buyer_username, string buyer_password, Address buyer_address) 
        {
            while (true)
            {
                if (SetBuyerUsername(buyer_username) == true) { break; }
                Console.WriteLine("please reenter buyer name: ");
                buyer_username = Console.ReadLine();
            }

            while (true)
            {
                if (SetBuyerPassword(buyer_password) == true) { break; }
                Console.WriteLine("please reenter buyer password: ");
                buyer_password = Console.ReadLine();
            }

            while (true)
            {
                if (SetBuyerAddress(buyer_address) == true) { break; }
                break;
            }

            this.shopping_cart = new Product[1];
            this.past_purchases = new Order[1];
        }

        public Buyer(Buyer other)
        {
            this.buyer_username = other.buyer_username;
            this.buyer_password = other.buyer_password;
            this.buyer_address = other.buyer_address;
            this.shopping_cart = other.shopping_cart;
            this.past_purchases = other.past_purchases;
        }

        public bool SetBuyerUsername(string buyer_username)
        {
            this.buyer_username = buyer_username;
            return true;
        }

        public bool SetBuyerPassword(string buyer_password)
        {
            this.buyer_password = buyer_password;
            return true;
        }

        public bool SetBuyerAddress(Address buyer_address)
        {
            this.buyer_address = buyer_address;
            return true;
        }

        public string GetBuyerUsername()
        {
            return this.buyer_username;
        }

        public string GetBuyerpassword()
        {
            return this.buyer_password;
        }

        public Address GetBuyerAddress()
        {
            return this.buyer_address;
        }

        public Product[] GetShoppingCart()
        {
            return this.shopping_cart;
        }

        public Order[] GetPastPurchases()
        {
            return this.past_purchases;
        }

        public void AddProductToShoppingCart(Product product)
        {
            if (logi_size < this.shopping_cart.Length)
            {
                this.shopping_cart[logi_size] = product;
                logi_size++;
            }
            else
            {
                Product[] temp = new Product[this.shopping_cart.Length * 2];
                shopping_cart.CopyTo(temp, 0);
                temp[logi_size] = product;
                logi_size++;
                this.shopping_cart = temp;
                Console.WriteLine("increasing size...");
            }

        }

        public void BuyTheShoppingCart()
        {
            Order currorder = new Order();

            for (int i = 0; i < this.shopping_cart.Length; i++)
            {
                //currorder. im cooked asf my boi omwtfyb
            } 
        }    



    }   

}
