using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApp.ViewComponents
{
    public class DisplayInitialFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(InitialForm form)
        {
            return View(form);
        }
    }
}
