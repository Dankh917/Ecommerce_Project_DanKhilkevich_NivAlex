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
        static void Main(string[] args)
        {
            EcommerceStore store = new EcommerceStore("niv and dan store");
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
                Console.WriteLine("11. Exit");

                Console.Write("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        store.AddBuyer();
                        break;
                    case 2:
                        store.AddSeller();
                        break;
                    case 3:
                        store.AddProductToSeller();
                        break;
                    case 4:
                        store.AddProductToBuyersCart();
                        break;
                    case 5:
                        store.CheckoutForBuyer();
                        break;
                    case 6:
                        store.PrintBuyersDetails();
                        break;
                    case 7:
                        store.PrintSellersDetails();
                        break;
                    case 8:
                        store.PrintBuyerShoppingCart();
                        break;
                    case 9:
                        store.PrintSellerProductsList();
                        break;
                    case 10:
                        store.ViewPastPurchases();
                        break;
                    case 11:
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
