using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using praktika22.Data.ViewModell;
using System.Reflection.Metadata.Ecma335;

namespace praktika22.Controllers
{
    public class ItemsController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategorys;
        VMItems VMItems = new VMItems();

        private readonly IHostingEnvironment hostingEnvironment;

        public ItemsController(IItems IAllItems, ICategorys IAllCategorys, IHostingEnvironment hostingEnvironment)
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";
            VMItems.Items = IAllItems.AllItems;
            VMItems.Categorys = IAllCategorys.AllCategorys;
            VMItems.SelectCategory = id;

            return View(VMItems);
        }

        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<Data.Models.Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }

        [HttpGet]
        public ViewResult Delete()
        {
            IEnumerable<Data.Models.Items> Items = IAllItems.AllItems;
            return View(Items);
        }

        [HttpGet]
        public ViewResult Update()
        {
            VMItems.Items = IAllItems.AllItems;
            VMItems.Categorys = IAllCategorys.AllCategorys;
            return View(VMItems);
        }

        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = "/img/" + files.FileName;
            newItems.Price = (int)price;
            newItems.Category = new Categorys() { Id = idCategory };

            int id = IAllItems.Add(newItems);
            return Redirect("/Items/List?id=" + newItems.Category.Id);
        }

        [HttpPost]
        public RedirectResult Delete(int idItem)
        {
            if (idItem != 0)
            {
                Items categoryItem = IAllItems.AllItems.First(x => x.Id == idItem);
                IAllItems.Delete(idItem);
                return Redirect("/Items/List?id=" + categoryItem.Category.Id);
            }

            return Redirect("/Items/List");
        }

        [HttpPost]
        public RedirectResult Update(int idItem, string name, string description, IFormFile files, float price, int idCategory)
        {
            Items updateItem = new Items();
            updateItem.Id = idItem;
            updateItem.Name = name;
            updateItem.Description = description;

            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            updateItem.Img = "/img/" + files.FileName;
            updateItem.Price = (int)price;
            updateItem.Category = new Categorys() { Id = idCategory };

            IAllItems.Update(updateItem);
            return Redirect("/Items/List?id=" + idCategory);
        }
    }
}
