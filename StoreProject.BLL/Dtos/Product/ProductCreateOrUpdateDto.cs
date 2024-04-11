using Microsoft.AspNetCore.Http;
using StoreProject.BLL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.Product
{
    //Do I even need it?
    public class ProductCreateOrUpdateDto
    {
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? GenreId { get; set; }
        public string? CategoryId { get; set; }
        public string? Description { get; set; }
        public IFormFile Icon { get; set; }
    }
}
