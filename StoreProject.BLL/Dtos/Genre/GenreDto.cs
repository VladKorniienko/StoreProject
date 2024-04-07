using StoreProject.BLL.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.Genre
{
    public class GenreDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ProductPartialDto> Products { get; } = new();
    }
}
