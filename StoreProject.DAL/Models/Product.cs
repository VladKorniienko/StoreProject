using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? Genre { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public List<User>? Users { get; } = new();
    }
}
