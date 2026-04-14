using Microsoft.AspNetCore.Mvc;
using praktika22.Data.Interfaces;

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
    }
}
