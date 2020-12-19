using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICategoryRepositoy categoryRepositoy;
        private readonly IItemRepository itemRepository;

       
        public AdminController(AppDbContext context, IHostingEnvironment hostingEnvironment,ICategoryRepositoy categoryRepositoy,IItemRepository itemRepository)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.categoryRepositoy = categoryRepositoy;
            this.itemRepository = itemRepository;
        }

        // GET: Admin
        public async Task<IActionResult> AdminIndex()
        {
            return View( categoryRepositoy.GetAllCategories());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Create
        public IActionResult CreateCategory()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                string UniqueFile = Processuploadfile(model);
                string UniqueFileBG = ProcessuploadfileBG(model);
                Category category = new Category()
                {
                    CategoryName = model.CategoryName,
                    OfferStatus = model.OfferStatus,
                    OfferPersent = model.OfferPersent,
                    OfferAmount = model.OfferAmount,
                    IsDeleted = false,
                    ActionBy = 1,
                    ActionOn = DateTime.Now,
                    Photopath = UniqueFile,
                    Background = UniqueFileBG


                };

                categoryRepositoy.AddCategory(category);

                return View();
               // return RedirectToAction("details", new { id = category.Id });
            }
            else
                return View();

            }

        [HttpGet]
        public ViewResult Editcategory(int id)
        {
            Category category = categoryRepositoy.GetCategory(id);
            CategoryEditViewModel model = new CategoryEditViewModel()
            {
                id = id,
                CategoryName = category.CategoryName,
                OfferAmount  = Convert.ToDouble(category.OfferAmount),
                OfferPersent = Convert.ToInt32(category.OfferPersent),
                OfferStatus  = category.OfferStatus, 
                excistingphotopath = category.Photopath,
                excistingBackground =category.Background
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Editcategory(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryRepositoy.GetCategory(model.id);
                category.CategoryName = model.CategoryName;
                category.OfferStatus = model.OfferStatus;
                category.OfferAmount = model.OfferAmount;
                category.OfferPersent = model.OfferPersent;
                if (model.Photo != null)
                {
                    category.Photopath = Processuploadfile(model);
                }
                if (model.BG != null)
                {
                    category.Background = ProcessuploadfileBG(model);
                }

                categoryRepositoy.UpdateCategory(category);
                return RedirectToAction("details", new { id = category.ID });
            }
            return View();
        }
        [HttpGet]
        public ViewResult EditItem(int id)
        {
            
            Item item = itemRepository.GetItem(id);
            ItemEditViewModel model = new ItemEditViewModel()
            {
                id = id,
                ItemName = item.ItemName,
                ItemDesc=item.ItemDesc,
                SellPrice=item.SellPrice,
                Quantity=item.Quantity,
                excistingphotopath = item.Photopath,
                excistingphotopath1=item.Photopath1,
                excistingphotopath2=item.Photopath2
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult EditItem(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
               string[] itemphotos = Processuploadfileitem(model);
                Item item = itemRepository.GetItem(model.id);
                item.ItemName = model.ItemName;
                item.ItemDesc = model.ItemDesc;
                item.SellPrice = model.SellPrice;
                item.Quantity = model.Quantity;
                if (model.Photo != null)
                {
                    item.Photopath = itemphotos[0];
                }
                if (model.Photo1 != null)
                {
                    item.Photopath = itemphotos[1];
                }
                if (model.Photo2 != null)
                {
                    item.Photopath = itemphotos[2];
                }

                int catid = itemRepository.getcatID(item.ID);

                itemRepository.UpdateItem(item);
                return RedirectToAction("CategoryItemsAdmin", new { id = catid, page = 1 });
            }
            return View();
        }       

        // GET: Admin/Delete/5
        public async Task<IActionResult> Deletecat(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Deletecat")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedcat(int id)
        {
            categoryRepositoy.Delete(id);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminIndex" ,"admin");
        }


        public async Task<IActionResult> DeleteItem(int id)
        {
            
            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (item == null)
            {
                return NotFound();
            }

            itemRepository.Delete(id);

            return View(item);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Deleteitem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmeditem(int id)
        {
            itemRepository.Delete(id);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminIndex", "admin");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }



        public IActionResult CategoryItemsAdmin(int id, int? page)
        {

            
            ViewBag.ID = id;
            //Category cat = (from i in _context.Categories where i.ID == categoryID select i).First();
            //if (items == null)
            //{
            //    Response.StatusCode = 404;
            //    return View("Error404", ViewBag.id);
            //}

            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 6; // Get 12 Items for each requested page.
            var onePageOfItems = itemRepository.GetCategoryItems(id).ToPagedList(pageNumber, pageSize);
            return View(onePageOfItems); // Send 12 Items to the page.
           

        }


        public IActionResult CreateItem()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(CreateitemViewModel model,int id)
        {
            ViewBag.ID = id;
            if (ModelState.IsValid)
            {

                string[] itemphotos = Processuploadfileitem(model);

                Item item = new Item()
                {
                    ItemName = model.ItemName,
                    CategoryID = id,
                    ItemDesc = model.ItemDesc,
                    Quantity = model.Quantity,
                    SellPrice = model.SellPrice,
                    IsDeleted = false,
                    ActionBy = 1,
                    ActionOn = DateTime.Now,
                    Photopath = itemphotos[0],
                    Photopath1= itemphotos[1],
                    Photopath2= itemphotos[2]


                };

                itemRepository.AddItem(item);

                return View();
                // return RedirectToAction("details", new { id = category.Id });
            }
            else
                return View();

        }

      
        private string Processuploadfile(CreateCategoryViewModel model)
        { 
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            string UniqueFile = null;
            if (model.Photo != null)
            {
                
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFile = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFile);
                model.Photo.CopyTo(new FileStream(Filepath, FileMode.Create));
            }

            return UniqueFile;
        }
        private string ProcessuploadfileBG(CreateCategoryViewModel model)
        {
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            string UniqueFileBG = null;
            if (model.BG != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFileBG = Guid.NewGuid().ToString() + "_" + model.BG.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFileBG);
                model.BG.CopyTo(new FileStream(Filepath, FileMode.Create));
            }

            return UniqueFileBG;
        }
        private string[] Processuploadfileitem(CreateitemViewModel model)
        {
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            string[] itemphotos = new string[3];
            string UniqueFile = null;

            if (model.Photo != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFile = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFile);
                model.Photo.CopyTo(new FileStream(Filepath, FileMode.Create));
                itemphotos[0] = UniqueFile;
            }
             UniqueFile = null;
            if (model.Photo1 != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFile = Guid.NewGuid().ToString() + "_" + model.Photo1.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFile);
                model.Photo1.CopyTo(new FileStream(Filepath, FileMode.Create));
                itemphotos[1] = UniqueFile;
            }
            UniqueFile = null;
            if (model.Photo2 != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                UniqueFile = Guid.NewGuid().ToString() + "_" + model.Photo2.FileName;
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string Filepath = Path.Combine(UploadsFolder, UniqueFile);
                model.Photo2.CopyTo(new FileStream(Filepath, FileMode.Create));
                itemphotos[2] = UniqueFile;
            }

            return itemphotos;
        }
    }
}
