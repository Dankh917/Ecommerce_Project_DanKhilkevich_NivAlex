using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class MainProgram
    {
        //DAN KHILKEVICH 212394274
        //NIV ALEX 322822602
        static EcommerceStore store = new EcommerceStore("niv alex store");
        static void Main(string[] args)
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Add Buyer");
                Console.WriteLine("2. Add Seller");
                Console.WriteLine("3. Add Product to Seller");
                Console.WriteLine("4. Add Product to Buyer's Cart");
                Console.WriteLine("5. Checkout for Buyer");
                Console.WriteLine("6. Print Buyers Details");
                Console.WriteLine("7. Print Sellers Details");
                Console.WriteLine("8. Print Buyer's Shopping Cart");
                Console.WriteLine("9. View Past Purchases of a Buyer");
                Console.WriteLine("10. Exit");

                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddBuyer();
                        break;
                    case 2:
                        AddSeller();
                        break;
                    case 3:
                        AddProductToSeller();
                        break;
                    case 4:
                        AddProductToBuyersCart();
                        break;
                    case 5:
                        CheckoutForBuyer();
                        break;
                    case 6:
                        store.PrintBuyersDetails();
                        break;
                    case 7:
                        store.PrintSellersDetails();
                        break;
                    case 8:
                        PrintBuyerShoppingCart();
                        break;
                    case 9:
                        ViewPastPurchases();
                        break;
                    case 10:
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        static void AddBuyer()
        {
            Console.WriteLine("Enter buyer username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter buyer password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter street name:");
            string street = Console.ReadLine();
            Console.WriteLine("Enter building number:");
            int buildingNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter city name:");
            string city = Console.ReadLine();
            Console.WriteLine("Enter country name:");
            string country = Console.ReadLine();

            Buyer buyer = new Buyer(username, password, new Address(street, buildingNumber, city, country));
            store.AddBuyerToStore(buyer);
            Console.WriteLine("Buyer added successfully.");
        }
        static void AddSeller()
        {
            Console.WriteLine("Enter seller username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter seller password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter street name:");
            string street = Console.ReadLine();
            Console.WriteLine("Enter building number:");
            int buildingNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter city name:");
            string city = Console.ReadLine();
            Console.WriteLine("Enter country name:");
            string country = Console.ReadLine();

            Seller seller = new Seller(username, password, new Address(street, buildingNumber, city, country));
            store.AddSellerToStore(seller);
            Console.WriteLine("Seller added successfully.");
        }
        static void AddProductToSeller()
        {
            Console.WriteLine("Enter seller username:");
            string username = Console.ReadLine();

            // Find the seller by username
            Seller seller = FindSellerByUsername(username);

            if (seller == null)
            {
                Console.WriteLine("Seller not found.");
                return;
            }

            Console.WriteLine("Enter product name:");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            int productPrice = int.Parse(Console.ReadLine());
            Console.WriteLine("Is it a special product? (true/false):");
            bool isSpecialProduct = bool.Parse(Console.ReadLine());
            int packagingFee = 0;
            if (isSpecialProduct)
            {
                Console.WriteLine("Enter packaging fee:");
                packagingFee = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Enter category of product:");
            string category = Console.ReadLine();

            // Create a new product
            Product product = new Product(productName, productPrice, isSpecialProduct, packagingFee, category);

            // Add the product to the seller's product list
            seller.AddToProductList(product);
            Console.WriteLine("Product added successfully to the seller.");
        }

        static Seller FindSellerByUsername(string username)
        {
            foreach (Seller seller in store.GetSellerList())
            {
                if (seller.GetSellerUsername() == username)
                {
                    return seller;
                }
            }
            return null;
        }
        static void AddProductToBuyersCart()
        {
            Console.WriteLine("Enter buyer username:");
            string username = Console.ReadLine();

            // Find the buyer by username
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Console.WriteLine("Enter product name:");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            int productPrice = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter category of product:");
            string category = Console.ReadLine();

            // Create a new product
            Product product = new Product(productName, productPrice, false, 0, category);

            // Add the product to the buyer's cart
            buyer.AddProductToShoppingCart(product);
            Console.WriteLine("Product added successfully to the buyer's cart.");
        }
        private static Buyer FindBuyerByUsername(string username)
        {
            foreach (Buyer buyer in store.GetBuyerList())
            {
                if (buyer.GetBuyerUsername() == username)
                {
                    return buyer;
                }
            }
            return null;
        }
        static void CheckoutForBuyer()
        {
            Console.WriteLine("Enter buyer username:");
            string username = Console.ReadLine();

            // Find the buyer by username
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            // Display the buyer's shopping cart
            Console.WriteLine("Buyer's Shopping Cart:");
            foreach (Product product in buyer.GetShoppingCart())
            {
                if (product != null)
                {
                    Console.WriteLine(product.PrintProduct2String());
                }
            }

            // Calculate the total price of the items in the shopping cart
            int totalPrice = buyer.CalculateTotalPrice();

            // Display the total price
            Console.WriteLine($"Total Price: {totalPrice}");

            // Proceed with the payment by buying the shopping cart
            buyer.BuyTheShoppingCart();

            Console.WriteLine("Checkout completed successfully.");
        }
        public static void PrintBuyerShoppingCart()
        {
            Console.WriteLine("Enter buyer username:");
            string username = Console.ReadLine();

            // Find the buyer by username
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            buyer.PrintcurrentShoppingCart();
        }
        static void ViewPastPurchases()
        {
            Console.WriteLine("Enter buyer username:");
            string username = Console.ReadLine();

            // Find the buyer by username
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            // Print the past purchases of the buyer
            Console.WriteLine($"Past Purchases of Buyer: {buyer.GetBuyerUsername()}");
            buyer.PrintPastPurchases();
        }
    }
}
