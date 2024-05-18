using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class Order
    {
        private Product[] product_list;
        private int total_price;
        private Buyer buyer_details;
        private int itemCount; // to keep track of the number of products in the order
        private int physicalSize;

        public Order(Buyer buyer_details) //order constructor
        {
            this.buyer_details = buyer_details;
            product_list = new Product[0];
            total_price = 0;
            itemCount = 0;
            physicalSize = 0;
        }

        public Buyer GetBuyerDetails()
        {
            return buyer_details;
        }

        public int GetTotalPrice()
        {
            return total_price;
        }

        public Product[] GetProductList()
        {
            // Create a new array to hold only the products added to the order
            Product[] products = new Product[itemCount];

            // Copy the products from the product_list array to the new array
            Array.Copy(product_list, products, itemCount);

            return products;
        }

        public void AddProductToOrder(Product product)
        {
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
            total_price += product.GetProductPrice();
            // If the product is a special product, we add the packaging fee to the total price
            if (product.GetIsSpecialProduct())
            {
                total_price += product.GetPackagingFee();
            }
        }

    }
}
