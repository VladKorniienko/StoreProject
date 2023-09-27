using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Models
{
    public class ProductUser
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}
