﻿using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Interfaces
{
    public interface IGenreRepository :  IRepositoryBase<Genre>
    {
        public Task<Genre> GetByIdWithProducts(string id);
    }
}
