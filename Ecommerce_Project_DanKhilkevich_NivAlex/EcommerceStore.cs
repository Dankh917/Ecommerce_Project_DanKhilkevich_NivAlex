using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class EcommerceStore
    {
        private string name;
        private Buyer[] buyerlist;
        private Seller[] sellerlist;
        private int logi_size = 0;

        public void AddBuyerToStore(Buyer buyer)
        {
            if (logi_size < this.buyerlist.Length)
            {
                this.buyerlist[logi_size] = buyer;
                logi_size++;
            }
            else
            {
                Buyer[] temp = new Buyer[this.buyerlist.Length * 2];
                buyerlist.CopyTo(temp, 0);
                temp[logi_size] = buyer;
                logi_size++;
                this.buyerlist = temp;
                Console.WriteLine("increasing size...");
            }
        }
    }
}
