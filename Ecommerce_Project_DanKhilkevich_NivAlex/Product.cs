using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Product
    {
        private string product_name;
        private int product_price;
        private bool is_special_product;
        private int packaging_fee;
        private string catagory_of_product;
        private string[] list_catagory = { "kids", "electricity", "office", "clothing" };

        public Product(string product_name, int product_price, bool is_special_product,int packaging_fee,string catagory_of_product) //product constructor
        {
            while (true)
            {
                if (SetProductName(product_name) == true) { break; }
                Console.WriteLine("please reenter product name: ");
                product_name = Console.ReadLine();
            }

            while (true)
            {
                if (SetProductPrice(product_price) == true) { break; }
                Console.WriteLine("please reenter product price: ");
                product_price = int.Parse(Console.ReadLine());
            }

            while (true)
            {
                if (SetPackagingFee(packaging_fee) == true) { break; }
                Console.WriteLine("please reenter packaging_fee: ");
                packaging_fee = int.Parse(Console.ReadLine());
            }

            while (true)
            {
                if (SetIsSpecialProduct(is_special_product) == true) { break; }
                Console.WriteLine("please reenter true or false: ");
                is_special_product = bool.Parse(Console.ReadLine());
            }

            while (true)
            {
                if (SetCatagoryOfProduct(catagory_of_product) == true) { break; }
                Console.WriteLine("please reenter catagory: ");
                catagory_of_product = (Console.ReadLine());
            }


        }

        public Product(Product other) // copy constructor
        {
            this.product_name = other.product_name;
            this.product_price = other.product_price;
            this.is_special_product=other.is_special_product;
            this.packaging_fee = other.packaging_fee;
            this.catagory_of_product = other.catagory_of_product;
        }


        public bool SetProductName(string product_name)
        {
            this.product_name = product_name;
            return true;
        }

        public bool SetProductPrice(int product_price)
        {
            this.product_price = product_price; 
            return true;
        }

        public bool SetIsSpecialProduct(bool is_special_product)
        {
            this.is_special_product = is_special_product;
            return true;
        }

        public bool SetPackagingFee(int packaging_fee)
        {
            if(is_special_product == true)
            {
                this.packaging_fee = packaging_fee;
                return true;
            }

            this.packaging_fee = packaging_fee;
            return true;
        }

        public bool SetCatagoryOfProduct(string catagory_of_product)
        {
            foreach (string product in list_catagory) 
            {
                if (catagory_of_product.ToLower() == product)
                {
                    this.catagory_of_product = catagory_of_product;
                    return true;
                }
            }

            Console.WriteLine("Catagory doesnt exist, failed to create product , try again");
            return false;
            
        }

        public String GetProductName()
        {
            return this.product_name;
        }

        public int GetProductPrice()
        {
            return this.product_price;
        }

        public bool GetIsSpecialProduct()
        {
            return this.is_special_product;
        }

        public int GetPackagingFee()
        {
            return this.packaging_fee;
        }

        public string GetCatagoryOfProduct()
        {
            return this.catagory_of_product;
        }

        public string PrintProductToString()
        {
            return $"Product Name: {product_name}\nProduct Price: {product_price}\nIs Special Product: {is_special_product}\nPackaging Fee: {packaging_fee}\nCategory of Product: {catagory_of_product}";
        }
    }

}
