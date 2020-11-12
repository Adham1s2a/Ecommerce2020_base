using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Models
{
    public class SQLCategoryRepository : ICategoryRepositoy
    {
        private readonly AppDbContext context;

        public SQLCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Category AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
            throw new NotImplementedException();
        }

        public Category Delete(int id)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
            {
                (from cat in context.Categories
                 where cat.ID == id
                 select cat).ToList()
                 .ForEach(x => x.IsDeleted = true);

                context.SaveChanges();
            }
            return category;
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            return (from all in context.Categories where all.IsDeleted != true select all).ToList();
            throw new NotImplementedException();
        }

        //public IEnumerable<SelectListItem> GetCategoriesNames()
        //{
        //    var cat = (from i in context.Categories select i.CategoryName).ToList();
        //    return cat;
        //    throw new NotImplementedException();
        //}

        public Category GetCategory(int id)
        {
            Category category = context.Categories.Find(id);
            return category;

            throw new NotImplementedException();
        }



        public Category UpdateCategory(Category categorychange)
        {
            var category = context.Categories.Attach(categorychange);
            category.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return categorychange;
            throw new NotImplementedException();
        }


    }
}
