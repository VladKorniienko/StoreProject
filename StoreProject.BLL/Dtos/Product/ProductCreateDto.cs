﻿using StoreProject.BLL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.Product
{
    //Do I even need it?
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? GenreName { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
