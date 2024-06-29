using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Ecommerce_Project_DanKhilkevich_NivAlex.Product;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class EcommerceStore
    {
        private string name;
        private User[] users_list; // users array each user can be buyer or seller (polymorphism principle)
        private int users_array_logical_size = 0;
        private int users_array_physical_size = 0;

        public EcommerceStore(string name) //EcommerceStore constructor
        {
            this.name = name;
            users_list = new User[1]; // Initialize with a capacity of 1 //need to check
            users_array_physical_size = 1;
        }

        public User[] UsersList
        {
            get { return users_list; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(UsersList), "User list cannot be null.");
                }
                users_list = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Store name cannot be null or empty.", nameof(Name));
                }
                name = value;
            }
        }

        // AddBuyer function recieves buyer details and adds the buyer to store
        public void AddBuyer(string username, string password, string street, int buildingNumber, string city, string country)
        {
            if (IsUserAlreadyExists(username))
            {
                Console.WriteLine("User with the same username already exists.");
                return;
            }

            Buyer buyer = new Buyer(username, password, new Address(street, buildingNumber, city, country));
            AddUserToStore(buyer);
            Console.WriteLine("Buyer added successfully.");
        }
        // AddSeller function recieves seller details and adds the seller to store
        public void AddSeller(string username, string password, string street, int buildingNumber, string city, string country)
        {
            if (IsUserAlreadyExists(username))
            {
                Console.WriteLine("User with the same username already exists.");
                return;
            }

            Seller seller = new Seller(username, password, new Address(street, buildingNumber, city, country));
            AddUserToStore(seller);
            Console.WriteLine("Seller added successfully.");
        }

        // Adds a user (can be buyer or seller) to the store
        private void AddUserToStore(User user)
        {
            if (user == null)
            {
                return;
            }

            if (users_array_logical_size == users_array_physical_size)
            {
                int newSize = users_array_physical_size * 2;
                User[] temp = new User[newSize];
                Array.Copy(users_list, temp, users_array_logical_size);
                users_list = temp;
                users_array_physical_size = newSize;
            }

            users_list[users_array_logical_size++] = user;
        }


        public void PrintUsersArrayDetails()
        {
            Console.WriteLine("Users array details:");
            Console.WriteLine($"Logical Size: {users_array_logical_size}, Physical Size: {users_array_physical_size}");
            foreach (User user in users_list)
            {
                if (user != null)
                {
                    Console.WriteLine(user.ToString());
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }
        public void PrintBuyersArrayDetails()
        {
            Console.WriteLine("Buyers array Details:");
            foreach (User user in users_list)
            {
                if (user is Buyer buyer)
                {
                    Console.WriteLine($"Username: {buyer.Username}");
                    Console.WriteLine($"Address: {buyer.Address.ToString()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }

        public void PrintSellersArrayDetails()
        {
            Console.WriteLine("Sellers array Details:");
            foreach (User user in users_list)
            {
                if (user is Seller seller)
                {
                    Console.WriteLine($"Username: {seller.Username}");
                    Console.WriteLine($"Address: {seller.Address.ToString()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }

        // Adds a product to the seller's product list
        public void AddProductToSeller(string username, Product product)
        {
            Seller seller = FindSellerByUsername(username);

            if (seller == null)
            {
                Console.WriteLine("Seller not found.");
                return;
            }

            seller.AddToProductList(product);
        }

        // Adds a product to the buyer's shopping cart
        public void AddProductToBuyersCart(string username, string productName)
        {
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Seller seller = FindSellerByProduct(productName);

            if (seller == null)
            {
                Console.WriteLine("There is no seller selling this product.");
                return;
            }

            Product product = seller.FindProductByName(productName);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            buyer.AddProductToShoppingCart(product);
            Console.WriteLine("Product added successfully to the buyer's cart.");
        }

        // Checkout for the buyer
        public void CheckoutForBuyer(string username)
        {
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Console.WriteLine("Buyer's Shopping Cart:");
            foreach (Product product in buyer.ShoppingCart)
            {
                if (product != null)
                {
                    Console.WriteLine(product.ToString());
                }
            }

            int totalPrice = buyer.CalculateTotalPrice();
            Console.WriteLine($"Total Price: {totalPrice}");

            buyer.BuyTheShoppingCart();
        }

        // Prints buyer's shopping cart
        public void PrintBuyerShoppingCart(string username)
        {
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            buyer.PrintCurrentShoppingCart();
        }

        // Prints seller's product list
        public void PrintSellerProductsList(string username)
        {
            Seller seller = FindSellerByUsername(username);

            if (seller == null)
            {
                Console.WriteLine("Seller not found.");
                return;
            }

            seller.PrintSellerProducts();
        }
        public Seller FindSellerByUsername(string username)
        {
            foreach (User user in users_list)
            {
                if (user is Seller seller && seller.Username == username)
                {
                    return seller;
                }
            }
            return null;
        }

        // Views past purchases of the buyer
        public void ViewPastPurchases(string username)
        {
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Console.WriteLine($"The Past Purchases of Buyer: {buyer.Username}");
            buyer.PrintPastPurchases();
        }
        public override string ToString()
        {
            return $"Store Name: {name}\nTotal Users: {users_array_logical_size}";
        }

        public void CloneCartFromLastPurchases(string buyerUsername, int orderId)
        {
            Buyer buyer = FindBuyerByUsername(buyerUsername);
            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Order orderToClone = buyer.FindOrderById(orderId); //find order that we want to clone
            if (orderToClone == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found in past purchases for buyer {buyerUsername}.");
                return;
            }

            try
            {
                // Clone the order by calling the Clone function from ICloneable interface
                Order clonedOrder = (Order)orderToClone.Clone();

                // Add the cloned products to the buyer's current shopping cart
                foreach (var product in clonedOrder.ProductList)
                {
                    AddProductToBuyersCart(buyerUsername, product.ProductName);
                }
                buyer.BuySpecificOrder(clonedOrder);
                Console.WriteLine("Shopping cart cloned and added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cloning shopping cart: {ex.Message}");
            }
        }








        // Private helper functions used only in this class
        private bool IsUserAlreadyExists(string username)
        {
            foreach (User user in users_list)
            {
                if (user != null && user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }


        private Seller FindSellerByProduct(string productName)
        {
            foreach (User user in users_list)
            {
                if (user is Seller seller && seller.SearchProductIfItExists(productName))
                {
                    return seller;
                }
            }
            return null;
        }

        private Buyer FindBuyerByUsername(string username)
        {
            foreach (User user in users_list)
            {
                if (user is Buyer buyer && buyer.Username == username)
                {
                    return buyer;
                }
            }
            return null;
        }
    }
}
