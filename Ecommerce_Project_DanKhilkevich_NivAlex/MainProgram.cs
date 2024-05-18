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
            EcommerceStore store = new EcommerceStore("niv and dan store"); //creating a new store
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
                    case 1: //Add Buyer
                        Console.Write("Enter buyer username:");
                        string buyer_username = Console.ReadLine();
                        Console.Write("Enter buyer password:");
                        string buyer_password = Console.ReadLine();
                        Console.Write("Enter street name:");
                        string buyer_street = Console.ReadLine();
                        Console.Write("Enter building number:");
                        int buyer_buildingNumber = int.Parse(Console.ReadLine());
                        Console.Write("Enter city name:");
                        string buyer_city = Console.ReadLine();
                        Console.Write("Enter country name:");
                        string buyer_country = Console.ReadLine();
                        store.AddBuyer(buyer_username, buyer_password, buyer_street, buyer_buildingNumber,buyer_city, buyer_country);
                        break;


                    case 2: //Add Seller
                        Console.Write("Enter seller username:");
                        string seller_username = Console.ReadLine();
                        Console.Write("Enter seller password:");
                        string seller_password = Console.ReadLine();
                        Console.Write("Enter street name:");
                        string seller_street = Console.ReadLine();
                        Console.Write("Enter building number:");
                        int seller_buildingNumber = int.Parse(Console.ReadLine());
                        Console.Write("Enter city name:");
                        string seller_city = Console.ReadLine();
                        Console.Write("Enter country name:");
                        string seller_country = Console.ReadLine();
                        store.AddSeller(seller_username,seller_password,seller_street,seller_buildingNumber,seller_city,seller_country);
                        break;

                    
                    case 3://Add Product to Seller
                        Console.WriteLine("Enter seller username:");
                        string username = Console.ReadLine();
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
                        Console.WriteLine("Enter category of product (kids, electricity, office, clothing):");
                        string category = Console.ReadLine();
                        store.AddProductToSeller(username, productName, productPrice, isSpecialProduct, packagingFee, category);
                        break;
                    
                    
                    case 4: //Add Product to buyer Cart 
                        Console.WriteLine("Enter buyer username:");
                        string add_product_username = Console.ReadLine();
                        Console.WriteLine("Enter product name:");
                        string add_product_productName = Console.ReadLine();
                        store.AddProductToBuyersCart(add_product_username, add_product_productName);
                        break;

                    case 5: //Checkout For Buyer
                        Console.WriteLine("Enter buyer username:");
                        string buyer_chackout_username = Console.ReadLine();
                        store.CheckoutForBuyer(buyer_chackout_username);
                        break;


                    case 6: // Print Buyers Array Details
                        store.PrintBuyersArrayDetails();
                        break;
                    
                    case 7: // Print Sellers Array Details
                        store.PrintSellersArrayDetails();
                        break;
                    
                    case 8: //Print specific Buyer Shopping Cart
                        Console.WriteLine("Enter buyer username:");
                        string print_buyer_shppoing_cart_username = Console.ReadLine();
                        store.PrintBuyerShoppingCart(print_buyer_shppoing_cart_username);
                        break;
                    
                    case 9: //Print specific Seller Product list 
                        Console.WriteLine("Enter seller username:");
                        string print_seller_product_list_username = Console.ReadLine();
                        store.PrintSellerProductsList(print_seller_product_list_username);
                        break;
                    
                    case 10: //Print last Purchases of user
                        Console.WriteLine("Enter buyer username:");
                        string last_purchases_username = Console.ReadLine();
                        store.ViewPastPurchases(last_purchases_username);
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
