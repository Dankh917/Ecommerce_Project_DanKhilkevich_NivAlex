using System;
using System.Collections;
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
        private List<User> usersList; // Use List<User> for the users list

        public EcommerceStore(string name) // EcommerceStore constructor
        {
            this.name = name;
            usersList = new List<User>(); // Initialize the List<User>
        }

        public List<User> UsersList
        {
            get { return usersList; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(UsersList), "User list cannot be null.");
                }
                usersList = value;
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

        // Define the + operator to add a Buyer to the store
        public static EcommerceStore operator +(EcommerceStore store, Buyer buyer)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store), "Store instance cannot be null.");
            }

            if (buyer == null)
            {
                throw new ArgumentNullException(nameof(buyer), "Buyer instance cannot be null.");
            }

            if (store.IsUserAlreadyExists(buyer.Username))
            {
                Console.WriteLine("User with the same username already exists.");
                return store;
            }

            store.AddUserToStore(buyer);
            Console.WriteLine("Buyer added successfully.");
            return store;
        }

        // Define the + operator to add a Seller to the store
        public static EcommerceStore operator +(EcommerceStore store, Seller seller)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store), "Store instance cannot be null.");
            }

            if (seller == null)
            {
                throw new ArgumentNullException(nameof(seller), "Seller instance cannot be null.");
            }

            if (store.IsUserAlreadyExists(seller.Username))
            {
                Console.WriteLine("User with the same username already exists.");
                return store;
            }

            store.AddUserToStore(seller);
            Console.WriteLine("Seller added successfully.");
            return store;
        }

        // Adds a user (can be buyer or seller) to the store
        private void AddUserToStore(User user)
        {
            if (user == null)
            {
                return;
            }

            usersList.Add(user);
        }

        public void PrintUsersArrayDetails()
        {
            Console.WriteLine("Users array details:");
            Console.WriteLine($"Logical Size: {usersList.Count}");
            foreach (User user in usersList)
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
            foreach (User user in usersList)
            {
                if (user is Buyer buyer)
                {
                    Console.WriteLine($"Username: {buyer.Username}");
                    Console.WriteLine($"Address: {buyer.Address}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }

        public void PrintSellersArrayDetails()
        {
            Console.WriteLine("Sellers array Details:");

            // Sort sellers based on the number of products they sell
            List<Seller> sellersList = usersList.OfType<Seller>().OrderByDescending(s => s.SellerProducts.Count).ToList();

            // Print sorted sellers details
            foreach (Seller seller in sellersList)
            {
                Console.WriteLine($"Username: {seller.Username}");
                Console.WriteLine($"Address: {seller.Address}");
                Console.WriteLine($"Number of products: {seller.SellerProducts.Count}");
                Console.WriteLine();
            }
            Console.WriteLine("Printing completed.");
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
            foreach (User user in usersList)
            {
                if (user is Seller seller && seller.Username == username)
                {
                    return seller;
                }
            }
            return null;
        }

        public Buyer FindBuyerByUsername(string username)
        {
            foreach (User user in usersList)
            {
                if (user is Buyer buyer && buyer.Username == username)
                {
                    return buyer;
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
                foreach (Product product in clonedOrder.ProductList)
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

        public void CompareBuyers(string username1, string username2)
        {
            Buyer buyer1 = FindBuyerByUsername(username1);
            Buyer buyer2 = FindBuyerByUsername(username2);

            if (buyer1 == null || buyer2 == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            int totalPriceBuyer1 = buyer1.CalculateTotalPrice();
            int totalPriceBuyer2 = buyer2.CalculateTotalPrice();

            Console.WriteLine($"Total price in {buyer1.Username}'s shopping cart: {totalPriceBuyer1}");
            Console.WriteLine($"Total price in {buyer2.Username}'s shopping cart: {totalPriceBuyer2}");

            if (totalPriceBuyer1 > totalPriceBuyer2)
            {
                Console.WriteLine($"{buyer1.Username} has a bigger shopping cart total than {buyer2.Username}.");
            }
            else if (totalPriceBuyer1 < totalPriceBuyer2)
            {
                Console.WriteLine($"{buyer2.Username} has a bigger shopping cart total than {buyer1.Username}.");
            }
            else
            {
                Console.WriteLine($"Both {buyer1.Username} and {buyer2.Username} have the same total price in their shopping carts.");
            }
        }

        public override string ToString()
        {
            return $"Store Name: {name}\nTotal Users: {usersList.Count}";
        }

        // Private helper functions used only in this class
        private bool IsUserAlreadyExists(string username)
        {
            foreach (User user in usersList)
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
            foreach (User user in usersList)
            {
                if (user is Seller seller && seller.SearchProductIfItExists(productName))
                {
                    return seller;
                }
            }
            return null;
        }
    }
}
