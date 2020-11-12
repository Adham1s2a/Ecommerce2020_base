using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IItemRepository
    {
        Item GetItem(int id);
        List<Item> GetAllItems();
        List<Item> GetCategoryItems(int categoryID);
        Item AddItem(Item item);
        Item UpdateItem(Item Itemchange);
        Item Delete(int id);        
        int getcatID(int id);
    }
}
