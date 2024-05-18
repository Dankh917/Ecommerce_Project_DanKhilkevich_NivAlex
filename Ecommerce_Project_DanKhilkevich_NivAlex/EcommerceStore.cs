using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class EcommerceStore
    {
        private string name;
        private Buyer[] buyerlist;
        private Seller[] sellerlist;
        private int buyers_array_logical_size = 0;
        private int buyers_array_physical_size = 0;
        private int sellers_array_logical_size = 0;
        private int sellers_array_physical__size = 0;

        public EcommerceStore(string name) //EcommerceStore constructor
        {
            this.name = name;
            buyerlist = new Buyer[0];
            sellerlist = new Seller[0];
        }

        public Buyer[] GetBuyerList()
        {
            return buyerlist;
        }

        public Seller[] GetSellerList()
        {
            return sellerlist;
        }

        public string GetStoreName()
        {
            return name;
        }

        //recived buyer object and add him to store. 
        //gets the buyer object from AddBuyer function (the AddBuyer function creates buyer from info that comes from the MainProgram)
        public void AddBuyerToStore(Buyer buyer)
        {
            if (buyer == null)
            {
                return;
            }

            if (buyers_array_logical_size == 0)
            {
                buyerlist = new Buyer[1];
                buyers_array_physical_size = 1;
            }
            else if (buyers_array_logical_size == buyers_array_physical_size)
            {
                int newSize = buyerlist.Length * 2;
                Buyer[] temp = new Buyer[newSize];
                buyerlist.CopyTo(temp, 0);
                buyerlist = temp;
                buyers_array_physical_size = newSize;
            }

            buyerlist[buyers_array_logical_size++] = buyer;
        }

        //recived seller object and add him to store. 
        //gets the seller object from AddBuyer function (the AddSeller function creates seller from info that comes from the MainProgram)
        public void AddSellerToStore(Seller seller)
        {
            if (sellers_array_logical_size == 0)
            {
                sellerlist = new Seller[1];
                sellers_array_physical__size = 1;
            }
            else if (sellers_array_logical_size == sellerlist.Length)
            {
                int newSize = sellerlist.Length * 2;
                Seller[] temp = new Seller[newSize];
                sellerlist.CopyTo(temp, 0);
                sellerlist = temp;
                sellers_array_physical__size = newSize;
            }

            sellerlist[sellers_array_logical_size++] = seller;
        }

        public void PrintBuyersArrayDetails()
        {
            Console.WriteLine("Buyers array Details:");
            Console.WriteLine($"Logical Size: {buyers_array_logical_size}, Physical Size: {buyers_array_physical_size}");
            foreach (Buyer buyer in buyerlist)
            {
                if (buyer != null)
                {
                    Console.WriteLine($"Username: {buyer.GetBuyerUsername()}");
                    Console.WriteLine($"Address: {buyer.GetBuyerAddress().PrintAddressToString()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }

        public void PrintSellersArrayDetails()
        {
            Console.WriteLine("Sellers array Details:");
            Console.WriteLine($"Logical Size: {sellers_array_logical_size}, Physical Size: {sellers_array_physical__size}");
            foreach (Seller seller in sellerlist)
            {
                if (seller != null)
                {
                    Console.WriteLine($"Username: {seller.GetSellerUsername()}, Address: {seller.GetSellerAddress().PrintAddressToString()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("The printing is completed.");
        }

        //recived the buyer details from the MainProgram
        public void AddBuyer(string username, string password, string street, int buildingNumber, string city, string country)
        {

            if (IsBuyerAlreadyExists(username))
            {
                Console.WriteLine("Buyer with the same username already exists.");
                return;
            }
            Buyer buyer = new Buyer(username, password, new Address(street, buildingNumber, city, country));
            AddBuyerToStore(buyer);
            Console.WriteLine("Buyer added successfully.");
        }

        //recived the seller details from the MainProgram
        public void AddSeller(string username, string password, string street, int buildingNumber, string city, string country)
        {

            if (IsSellerAlreadyExists(username))
            {
                Console.WriteLine("Seller with the same username already exists.");
                return;
            }
            Seller seller = new Seller(username, password, new Address(street, buildingNumber, city, country));
            AddSellerToStore(seller);
            Console.Write("Seller added successfully.\n");
        }

        //recived the info from the MainProgram
        public void AddProductToSeller(string username, string productName, int productPrice, bool isSpecialProduct, int packagingFee, string category)
        {
            Seller seller = FindSellerByUsername(username);

            if (seller == null)
            {
                Console.WriteLine("Seller not found.");
                return;
            }
            Product product = new Product(productName, productPrice, isSpecialProduct, packagingFee, category);
            seller.AddToProductList(product);
            Console.WriteLine("Product added successfully to the seller.");
        }

        //recived the info from the MainProgram
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

        //recived the info from the MainProgram
        public void CheckoutForBuyer(string username)
        {
            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Console.WriteLine("Buyer's Shopping Cart:");
            foreach (Product product in buyer.GetShoppingCart())
            {
                if (product != null)
                {
                    Console.WriteLine(product.PrintProductToString());
                }
            }

            int totalPrice = buyer.CalculateTotalPrice();

            Console.WriteLine($"Total Price: {totalPrice}");

            buyer.BuyTheShoppingCart();

            Console.WriteLine("Checkout completed successfully.");
        }

        //recived the info from the MainProgram
        public void PrintBuyerShoppingCart(string username)
        {

            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            buyer.PrintcurrentShoppingCart();
        }

        //recived the info from the MainProgram
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

        //recived the info from the MainProgram
        public void ViewPastPurchases(string username)
        {

            Buyer buyer = FindBuyerByUsername(username);

            if (buyer == null)
            {
                Console.WriteLine("Buyer not found.");
                return;
            }

            Console.WriteLine($"The Past Purchases of Buyer: {buyer.GetBuyerUsername()}");
            buyer.PrintPastPurchases();
        }




        // private functions, used only in this class: 
        private bool IsBuyerAlreadyExists(string username)
        {
            foreach (Buyer buyer in buyerlist)
            {
                if (buyer != null && buyer.GetBuyerUsername() == username)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsSellerAlreadyExists(string username)
        {
            foreach (Seller seller in sellerlist)
            {
                if (seller != null && seller.GetSellerUsername() == username)
                {
                    return true;
                }
            }
            return false;
        }

        private Seller FindSellerByUsername(string username)
        {
            if (sellerlist == null)
            {
                return null;
            }

            foreach (Seller seller in sellerlist)
            {
                if (seller != null && seller.GetSellerUsername() == username)
                {
                    return seller;
                }
            }
            return null;
        }

        private Seller FindSellerByProduct(string productName)
        {
            if (sellerlist == null)
            {
                return null;
            }

            foreach (Seller seller in sellerlist)
            {
                if (seller != null && seller.SearchProductIfItExists(productName))
                {
                    return seller;
                }
            }
            return null;
        }

        private Buyer FindBuyerByUsername(string username)
        {
            if (buyerlist == null)
            {
                return null;
            }

            foreach (Buyer buyer in buyerlist)
            {
                if (buyer != null && buyer.GetBuyerUsername() == username)
                {
                    return buyer;
                }
            }
            return null;
        }
    }
}
