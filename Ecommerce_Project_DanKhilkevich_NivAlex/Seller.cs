using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Seller : User
    {
        private Product[] seller_products; //products array, each product can be regular or special (polymorphism principle)
        private int logical_size = 0;
        private int physical_size = 0;

        // seller constructor
        public Seller() : base()
        {
            // Initialize the seller products array with zero items
            seller_products = new Product[0];
        }

        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address) : base(seller_username, seller_password, seller_address)
        {
            seller_products = new Product[0];
        }

        public Seller(Seller other) : base(other.GetUsername(), other.GetPassword(), other.GetAddress()) //copy constructor
        {
            this.seller_products = new Product[other.seller_products.Length];
            Array.Copy(other.seller_products, this.seller_products, other.seller_products.Length);
            this.logical_size = other.logical_size;
        }
        // function to get the list of products
        public Product[] GetSellerProductList()
        {
            Product[] products = new Product[logical_size];
            Array.Copy(seller_products, products, logical_size);
            return products;
        }


        // function to add a product to the seller's product list
        public void AddToProductList(Product product)
        {
            // Check if the product already exists in the seller's product list
            foreach (Product existingProduct in seller_products)
            {
                if (existingProduct != null && existingProduct.Equals(product))
                {
                    Console.WriteLine("Product already exists in the seller's product list.");
                    return;
                }
            }
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
            Console.WriteLine("Product added successfully to the seller.");
        }

        public bool SearchProductIfItExists(string name_of_product_to_find)
        {
            if (seller_products == null)
            {
                return false;
            }

            foreach (Product product in seller_products)
            {
                if (product != null && product.GetProductName() == name_of_product_to_find)
                {
                    return true;
                }
            }
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
            Console.WriteLine(ToString());
            Console.WriteLine($"Logical Size: {logical_size}, Physical Size: {physical_size}");
            if (logical_size == 0)
            {
                Console.WriteLine("Seller has no products.");
                return;
            }
            
            Console.WriteLine("Seller Products:");
            for (int i = 0; i < logical_size; i++)
            {
                Console.WriteLine($"{seller_products[i].ToString()}");
                Console.WriteLine("-------------------");
            }
        }

    }
}
