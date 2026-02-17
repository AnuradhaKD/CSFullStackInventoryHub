using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Product 
    { 
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public double Price { get; set; } 
        public int Stock { get; set; } 
        public Category Category { get; set; } = new Category(); 
    }
    public class Category 
    { 
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
    }
}
