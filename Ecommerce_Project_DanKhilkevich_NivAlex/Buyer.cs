using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Buyer
    {
        private string buyer_username;
        private string buyer_password;
        private Address buyer_address;
        private Product[] shopping_cart;
        private Order[] past_purchases;
        private int past_purchases_logical_size = 0;
        private int past_purchases_physicalSize = 0;
        private int cartSize = 0;


        public Buyer(string buyer_username, string buyer_password, Address buyer_address) //buyer constructor
        {
            SetBuyerUsername(buyer_username);
            SetBuyerPassword(buyer_password);
            SetBuyerAddress(buyer_address);
            shopping_cart = new Product[0]; // Initialize as an empty array
            past_purchases = new Order[0]; // Initialize as an empty array
            past_purchases_logical_size = 0;
            past_purchases_physicalSize = 0;
        }

        public Buyer(Buyer other) //copy constructor
        {
            buyer_username = other.buyer_username;
            buyer_password = other.buyer_password;
            buyer_address = new Address(other.buyer_address); // assuming Address has a copy constructor
            shopping_cart = new Product[other.shopping_cart.Length];
            Array.Copy(other.shopping_cart, shopping_cart, other.shopping_cart.Length);
            past_purchases = new Order[other.past_purchases.Length];
            Array.Copy(other.past_purchases, past_purchases, other.past_purchases.Length);
            cartSize = other.cartSize;
        }

        public bool SetBuyerUsername(string buyer_username)
        {
            this.buyer_username = buyer_username;
            return true;
        }

        public bool SetBuyerPassword(string buyer_password)
        {
            this.buyer_password = buyer_password;
            return true;
        }

        public bool SetBuyerAddress(Address buyer_address)
        {
            this.buyer_address = buyer_address;
            return true;
        }

        public string GetBuyerUsername()
        {
            return this.buyer_username;
        }

        public string GetBuyerpassword()
        {
            return this.buyer_password;
        }

        public Address GetBuyerAddress()
        {
            return this.buyer_address;
        }

        public Product[] GetShoppingCart()
        {
            return this.shopping_cart;
        }

        public Order[] GetPastPurchases()
        {
            return this.past_purchases;
        }

        // AddProductToShoppingCart function received product and add the product to the buyer ShoppingCart
        public void AddProductToShoppingCart(Product product) 
        {
            if (cartSize == 0)
            {
                shopping_cart = new Product[1];
            }
            else if (cartSize == shopping_cart.Length)
            {
                int newSize = shopping_cart.Length * 2;
                Product[] temp = new Product[newSize];
                shopping_cart.CopyTo(temp, 0);
                shopping_cart = temp;
            }

            shopping_cart[cartSize++] = product;
        }

        // BuyTheShoppingCart function create new order from current products list, add it to PastPurchases and clear the ShoppingCart 
        public void BuyTheShoppingCart()
        {
            // Create a new order
            Order currOrder = new Order(this); // Pass the buyer details to the order constructor

            // Add all products from the shopping cart to the order
            foreach (Product product in shopping_cart)
            {
                if (product != null)
                {
                    // Add the product to the current order
                    currOrder.AddProductToOrder(product);
                }
            }

            // Add the current order to past purchases
            AddOrderToPastPurchases(currOrder);

            // Clear the shopping cart after purchase
            ClearShoppingCart();
        }

        public int CalculateTotalPrice()
        {
            int totalPrice = 0;

            // Iterate over the shopping cart and sum up the prices of all products
            foreach (Product product in shopping_cart)
            {
                if (product != null)
                {
                    totalPrice += product.GetProductPrice();
                    if (product.GetIsSpecialProduct())
                    {
                        totalPrice += product.GetPackagingFee();
                    }
                }
            }

            return totalPrice;
        }

        private void AddOrderToPastPurchases(Order order)
        {
            if (past_purchases_logical_size == 0)
            {
                // Initialize the past purchases array with a single element
                past_purchases = new Order[1];
                past_purchases_physicalSize = 1;
            }
            else if (past_purchases_logical_size == past_purchases_physicalSize)
            {
                // Double the size of the past purchases array if it full
                int newSize = past_purchases_physicalSize * 2;
                Order[] temp = new Order[newSize];
                Array.Copy(past_purchases, temp, past_purchases_logical_size);
                past_purchases = temp;
                past_purchases_physicalSize = newSize;
            }

            // Add the order to past purchases at the logical size index
            past_purchases[past_purchases_logical_size++] = order;
        }

        private void ClearShoppingCart()
        {
            // Clear all elements in the shopping cart array
            Array.Clear(shopping_cart, 0, shopping_cart.Length);
            // Reset logical size to 0
            cartSize = 0;
        }
        public void PrintcurrentShoppingCart()
        {
            Console.WriteLine("Shopping Cart Contents:");
            Console.WriteLine($"Logical Size: {cartSize}, Physical Size: {shopping_cart.Length}");
            for (int i = 0; i < cartSize; i++)
            {
                Console.WriteLine($"Product {i + 1}: {shopping_cart[i].PrintProductToString()}");
                Console.WriteLine("--------------------------------------------");
            }
        }
        public void PrintPastPurchases()
        {
            if (past_purchases == null || past_purchases.Length == 0)
            {
                Console.WriteLine("No past purchases available.");
                return;
            }

            Console.WriteLine("Past Purchases:");
            int orderNumber = 1; // Initialize order number

            foreach (Order order in past_purchases)
            {
                Console.WriteLine($"Order {orderNumber}:");
                Console.WriteLine("Buyer Details:");
                Console.WriteLine($"Username: {order.GetBuyerDetails().GetBuyerUsername()}, Address: {order.GetBuyerDetails().GetBuyerAddress().PrintAddressToString()}");
                Console.WriteLine("Products:");

                foreach (Product product in order.GetProductList())
                {
                    Console.WriteLine(product.PrintProductToString());
                }

                Console.WriteLine($"Total Price: {order.GetTotalPrice()}");
                Console.WriteLine("--------------------------------------------");
                orderNumber++; // Increment order number for the next order
            }
        }



    }   

}
