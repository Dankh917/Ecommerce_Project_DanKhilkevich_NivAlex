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

                    case 3: // Add Product to Seller
                        Console.Write("Enter seller username: ");
                        string username = Console.ReadLine();
                        Seller seller = store.FindSellerByUsername(username);
                        if (seller == null)
                        {
                            Console.WriteLine("Seller not found.");
                            break;
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

                        while (validCategory == false)
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

                        // Check if the product already exists. if product is already exists in the seller array we dont create a new instance of product/special product
                        // and then we do not increment the id number of product (note: because id number is static attribute its automatically increasing by 1 when
                        // we crate new product/special product. so this validation will ensure we not crate products duplication)
                        bool productExists = seller.GetSellerProductList().Any(p =>
                            p.GetProductName().Equals(productName, StringComparison.OrdinalIgnoreCase) &&
                            p.GetProductPrice() == productPrice &&
                            p.GetCategoryOfProduct() == category &&
                            ((p is SpecialProduct sp && isSpecialProduct && sp.GetPackagingFee() == packagingFee && sp.GetStarsRanking() == starsRanking) ||
                             (!(p is SpecialProduct) && !isSpecialProduct)));

                        if (productExists)
                        {
                            Console.WriteLine("Product with the same name, price, category, and special status already exists for this seller.");
                            break;
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
                        break;

                    case 4: //Add Product to buyer Cart 
                        Console.Write("Enter buyer username:");
                        string add_product_username = Console.ReadLine();
                        Console.Write("Enter product name:");
                        string add_product_productName = Console.ReadLine();
                        store.AddProductToBuyersCart(add_product_username, add_product_productName);
                        break;

                    case 5: //Checkout For Buyer
                        Console.Write("Enter buyer username:");
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
                        Console.Write("Enter buyer username:");
                        string print_buyer_shppoing_cart_username = Console.ReadLine();
                        store.PrintBuyerShoppingCart(print_buyer_shppoing_cart_username);
                        break;
                    
                    case 9: //Print specific Seller Product list 
                        Console.Write("Enter seller username:");
                        string print_seller_product_list_username = Console.ReadLine();
                        store.PrintSellerProductsList(print_seller_product_list_username);
                        break;
                    
                    case 10: //Print last Purchases of user
                        Console.Write("Enter buyer username:");
                        string last_purchases_username = Console.ReadLine();
                        store.ViewPastPurchases(last_purchases_username);
                        break;
                   
                    case 11:
                        exitRequested = true;
                        break;

                    default:
                        Console.Write("Invalid choice. Please select a valid option.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
