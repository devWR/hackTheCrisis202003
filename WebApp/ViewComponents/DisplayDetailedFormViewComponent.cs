using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApp.ViewComponents
{
    public class DisplayDetailedFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DetailedForm form)
        {
            return View(form);
        }
    }
}
