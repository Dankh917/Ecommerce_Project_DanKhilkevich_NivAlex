using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{

    internal class Buyer : User
    {
        private Product[] shopping_cart; //products array, each product can be regular or special (polymorphism principle)
        private Order[] past_purchases;
        private int past_purchases_logical_size = 0;
        private int past_purchases_physicalSize = 0;
        private int cartSize = 0;

        public Buyer(string buyer_username, string buyer_password, Address buyer_address) : base(buyer_username, buyer_password, buyer_address) //buyer constructor
        {
            ShoppingCart = new Product[0]; // Initialize as an empty array
            PastPurchases = new Order[0]; // Initialize as an empty array
            PastPurchasesLogicalSize = 0;
            PastPurchasesPhysicalSize = 0;
        }

        public Buyer(Buyer other) : base(other.Username, other.Password, other.Address) //copy constructor
        {
            ShoppingCart = new Product[other.ShoppingCart.Length];
            Array.Copy(other.ShoppingCart, ShoppingCart, other.ShoppingCart.Length);
            PastPurchases = new Order[other.PastPurchases.Length];
            Array.Copy(other.PastPurchases, PastPurchases, other.PastPurchases.Length);
            CartSize = other.CartSize;
        }

        public Product[] ShoppingCart
        {
            get { return shopping_cart; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Shopping cart cannot be null.");
                shopping_cart = value;
            }
        }

        public Order[] PastPurchases
        {
            get { return past_purchases; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Past purchases cannot be null.");
                past_purchases = value;
            }
        }

        public int PastPurchasesLogicalSize
        {
            get { return past_purchases_logical_size; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Logical size of past purchases cannot be negative.");
                past_purchases_logical_size = value;
            }
        }

        public int PastPurchasesPhysicalSize
        {
            get { return past_purchases_physicalSize; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Physical size of past purchases cannot be negative.");
                past_purchases_physicalSize = value;
            }
        }

        public int CartSize
        {
            get { return cartSize; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Cart size cannot be negative.");
                cartSize = value;
            }
        }

        // AddProductToShoppingCart function received product and add the product to the buyer ShoppingCart
        public void AddProductToShoppingCart(Product product)
        {
            if (CartSize == 0)
            {
                ShoppingCart = new Product[1];
            }
            else if (CartSize == ShoppingCart.Length)
            {
                int newSize = ShoppingCart.Length * 2;
                Product[] temp = new Product[newSize];
                ShoppingCart.CopyTo(temp, 0);
                ShoppingCart = temp;
            }

            ShoppingCart[CartSize++] = product;
        }

        // BuyTheShoppingCart function create new order from current products list, add it to PastPurchases and clear the ShoppingCart 
        public void BuyTheShoppingCart()
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
            try
            {
                currOrder.ValidateOrder(); //check if order has more then 1 product
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

        private void AddOrderToPastPurchases(Order order)
        {
            if (PastPurchasesLogicalSize == 0)
            {
                // Initialize the past purchases array with a single element
                PastPurchases = new Order[1];
                PastPurchasesPhysicalSize = 1;
            }
            else if (PastPurchasesLogicalSize == PastPurchasesPhysicalSize)
            {
                // Double the size of the past purchases array if it full
                int newSize = PastPurchasesPhysicalSize * 2;
                Order[] temp = new Order[newSize];
                Array.Copy(PastPurchases, temp, PastPurchasesLogicalSize);
                PastPurchases = temp;
                PastPurchasesPhysicalSize = newSize;
            }

            // Add the order to past purchases at the logical size index
            PastPurchases[PastPurchasesLogicalSize++] = order;
        }

        private void ClearShoppingCart()
        {
            // Clear all elements in the shopping cart array
            Array.Clear(ShoppingCart, 0, ShoppingCart.Length);
            // Reset logical size to 0
            CartSize = 0;
        }

        public Order FindOrderById(int orderId)
        {
            foreach (Order order in past_purchases)
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
            Console.WriteLine($"Logical Size: {CartSize}, Physical Size: {ShoppingCart.Length}");
            for (int i = 0; i < CartSize; i++)
            {
                Console.WriteLine($"Product {i + 1}: {ShoppingCart[i].ToString()}");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public void PrintPastPurchases()
        {
            if (PastPurchases == null || PastPurchases.Length == 0)
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
                   ShoppingCart.SequenceEqual(other.ShoppingCart) &&
                   PastPurchases.SequenceEqual(other.PastPurchases);
        }

    }   

}
