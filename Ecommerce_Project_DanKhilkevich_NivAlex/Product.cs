using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            this.ProductId = GetNextProductId(); // assign ID using static method

            this.ProductName = product_name; // Use property to validate
            this.ProductPrice = product_price; // Use property to validate
            this.CategoryOfProduct = category_of_product; // Use property to validate
        }

        public Product(Product other) // Copy constructor
        {
            this.ProductId = GetNextProductId(); // Assign ID using static method
            this.ProductName = other.ProductName;
            this.ProductPrice = other.ProductPrice;
            this.CategoryOfProduct = other.CategoryOfProduct;
        }

        public static int GetNextProductId()
        {
            return ++product_id_number;
        }

        public int ProductId
        {
            get { return product_id; }
            private set { product_id = value; }
        }

        public string ProductName
        {
            get { return product_name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Product name cannot be null or empty.");
                }
                product_name = value;
            }
        }

        public int ProductPrice
        {
            get { return product_price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Product price cannot be negative.");
                }
                product_price = value;
            }
        }

        public ProductCategory CategoryOfProduct
        {
            get { return category_of_product; }
            set
            {
                if (!Enum.IsDefined(typeof(ProductCategory), value))
                {
                    throw new ArgumentException("Invalid product category.");
                }
                category_of_product = value;
            }
        }

        public override bool Equals(object obj)
        {
            // Check if the object is null or not of the correct type
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Casting the object to a Product
            Product other = (Product)obj;

            // Compare the product's name, category, and price
            return ProductName == other.ProductName &&
                   ProductPrice == other.ProductPrice &&
                   CategoryOfProduct == other.CategoryOfProduct;
        }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }

        public override string ToString()
        {
            return $"Product Type: {GetType().Name}\nProduct ID: {ProductId}\nProduct Name: {ProductName}\nProduct Price: {ProductPrice}\nCategory of Product: {CategoryOfProduct}";
        }
    }

}
