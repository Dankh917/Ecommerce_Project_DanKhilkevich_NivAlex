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
        private ArrayList seller_products; // Using ArrayList instead of Product[]
        private int logical_size = 0;

        // seller constructor
        public Seller() : base()
        {
            // Initialize the seller products ArrayList
            SellerProducts = new ArrayList();
        }

        // Constructor to initialize the seller properties
        public Seller(string seller_username, string seller_password, Address seller_address) : base(seller_username, seller_password, seller_address)
        {
            SellerProducts = new ArrayList();
        }

        public Seller(Seller other) : base(other.Username, other.Password, other.Address) // copy constructor
        {
            SellerProducts = new ArrayList(other.SellerProducts);
            LogicalSize = other.LogicalSize;
        }

        public ArrayList SellerProducts
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

        // function to get the list of products
        public Product[] GetSellerProductList()
        {
            Product[] products = new Product[LogicalSize];
            SellerProducts.CopyTo(products, 0);
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

            // Add the product to the seller's product list
            SellerProducts.Add(product);
            LogicalSize++;
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
                   SellerProducts.Cast<Product>().SequenceEqual(other.SellerProducts.Cast<Product>());
        }
        public int CompareTo(Seller other)
        {
            // Compare sellers based on the number of products they sell
            return seller_products.Count.CompareTo(other.seller_products.Count);
        }

    }
}
