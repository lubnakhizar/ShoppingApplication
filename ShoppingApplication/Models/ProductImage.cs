using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingApplication.Models
{
  
    public class ProductImage
    {
        // Like junction table 
        // iss mai images k url rkhai gai k knsai product ki knsi images ha..
        // different product ki different images hoti hana...

        public int Id { get; set; }

        public string Image { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

    }
}