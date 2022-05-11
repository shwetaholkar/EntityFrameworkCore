using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore
{
    public class BrandProductInfoResult
    {
        [Key]
        public int product_id { get; set; }
        public string brand_name { get; set; }
        public string product_name { get; set; }
        public string category_name { get; set; }
        public decimal list_price { get; set; }
    }
}