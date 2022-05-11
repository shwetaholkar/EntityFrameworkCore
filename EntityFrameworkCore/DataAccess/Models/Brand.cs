using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DataAccess.Models
{
    //[production].[brands]
    [Table("brands", Schema = "production")]
    public class Brand
    {
        [Key]
        public int brand_id { get; set; }
        public string brand_name { get; set; }

    }
}
