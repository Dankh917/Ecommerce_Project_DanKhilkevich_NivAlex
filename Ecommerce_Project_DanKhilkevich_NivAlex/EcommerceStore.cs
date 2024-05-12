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
        private int buyer_logical_size = 0;
        private int seller_logical_size = 0;
        // Default constructor
        public EcommerceStore(string name)
        {
            this.name = name;
            // Initialize the buyer list and seller list with zero items
            buyerlist = new Buyer[0];
            sellerlist = new Seller[0];
        }
        // Method to get the list of buyers
        public Buyer[] GetBuyerList()
        {
            return buyerlist;
        }

        // Method to get the list of sellers
        public Seller[] GetSellerList()
        {
            return sellerlist;
        }

        // Method to get the name of the ecommerce store
        public string GetStoreName()
        {
            return name;
        }

        // Method to add a buyer to the store
        public void AddBuyerToStore(Buyer buyer)
        {
            if (buyer_logical_size == 0)
            {
                buyerlist = new Buyer[1];
            }
            else if (buyer_logical_size == buyerlist.Length)
            {
                int newSize = buyerlist.Length * 2;
                Buyer[] temp = new Buyer[newSize];
                buyerlist.CopyTo(temp, 0);
                buyerlist = temp;
            }

            buyerlist[buyer_logical_size++] = buyer;
        }

        // Method to add a seller to the store
        public void AddSellerToStore(Seller seller)
        {
            if (seller_logical_size == 0)
            {
                sellerlist = new Seller[1];
            }
            else if (seller_logical_size == sellerlist.Length)
            {
                int newSize = sellerlist.Length * 2;
                Seller[] temp = new Seller[newSize];
                sellerlist.CopyTo(temp, 0);
                sellerlist = temp;
            }

            sellerlist[seller_logical_size++] = seller;
        }

        // Method to print all buyers' details
        public void PrintBuyersDetails()
        {
            Console.WriteLine("Buyers Details:");
            foreach (Buyer buyer in buyerlist)
            {
                Console.WriteLine($"Username: {buyer.GetBuyerUsername()}");
                Console.WriteLine($"Address: {buyer.GetBuyerAddress().PrintAddress2String()}");
                Console.WriteLine(); // Add an empty line for separation
            }
        }

        // Method to print all sellers' details
        public void PrintSellersDetails()
        {
            Console.WriteLine("Sellers Details:");
            foreach (Seller seller in sellerlist)
            {
                Console.WriteLine($"Username: {seller.GetSellerUsername()}, Address: {seller.GetSellerAddress().PrintAddress2String()}");
            }
        }
    }
}
