using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Seller : User, IComparable<Seller>
    {
        private List<Product> seller_products; // Using List<Product> 
        private int logical_size = 0;

        // Seller constructor
        public Seller() : base()
        {
            // Initialize the seller products List<Product>
            SellerProducts = new List<Product>();
        }

        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address) : base(seller_username, seller_password, seller_address)
        {
            SellerProducts = new List<Product>();
        }

        public Seller(Seller other) : base(other.Username, other.Password, other.Address) // copy constructor
        {
            SellerProducts = new List<Product>(other.SellerProducts);
            LogicalSize = other.LogicalSize;
        }

        public List<Product> SellerProducts
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

        // Function to get the list of products
        public Product[] GetSellerProductList()
        {
            return SellerProducts.Take(LogicalSize).ToArray();
        }

        // Function to add a product to the seller's product list
        public void AddToProductList(Product product)
        {
            // Check if the product already exists in the seller's product list
            if (SellerProducts.Any(existingProduct => existingProduct != null && existingProduct.Equals(product)))
            {
                Console.WriteLine("Product already exists in the seller's product list.");
                return;
            }

            // Add the product to the seller's product list
            SellerProducts.Add(product);
            LogicalSize++;
            Console.WriteLine("Product added successfully to the seller.");
        }

        public bool SearchProductIfItExists(string name_of_product_to_find)
        {
            return SellerProducts.Any(product => product != null && product.ProductName == name_of_product_to_find);
        }

        public Product FindProductByName(string productName)
        {
            return SellerProducts.FirstOrDefault(product => product != null && product.ProductName.Equals(productName));
        }

        public void PrintSellerProducts()
        {
            Console.WriteLine(ToString());
            Console.WriteLine($"Logical Size: {LogicalSize}");
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

        public int CompareTo(Seller other)
        {
            // Compare sellers based on the number of products they sell
            return SellerProducts.Count.CompareTo(other.SellerProducts.Count);
        }
        public override int GetHashCode()
        {
            return Tuple.Create(base.GetHashCode(), seller_products).GetHashCode();
        }

    }
}
