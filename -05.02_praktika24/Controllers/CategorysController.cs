using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;

namespace praktika22.Controllers
{
    public class CategorysController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategorys;

        public CategorysController(IItems IAllItems, ICategorys IAllCategorys) {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
        }

        public ActionResult List() {
            ViewBag.Title = "Страница с категориями";
            var Items = IAllCategorys.AllCategorys;
            return View(Items);
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
            IEnumerable<Data.Models.Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }

        [HttpGet]
        public ViewResult Update()
        {
            IEnumerable<Data.Models.Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }

        [HttpPost]
        public RedirectResult Add(string Name, string Desc)
        {
            IAllCategorys.Add(Name, Desc);
            return Redirect("/Categorys/List");
        }

        [HttpPost]
        public RedirectResult Delete(int Id)
        {
            IAllCategorys.Delete(Id);
            return Redirect("/Categorys/List");
        }

        [HttpPost]
        public RedirectResult Update(int id, string name, string desc)
        {
            Categorys category = new Categorys();
            category.Id = id;
            category.Name = name;
            category.Description = desc;

            IAllCategorys.Update(category);
            return Redirect("/Categorys/List");
        }
    }
}
