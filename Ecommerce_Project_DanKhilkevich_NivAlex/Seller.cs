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
        private int logical_size = 0;
        private int physical_size = 0;

        // Default constructor
        public Seller()
        {
            // Initialize the seller products array with zero items
            seller_products = new Product[0];
        }
        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address)
        {
            SetSellerUsername(seller_username);
            SetSellerPassword(seller_password);
            SetSellerAddress(seller_address);
            // Initialize the seller products array with zero items
            seller_products = new Product[0];
        }
        public Seller(Seller other)
        {
            this.seller_username = other.seller_username;
            this.seller_password = other.seller_password;
            // Creating a new Address object to avoid reference sharing
            this.seller_address = new Address(other.seller_address);
            // Copying the products array
            this.seller_products = new Product[other.seller_products.Length];
            Array.Copy(other.seller_products, this.seller_products, other.seller_products.Length);
            this.logical_size = other.logical_size;
        }

        // Method to set the seller username
        public bool SetSellerUsername(string seller_username)
        {
            this.seller_username = seller_username;
            return true; // Consider adding validation logic in the future
        }

        // Method to set the seller password
        public bool SetSellerPassword(string seller_password)
        {
            this.seller_password = seller_password;
            return true; // Consider adding validation logic in the future
        }

        // Method to set the seller address
        public bool SetSellerAddress(Address seller_address)
        {
            this.seller_address = seller_address;
            return true; // Consider adding validation logic in the future
        }

        // Method to get the seller username
        public string GetSellerUsername()
        {
            return seller_username;
        }

        // Method to get the seller password
        public string GetSellerPassword()
        {
            return seller_password;
        }

        // Method to get the seller address
        public Address GetSellerAddress()
        {
            return seller_address;
        }

        // Method to add a product to the seller's product list
        public void AddToProductList(Product product)
        {
            if (physical_size == 0)
            {
                seller_products = new Product[1];
                physical_size = 1;
            }
            // Check if the logical size exceeds the length of the product list
            if (logical_size == physical_size)
            {
                // Double the size of the product list
                int newLength = physical_size * 2;
                Product[] temp = new Product[newLength];
                Array.Copy(seller_products, temp, logical_size);
                seller_products = temp;
                physical_size = newLength;
            }

            // Add the product to the end of the product list
            seller_products[logical_size++] = product;
        }
        public bool SearchProductIfItExists(string name_of_product_to_find)
        {
            foreach (Product product in seller_products)
            {
                // Assuming the Product class has a method to check if two products are the same
                if (product.GetProductName()== name_of_product_to_find)
                {
                    // Return the product name if it exists in the seller's products array
                    return true;
                }
            }
            // Return fasle if the product does not exist in the seller's products array
            return false;
        }
        public Product FindProductByName(string productName)
        {
            foreach (Product product in seller_products)
            {
                if (product != null && product.GetProductName().Equals(productName))
                {
                    return product;
                }
            }
            return null; // Product not found
        }
        public void PrintSellerProducts()
        {
            Console.WriteLine($"Seller: {seller_username}");
            Console.WriteLine($"Logical Size: {logical_size}, Physical Size: {physical_size}");

            if (logical_size == 0)
            {
                Console.WriteLine("Seller has no products.");
                return;
            }
            
            Console.WriteLine("Seller Products:");
            for (int i = 0; i < logical_size; i++)
            {
                Console.WriteLine($"Product {i + 1}: {seller_products[i].PrintProduct2String()}");
            }
        }

    }
}
