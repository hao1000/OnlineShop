﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Category.Where(x => x.Status == true).ToList();
        }
    }
}