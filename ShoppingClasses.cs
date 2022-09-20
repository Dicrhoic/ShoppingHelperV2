using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperV2
{
    internal class ShoppingClasses
    {
        public class Item
        {

            public int price;
            public string Name;
            public string Vendor;
            public string link;
            public string image;
            public string comment = "";

            public Item(int price, string Name, string Vendor, string link, string image)
            {
                this.Name = Name;
                this.Vendor = Vendor;
                this.price = price;
                this.link = link;
                this.image = image;
            }

            public void SetPrice(int price)
            {
                this.price = price;   
            }

            public void AddComment(string comment)
            {
                this.comment = comment;
            }
        }

      

       
    }
}
