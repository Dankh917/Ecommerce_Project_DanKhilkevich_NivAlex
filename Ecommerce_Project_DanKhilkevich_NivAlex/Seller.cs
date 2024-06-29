using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Seller : User
    {
        private Product[] seller_products; // products array, each product can be regular or special (polymorphism principle)
        private int logical_size = 0;
        private int physical_size = 0;

        // seller constructor
        public Seller() : base()
        {
            // Initialize the seller products array with zero items
            SellerProducts = new Product[0];
        }

        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address) : base(seller_username, seller_password, seller_address)
        {
            SellerProducts = new Product[0];
        }

        public Seller(Seller other) : base(other.Username, other.Password, other.Address) // copy constructor
        {
            SellerProducts = new Product[other.SellerProducts.Length];
            Array.Copy(other.SellerProducts, SellerProducts, other.SellerProducts.Length);
            LogicalSize = other.LogicalSize;
        }

        public Product[] SellerProducts
        {
            get { return seller_products; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Seller products cannot be null.");
                seller_products = value;
            }
        }

        public int LogicalSize
        {
            get { return logical_size; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Logical size cannot be negative.");
                logical_size = value;
            }
        }

        public int PhysicalSize
        {
            get { return physical_size; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Physical size cannot be negative.");
                physical_size = value;
            }
        }

        // function to get the list of products
        public Product[] GetSellerProductList()
        {
            Product[] products = new Product[LogicalSize];
            Array.Copy(SellerProducts, products, LogicalSize);
            return products;
        }

        // function to add a product to the seller's product list
        public void AddToProductList(Product product)
        {
            // Check if the product already exists in the seller's product list
            foreach (Product existingProduct in SellerProducts)
            {
                if (existingProduct != null && existingProduct.Equals(product))
                {
                    Console.WriteLine("Product already exists in the seller's product list.");
                    return;
                }
            }
            if (PhysicalSize == 0)
            {
                SellerProducts = new Product[1];
                PhysicalSize = 1;
            }
            // Check if the logical size exceeds the length of the product list
            if (LogicalSize == PhysicalSize)
            {
                // Double the size of the product list
                int newLength = PhysicalSize * 2;
                Product[] temp = new Product[newLength];
                Array.Copy(SellerProducts, temp, LogicalSize);
                SellerProducts = temp;
                PhysicalSize = newLength;
            }

            // Add the product to the end of the product list
            SellerProducts[LogicalSize++] = product;
            Console.WriteLine("Product added successfully to the seller.");
        }

        public bool SearchProductIfItExists(string name_of_product_to_find)
        {
            if (SellerProducts == null)
            {
                return false;
            }

            foreach (Product product in SellerProducts)
            {
                if (product != null && product.ProductName == name_of_product_to_find)
                {
                    return true;
                }
            }
            return false;
        }

        public Product FindProductByName(string productName)
        {
            foreach (Product product in SellerProducts)
            {
                if (product != null && product.ProductName.Equals(productName))
                {
                    return product;
                }
            }
            return null; // Product not found
        }

        public void PrintSellerProducts()
        {
            Console.WriteLine(ToString());
            Console.WriteLine($"Logical Size: {LogicalSize}, Physical Size: {PhysicalSize}");
            if (LogicalSize == 0)
            {
                Console.WriteLine("Seller has no products.");
                return;
            }

            Console.WriteLine("Seller Products:");
            for (int i = 0; i < LogicalSize; i++)
            {
                Console.WriteLine($"{SellerProducts[i].ToString()}");
                Console.WriteLine("-------------------");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Seller other = (Seller)obj;

            return Username.Equals(other.Username) &&
                   Password.Equals(other.Password) &&
                   Address.Equals(other.Address) &&
                   SellerProducts.SequenceEqual(other.SellerProducts);
        }

    }
}
