using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    public class SingleItemOrderException : Exception
    {
        public SingleItemOrderException() { }

        public SingleItemOrderException(string message) : base(message) { }

        public SingleItemOrderException(string message, Exception inner) : base(message, inner) { }
    }
    internal class Order: ICloneable
    {
        private static int nextOrderId = 1; // Static field to track the next order ID
        private int orderId; // Instance field for the order ID
        private Product[] product_list; // Products array, each product can be regular or special (polymorphism principle)
        private int total_price;
        private Buyer buyer_details;
        private int itemCount; // To keep track of the number of products in the order
        private int physicalSize;

        public Order(Buyer buyer_details) // Order constructor
        {
            this.orderId = GetNextOrderId(); // Assign the next available order ID
            BuyerDetails = buyer_details;
            product_list = new Product[0];
            total_price = 0;
            itemCount = 0;
            physicalSize = 0;
        }

        public int OrderID // Public property for accessing the order ID
        {
            get { return orderId; }
        }

        private static int GetNextOrderId()
        {
            return nextOrderId++; // Increment and return the next order ID
        }

        public Buyer BuyerDetails
        {
            get { return buyer_details; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(BuyerDetails), "Buyer details cannot be null.");
                }
                buyer_details = value;
            }
        }

        public int TotalPrice
        {
            get { return total_price; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(TotalPrice), "Total price cannot be negative.");
                }
                total_price = value;
            }
        }

        public Product[] ProductList
        {
            get
            {
                // Create a new array to hold only the products added to the order
                Product[] products = new Product[itemCount];

                // Copy the products from the product_list array to the new array
                Array.Copy(product_list, products, itemCount);

                return products;
            }
        }

        public void AddProductToOrder(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            if (itemCount == 0)
            {
                // If the product list is null, initialize it with a single element
                product_list = new Product[1];
                physicalSize = 1;
            }
            else if (itemCount == physicalSize)
            {
                // Double the size of the product list array if it's full
                int newSize = physicalSize * 2;
                Product[] temp = new Product[newSize];
                Array.Copy(product_list, temp, itemCount);
                product_list = temp;
                physicalSize = newSize;
            }

            // Add the product to the product list
            product_list[itemCount++] = product;

            // Update the total price
            TotalPrice += product.ProductPrice;

            // If the product is a special product, add the packaging fee to the total price
            if (product is SpecialProduct specialProduct)
            {
                TotalPrice += specialProduct.PackagingFee;
            }
        }

        public void ValidateOrder()
        {
            if (itemCount == 1)
            {
                throw new SingleItemOrderException("Order cannot contain only one product.");
            }
        }

        public object Clone()
        {
            Order clonedOrder = new Order(this.BuyerDetails);

            // Clone the product list
            clonedOrder.product_list = new Product[this.product_list.Length];
            Array.Copy(this.product_list, clonedOrder.product_list, this.product_list.Length);

            clonedOrder.total_price = this.total_price;
            clonedOrder.itemCount = this.itemCount;
            clonedOrder.physicalSize = this.physicalSize;

            return clonedOrder;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Order other = (Order)obj;

            return OrderID == other.OrderID &&
                   TotalPrice == other.TotalPrice &&
                   BuyerDetails.Equals(other.BuyerDetails) &&
                   ProductList.SequenceEqual(other.ProductList);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Order Details:");
            result.AppendLine($"Order ID: {OrderID}");
            result.AppendLine($"Buyer: {BuyerDetails}");
            result.AppendLine("Products:");
            foreach (var product in ProductList)
            {
                result.AppendLine(product.ToString());
            }
            result.AppendLine($"Total Price: {TotalPrice}");
            return result.ToString();
        }

    }
}
