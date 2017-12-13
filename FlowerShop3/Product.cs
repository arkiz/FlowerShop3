using System;
using SQLite;

namespace FlowerShop3
{
    public class Product
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]  
  
        public int id { get; set; } // AutoIncrement and set primarykey  
  
        [MaxLength(40)]  
  
        public string pName { get; set; }  
  
        [MaxLength(40)]  
  
        public string pType { get; set; } 

        public string pPrice { get; set; }

        public string pQty { get; set; } 
    }
}