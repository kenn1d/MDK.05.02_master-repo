using Microsoft.AspNetCore.Mvc;

namespace praktika22.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Страница, открывающаяся при старте проекта
        /// </summary>
        public RedirectResult Index()
        {
            return Redirect("/Items/List");
        }
    }
}
