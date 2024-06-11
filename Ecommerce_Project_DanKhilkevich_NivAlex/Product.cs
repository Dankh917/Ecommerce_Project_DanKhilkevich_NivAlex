using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{

    internal class Product
    {
        private static int product_id_number = 0; // static property for the product IDs
        private int product_id;
        private string product_name;
        private int product_price;
        private ProductCategory category_of_product; // Use enum for category

        public enum ProductCategory
        {
            Kids = 1,
            Electricity = 2,
            Office = 3,
            Clothing = 4
        }

        public Product(string product_name, int product_price, ProductCategory category_of_product) // Product constructor
        {
            this.product_id = GetNextProductId(); // assign ID using static method

            while (true)
            {
                if (SetProductName(product_name)) break;
                Console.WriteLine("Please reenter product name: ");
                product_name = Console.ReadLine();
            }

            while (true)
            {
                if (SetProductPrice(product_price)) break;
                Console.WriteLine("Please reenter product price: ");
                product_price = int.Parse(Console.ReadLine());
            }

            while (true)
            {
                if (SetCategoryOfProduct(category_of_product)) break;
                Console.WriteLine("Please reenter category: ");
                category_of_product = (ProductCategory)Enum.Parse(typeof(ProductCategory), Console.ReadLine(), true);
            }
        }

        public Product(Product other) // Copy constructor
        {
            this.product_id = GetNextProductId(); // Assign ID using static method
            this.product_name = other.product_name;
            this.product_price = other.product_price;
            this.category_of_product = other.category_of_product;
        }

        public bool SetProductName(string product_name)
        {
            this.product_name = product_name;
            //add validation in the future
            return true;
        }

        public bool SetProductPrice(int product_price)
        {
            this.product_price = product_price;
            //add validation in the future
            return true;
        }

        public bool SetCategoryOfProduct(ProductCategory category_of_product)
        {
            this.category_of_product = category_of_product;
            //add validation in the future
            return true;
        }
        public static int GetNextProductId()
        {
            return ++product_id_number;
        }
        public string GetProductName()
        {
            return this.product_name;
        }

        public int GetProductPrice()
        {
            return this.product_price;
        }

        public ProductCategory GetCategoryOfProduct()
        {
            return this.category_of_product;
        }

        public int GetProductId()
        {
            return this.product_id;
        }
        public override bool Equals(object obj)
        {
            // check if the object is null or not of the correct type
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // casting the object to a Product
            Product other = (Product)obj;

            // compare the product's name, category, and price
            return product_name == other.product_name &&
                   product_price == other.product_price &&
                   category_of_product == other.category_of_product;
        }
        public override int GetHashCode()
        {
            return product_id.GetHashCode();
        }
        public override string ToString()
        {
            return $"Product Type: {GetType().Name}\nProduct ID: {product_id}\nProduct Name: {product_name}\nProduct Price: {product_price}\nCategory of Product: {category_of_product}";
            
        }
    }

}
