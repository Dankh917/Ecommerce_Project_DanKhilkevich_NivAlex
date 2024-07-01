using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class MainProgram
    {
        // DAN KHILKEVICH 212394274
        // NIV ALEX 322822602
        static void Main(string[] args)
        {
            EcommerceStore store = new EcommerceStore("niv and dan store"); // creating a new store
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
                Console.WriteLine("9. Print Seller Products List");
                Console.WriteLine("10. View Past Purchases of a Buyer");
                Console.WriteLine("11. Clone Shopping cart from Past Purchases");
                Console.WriteLine("12. Compare Buyers' Shopping Carts");
                Console.WriteLine("13. Exit");

                Console.Write("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1: // Add Buyer
                            AddBuyer(store);
                            break;

                        case 2: // Add Seller
                            AddSeller(store);
                            break;

                        case 3: // Add Product to Seller
                            AddProductToSeller(store);
                            break;

                        case 4: // Add Product to Buyer's Cart
                            AddProductToBuyersCart(store);
                            break;

                        case 5: // Checkout for Buyer
                            CheckoutForBuyer(store);
                            break;

                        case 6: // Print Buyers Details
                            store.PrintBuyersArrayDetails();
                            break;

                        case 7: // Print Sellers Details
                            store.PrintSellersArrayDetails();
                            break;

                        case 8: // Print specific Buyer Shopping Cart
                            PrintBuyerShoppingCart(store);
                            break;

                        case 9: // Print specific Seller Product list
                            PrintSellerProductsList(store);
                            break;

                        case 10: // View Past Purchases of a Buyer
                            ViewPastPurchases(store);
                            break;
                        
                        case 11:
                            CloneCartFromLastPurchases(store);
                            break;
                        case 12:
                            CheckBuyersComparison(store);
                            break;
                        case 13:
                            exitRequested = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }
                catch (SingleItemOrderException ex)
                {
                    Console.WriteLine($"Order error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private static void AddBuyer(EcommerceStore store)
        {
            Console.Write("Enter buyer username: ");
            string buyer_username = Console.ReadLine();

            Console.Write("Enter buyer password: ");
            string buyer_password = Console.ReadLine();

            Console.Write("Enter street name: ");
            string buyer_street = Console.ReadLine();

            Console.Write("Enter building number: ");
            int buyer_buildingNumber;
            while (!int.TryParse(Console.ReadLine(), out buyer_buildingNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid building number:");
            }

            Console.Write("Enter city name: ");
            string buyer_city = Console.ReadLine();

            Console.Write("Enter country name: ");
            string buyer_country = Console.ReadLine();

            // Create a new Buyer object
            Buyer buyer = new Buyer(buyer_username, buyer_password, new Address(buyer_street, buyer_buildingNumber, buyer_city, buyer_country));

            // Add the buyer to the store using the overloaded + operator
            store += buyer;
        }

        private static void AddSeller(EcommerceStore store)
        {
            Console.Write("Enter seller username: ");
            string seller_username = Console.ReadLine();

            Console.Write("Enter seller password: ");
            string seller_password = Console.ReadLine();

            Console.Write("Enter street name: ");
            string seller_street = Console.ReadLine();

            Console.Write("Enter building number: ");
            int seller_buildingNumber;
            while (!int.TryParse(Console.ReadLine(), out seller_buildingNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid building number:");
            }

            Console.Write("Enter city name: ");
            string seller_city = Console.ReadLine();

            Console.Write("Enter country name: ");
            string seller_country = Console.ReadLine();

            // Create a new Seller object
            Seller seller = new Seller(seller_username, seller_password, new Address(seller_street, seller_buildingNumber, seller_city, seller_country));

            // Add the seller to the store using the overloaded + operator
            store += seller;
        }

        private static void AddProductToSeller(EcommerceStore store)
        {
            Console.Write("Enter seller username: ");
            string username = Console.ReadLine();
            Seller seller = store.FindSellerByUsername(username);
            if (seller == null)
            {
                Console.WriteLine("Seller not found.");
                return;
            }

            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter product price: ");
            int productPrice = int.Parse(Console.ReadLine());
            Console.Write("Is it a special product? (true/false): ");
            bool isSpecialProduct = bool.Parse(Console.ReadLine());
            int packagingFee = 0;
            int starsRanking = 0;

            if (isSpecialProduct)
            {
                Console.Write("Enter packaging fee: ");
                packagingFee = int.Parse(Console.ReadLine());
                Console.Write("Enter the stars ranking: ");
                starsRanking = int.Parse(Console.ReadLine());
            }

            bool validCategory = false;
            Product.ProductCategory category = 0;

            while (!validCategory)
            {
                Console.WriteLine("Enter category of product:");
                Console.Write("1 for Kids, 2 for Electricity, 3 for Office, 4 for Clothing: ");
                if (Enum.TryParse(Console.ReadLine(), out category) && Enum.IsDefined(typeof(Product.ProductCategory), category))
                {
                    validCategory = true; // Set validCategory to true to exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid category. Please enter a valid category number.");
                }
            }

            bool productExists = seller.GetSellerProductList().Any(p =>
                p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase) &&
                p.ProductPrice == productPrice &&
                p.CategoryOfProduct == category &&
                ((p is SpecialProduct sp && isSpecialProduct && sp.PackagingFee == packagingFee && sp.StarsRanking == starsRanking) ||
                 (!(p is SpecialProduct) && !isSpecialProduct)));

            if (productExists)
            {
                Console.WriteLine("Product with the same name, price, category, and special status already exists for this seller.");
                return;
            }

            // Add the product to the seller's list
            if (isSpecialProduct) // add special product 
            {
                SpecialProduct specialProduct = new SpecialProduct(productName, productPrice, category, starsRanking, packagingFee);
                store.AddProductToSeller(username, specialProduct);
            }
            else // add regular product
            {
                Product product = new Product(productName, productPrice, category);
                store.AddProductToSeller(username, product);
            }
        }

        private static void AddProductToBuyersCart(EcommerceStore store)
        {
            Console.Write("Enter buyer username:");
            string add_product_username = Console.ReadLine();
            Console.Write("Enter product name:");
            string add_product_productName = Console.ReadLine();
            store.AddProductToBuyersCart(add_product_username, add_product_productName);
        }

        private static void CheckoutForBuyer(EcommerceStore store)
        {
            Console.Write("Enter buyer username:");
            string buyer_checkout_username = Console.ReadLine();
            store.CheckoutForBuyer(buyer_checkout_username);
        }

        private static void PrintBuyerShoppingCart(EcommerceStore store)
        {
            Console.Write("Enter buyer username:");
            string print_buyer_shopping_cart_username = Console.ReadLine();
            store.PrintBuyerShoppingCart(print_buyer_shopping_cart_username);
        }

        private static void PrintSellerProductsList(EcommerceStore store)
        {
            Console.Write("Enter seller username:");
            string print_seller_product_list_username = Console.ReadLine();
            store.PrintSellerProductsList(print_seller_product_list_username);
        }

        private static void ViewPastPurchases(EcommerceStore store)
        {
            Console.Write("Enter buyer username:");
            string view_past_purchases_username = Console.ReadLine();
            store.ViewPastPurchases(view_past_purchases_username);
        }
        private static void CloneCartFromLastPurchases(EcommerceStore store)
        {
            Console.WriteLine("Enter buyer username:");
            string buyerUsername = Console.ReadLine();
            Console.WriteLine("Enter order ID of the order you want to clone:");
            int orderId = int.Parse(Console.ReadLine());

            store.CloneCartFromLastPurchases(buyerUsername, orderId);
        }
        private static void CheckBuyersComparison(EcommerceStore store)
        {
            try
            {
                Console.WriteLine("Enter the name of the first buyer:");
                string buyerName1 = Console.ReadLine();

                Console.WriteLine("Enter the name of the second buyer:");
                string buyerName2 = Console.ReadLine();

                store.CompareBuyers(buyerName1, buyerName2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while comparing buyers: {ex.Message}");
            }
        }

    }
}
