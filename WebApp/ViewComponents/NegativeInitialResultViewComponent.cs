using Microsoft.AspNetCore.Mvc;

namespace WebApp.ViewComponents
{
    public class NegativeInitialResultViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
