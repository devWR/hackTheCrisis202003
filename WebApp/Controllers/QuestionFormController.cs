using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class QuestionFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DetailedForm(InitialForm initForm)
        {
            ModelState.Clear();
            InitialForm form = JsonConvert.DeserializeObject<InitialForm>(initForm.SerializedCopy);
            var detailedForm = AutoMapper.Mapper.Map<DetailedForm>(form);

            return View(detailedForm);
        }

        [HttpPost]
        public IActionResult ApplyForm(InitialForm form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", form);
                }

                TempData["Form"] = JsonConvert.SerializeObject(form);
                return RedirectToAction("ContactingPersonView", "Chat");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { errorType = "", errorName = e.Message, errorDetails = "" });
            }
        }


        [HttpPost]
        public IActionResult ApplyDetailedForm(DetailedForm form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("DetailedForm", form);
                }

                TempData["Form"] = JsonConvert.SerializeObject(form);
                return RedirectToAction("ContactingPersonViewDetailedForm", "Chat");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { errorType = "", errorName = e.Message, errorDetails = "" });
            }
        }

        public IActionResult ConsultantLogin()
        {
            try
            {
                return RedirectToAction("NormalConsultantView", "Chat");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { errorType = "", errorName = e.Message, errorDetails = "" });
            }
        }
    }
}