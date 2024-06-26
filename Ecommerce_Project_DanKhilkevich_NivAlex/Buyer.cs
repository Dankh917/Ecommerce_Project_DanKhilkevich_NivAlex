﻿using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{

    internal class Buyer : User
    {
        private ArrayList shopping_cart; // Using ArrayList instead of Product[]
        private ArrayList past_purchases;

        public Buyer(string buyer_username, string buyer_password, Address buyer_address) : base(buyer_username, buyer_password, buyer_address) //buyer constructor
        {
            ShoppingCart = new ArrayList(); // Initialize as an empty ArrayList
            PastPurchases = new ArrayList(); // Initialize as an empty ArrayList
        }

        public Buyer(Buyer other) : base(other.Username, other.Password, other.Address) //copy constructor
        {
            ShoppingCart = new ArrayList(other.ShoppingCart);
            PastPurchases = new ArrayList(other.PastPurchases);
        }

        public ArrayList ShoppingCart
        {
            get { return shopping_cart; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Shopping cart cannot be null.");
                shopping_cart = value;
            }
        }

        public ArrayList PastPurchases
        {
            get { return past_purchases; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Past purchases cannot be null.");
                past_purchases = value;
            }
        }

        // AddProductToShoppingCart function received product and add the product to the buyer ShoppingCart
        public void AddProductToShoppingCart(Product product)
        {
            ShoppingCart.Add(product);
        }

        // BuyTheShoppingCart function create new order from current products list, add it to PastPurchases and clear the ShoppingCart 
        public void BuyTheShoppingCart()
        {
            if (ShoppingCart.Count <= 1)
            {
                Console.WriteLine("Cannot create order. The shopping cart must contain more than one product.");
                return;
            }

            try
            {
                // Create a new order
                Order currOrder = new Order(this); // Pass the buyer details to the order constructor

                // Add all products from the shopping cart to the order
                foreach (Product product in ShoppingCart)
                {
                    if (product != null)
                    {
                        // Add the product to the current order
                        currOrder.AddProductToOrder(product);
                    }
                }

                currOrder.ValidateOrder(); // Check if order has more than 1 product
                AddOrderToPastPurchases(currOrder);
                ClearShoppingCart();
                Console.WriteLine("Checkout completed successfully.");
            }
            catch (SingleItemOrderException ex)
            {
                Console.WriteLine($"Order error: {ex.Message}");
            }
        }

        public void BuySpecificOrder(Order currOrder)
        {
            try
            {
                // Validate the order to ensure it has more than one product
                currOrder.ValidateOrder();

                // Add the validated order to the buyer's PastPurchases
                AddOrderToPastPurchases(currOrder);

                // Optional: Perform any additional actions after buying the order
                Console.WriteLine("Order bought successfully.");
            }
            catch (SingleItemOrderException ex)
            {
                Console.WriteLine($"Order error: {ex.Message}");
            }
        }

        public int CalculateTotalPrice()
        {
            int totalPrice = 0;

            // Iterate over the shopping cart and sum up the prices of all products
            foreach (Product product in ShoppingCart)
            {
                if (product != null)
                {
                    totalPrice += product.ProductPrice;
                    if (product is SpecialProduct specialProduct)
                    {
                        totalPrice += specialProduct.ProductPrice;
                    }
                }
            }

            return totalPrice;
        }
        // Operator for comparing shopping cart total prices: <
        public static bool operator <(Buyer buyer1, Buyer buyer2)
        {
            return buyer1.CalculateTotalPrice() < buyer2.CalculateTotalPrice();
        }

        // Operator for comparing shopping cart total prices: >
        public static bool operator >(Buyer buyer1, Buyer buyer2)
        {
            return buyer1.CalculateTotalPrice() > buyer2.CalculateTotalPrice();
        }

        private void AddOrderToPastPurchases(Order order)
        {
            PastPurchases.Add(order);
        }

        private void ClearShoppingCart()
        {
            // Clear all elements in the shopping cart ArrayList
            ShoppingCart.Clear();
        }

        public Order FindOrderById(int orderId)
        {
            foreach (Order order in PastPurchases)
            {
                if (order.OrderID == orderId)
                {
                    return order;
                }
            }
            return null; // Order not found
        }

        public void PrintCurrentShoppingCart()
        {
            Console.WriteLine("Shopping Cart Contents:");
            Console.WriteLine($"Logical Size: {ShoppingCart.Count}");
            for (int i = 0; i < ShoppingCart.Count; i++)
            {
                Console.WriteLine($"Product {i + 1}: {ShoppingCart[i].ToString()}");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public void PrintPastPurchases()
        {
            if (PastPurchases == null || PastPurchases.Count == 0)
            {
                Console.WriteLine("No past purchases available.");
                return;
            }

            Console.WriteLine("Past Purchases:");

            foreach (Order order in PastPurchases)
            {
                Console.WriteLine($"Order id: {order.OrderID}");
                Console.WriteLine("Buyer Details:");
                Console.WriteLine(order.BuyerDetails.ToString()); // Using UserToString from User class
                Console.WriteLine("Products list:");

                foreach (Product product in order.ProductList)
                {
                    Console.WriteLine(product.ToString());
                    Console.WriteLine("--------------------------------------------");
                }

                Console.WriteLine($"Total Price: {order.TotalPrice}");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Buyer other = (Buyer)obj;

            return Username == other.Username &&
                   Password == other.Password &&
                   Address.Equals(other.Address) &&
                   ShoppingCart.Cast<Product>().SequenceEqual(other.ShoppingCart.Cast<Product>()) &&
                   PastPurchases.Cast<Order>().SequenceEqual(other.PastPurchases.Cast<Order>());
        }

    }   

}
