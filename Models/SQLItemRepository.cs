using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        public SQLItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Item AddItem(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;

            throw new NotImplementedException();
        }

        public Item Delete(int id)
        {
            var item = context.Items.Find(id);
            if (item != null)
            {

                (from _item in context.Items
                where _item.ID == id
                select _item).ToList()
                .ForEach(x => x.IsDeleted = true);

                context.SaveChanges();
            }
            return (item);
        }

        public List<Item> GetAllItems()
        {
            return (from all in context.Items where all.IsDeleted != true select all).ToList();
            throw new NotImplementedException();
        }

      

        public List<Item> GetCategoryItems(int categoryID)
        {

            // return context.Items.Where(e => e.CategoryID == categoryID).ToList();
            //(from i in _context.Categories where i.ID == categoryID select i).First();
            return (from i in context.Items where i.CategoryID == categoryID && i.IsDeleted!= true select i).ToList();
            throw new NotImplementedException();
        }

        public int getcatID(int id)
        {
            return (from i in context.Items where i.ID == id select i.CategoryID).First();
            throw new NotImplementedException();
        }

        public Item GetItem(int id)
        {
            return context.Items.Find(id);
            
            throw new NotImplementedException();
        }

        public Item UpdateItem(Item Itemchange)
        {
            var item = context.Items.Attach(Itemchange);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Itemchange;
            throw new NotImplementedException();
        }
    }
}
