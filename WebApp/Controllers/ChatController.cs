using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Enum;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactingPersonView()
        {
            InitialForm form = JsonConvert.DeserializeObject<InitialForm>((string)TempData["Form"]);
            form.SerializedCopy = JsonConvert.SerializeObject(form);
            return View(form);
        }

        public IActionResult ContactingPersonViewDetailedForm()
        {
            DetailedForm form = JsonConvert.DeserializeObject<DetailedForm>((string)TempData["Form"]);
            return View(form);
        }

        [HttpPost]
        public IActionResult AddToChatQueue(string userId, InitialForm form)
        {
            try
            {
                if (Global.UserInitialForms.ContainsKey(userId))
                    Global.UserInitialForms[userId] = form;
                else
                    Global.UserInitialForms.Add(userId, form);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult AddToChatQueueDetailed(string userId, DetailedForm form)
        {
            try
            {
                if (Global.UserDetailedForms.ContainsKey(userId))
                    Global.UserDetailedForms[userId] = form;
                else
                    Global.UserDetailedForms.Add(userId, form);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public IActionResult NormalConsultantView()
        {
            InitialForm form2 = new InitialForm()
            {
                Age = 20,
                ContactWithSuspect = true,
                ContactWithUnacquainted = true,
                CoughNBreathProblems = true,
                HaveAnyOfDiseases = true,
                HighTemperature = true,
                IfPersonTravelled = true,
                IfReceiveImmunosuppressiveMed = true,
                Name = "Wojtek",
                PESEL = "921413412",
                Surname = "Roz"
            };

            return View();
        }


        public IActionResult FullRightConsultantView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MarkAsOcupated(string userId, string consultantId)
        {
            try
            {
                InitialForm result = new InitialForm();
                Hubs.ChatHub.AddToOcupated(userId, consultantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetInitialFormFor(string userId)
        {
            try
            {
                InitialForm result = new InitialForm();

                if (Global.UserInitialForms.ContainsKey(userId))
                    result = Global.UserInitialForms[userId];

                return ViewComponent("DisplayInitialForm", new { form = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetDetailedFormFor(string userId)
        {
            try
            {
                DetailedForm result = new DetailedForm();

                if (Global.UserDetailedForms.ContainsKey(userId))
                    result = Global.UserDetailedForms[userId];

                return ViewComponent("DisplayDetailedForm", new { form = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllNotOcupatedUsers(string selectedId)
        {
            var result = new List<SelectListItem>();
            var allAwaitingUsers = Hubs.ChatHub.GetNotOcupatedConnections();

            if (allAwaitingUsers != null && allAwaitingUsers.Count > 0)
            {
                allAwaitingUsers.ForEach(e =>
                {
                    if (Global.UserInitialForms.ContainsKey(e))
                    {
                        string name = $"{Global.UserInitialForms[e].Name} {Global.UserInitialForms[e].Surname}";
                        result.Add(new SelectListItem(name, e, e == selectedId));
                    }
                });
            }

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllNotOcupatedUsers2ndLine(string selectedId)
        {
            var result = new List<SelectListItem>();
            var allAwaitingUsers = Hubs.ChatHub.GetNotOcupatedConnections();

            if (allAwaitingUsers != null && allAwaitingUsers.Count > 0)
            {
                allAwaitingUsers.ForEach(e =>
                {
                    if (Global.UserDetailedForms.ContainsKey(e))
                    {
                        string name = $"{Global.UserDetailedForms[e].Name} {Global.UserDetailedForms[e].Surname}";
                        result.Add(new SelectListItem(name, e, e == selectedId));
                    }
                });
            }

            return Ok(result);
        }

        public IActionResult GetResultPage(InitialDiagnosisResult diagnosisResult)
        {
            if (diagnosisResult == InitialDiagnosisResult.Positive)
                return ViewComponent("PositiveInitialResult");
            else
                return ViewComponent("NegativeInitialResult");
        }
    }
}