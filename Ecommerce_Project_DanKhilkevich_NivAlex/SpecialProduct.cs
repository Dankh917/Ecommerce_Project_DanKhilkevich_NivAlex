using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ecommerce_Project_DanKhilkevich_NivAlex.Product;

namespace Ecommerce_Project_DanKhilkevich_NivAlex
{
    internal class SpecialProduct: Product
    {
        private int starsRanking; // Stars ranking attribute for special product
        private int packaging_fee; // Packaging fee attribute for special product

        public SpecialProduct(string product_name, int product_price, ProductCategory category_of_product, int starsRanking, int packaging_fee)
         : base(product_name, product_price, category_of_product)
        {
            this.starsRanking = starsRanking;
            this.packaging_fee = packaging_fee;
        }

        public SpecialProduct(SpecialProduct other) : base(other) // Copy constructor
        {
            this.starsRanking = other.starsRanking;
            this.packaging_fee = other.packaging_fee;
        }

        public int GetStarsRanking()
        {
            return this.starsRanking;
        }

        public int GetPackagingFee()
        {
            return this.packaging_fee;
        }

        public bool SetStarsRanking(int starsRanking)
        {
            if (starsRanking < 1 || starsRanking > 5)
            {
                Console.WriteLine("Stars ranking must be between 1 and 5.");
                return false;
            }
            this.starsRanking = starsRanking;
            return true;
        }

        public bool SetPackagingFee(int packaging_fee)
        {
            this.packaging_fee = packaging_fee;
            return true;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nPackaging Fee: {packaging_fee}\nStars Ranking: {starsRanking}";
        }
    }
}
