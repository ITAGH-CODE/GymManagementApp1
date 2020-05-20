using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GymApp.CORE.Interfaces;
using GymApp.CORE.Entities;
using Microsoft.AspNetCore.Mvc.Razor;
using GymManagementApp_NETCORE.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace GymManagementApp_NETCORE.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        public IActionResult List()
        {
            IEnumerable<Client> MyClients = _clientServices.GetClients();
            return View(MyClients);
        }

        public IActionResult CreateClient(int Id)
        {
            Client cl;
            if (Id == 0)
            {
                cl = new Client();
            }
            else
            {
                cl = _clientServices.GetClientById(Id);
            }
            return View(cl);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (client.IdClient == 0)
                    {
                        _clientServices.CreateClient(client);
                        //_clientServices.UpdateClientPayment(client);
                    }
                    else
                    {
                        _clientServices.UpdateClient(client);
                    }
                    return Json(new
                    {
                        isValid = true,
                        html = Helper
                        .RenderRazorViewToString(this, "_ViewListClients", _clientServices.GetClients())
                    });
                }
                return Json(new
                {
                    isValid = false,
                    html = Helper
                        .RenderRazorViewToString(this, "CreateClient", client)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new
                {
                    isValid = false,
                    html = Helper
                    .RenderRazorViewToString(this, "_ViewListClients", _clientServices.GetClients())
                });
            }
        }

        public Client GetClientById(int Id)
        {
            return _clientServices.GetClientById(Id);
        }

        public JsonResult GetClients()
        {
            var clients = _clientServices.GetClients().ToList<Client>();
            List<Client> cls = new List<Client>();
            string renderedImg = string.Empty;
            foreach (var c in clients)
            {
                c.IsPaymentOk = _clientServices.isPaymentOK(c.IdPaymentOnGoing);
                cls.Add(c);
            }
            return Json(new { data = cls });
        }

        [HttpPost]
        public JsonResult DeleteClient(int Id)
        {
            try
            {
                _clientServices.DeleteClient(Id);
                return Json(new
                {
                    isDeleted = true,
                    html = Helper
                        .RenderRazorViewToString(this, "_ViewListClients", _clientServices.GetClients())
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new
                {
                    isDeleted = false,
                    html = Helper
                        .RenderRazorViewToString(this, "_ViewListClients", _clientServices.GetClients())
                });
            }
        }


        public JsonResult GetLastPayedPeriodByIdPayment(int Id)
        {
            string lastPayedPeriod = string.Empty;
            try
            {
                Payment payment = _clientServices.GetPayement(Id);
                if(payment != null)
                {
                    lastPayedPeriod = payment.StartDate.ToString("dd/MM/yyyy") + " => " + payment.EndDate.ToString("dd/MM/yyyy");
                }
                return Json(new { lastPayedPeriod = lastPayedPeriod });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { lastPayedPeriod = lastPayedPeriod });
            }
        }

        
        public IActionResult isPaymentOK(int id)
        {
            Payment payment = _clientServices.GetPayement(id);
            bool isPOk = _clientServices.isPaymentOK(id);
            return Json(new { isPaymentOk = isPOk });
        }

        [HttpPost]
        public IActionResult Pay(int Id)
        {
            Client client = _clientServices.GetClientById(Id);
            if(client != null)
                _clientServices.UpdateClientPayment(client);
            int IdPaymentOnGoing = _clientServices.GetIdPaymentOnGoing(Id);
            return Json(new
            {
                IdPayment = IdPaymentOnGoing,
                html = Helper
                        .RenderRazorViewToString(this, "_ViewListClients", _clientServices.GetClients())
            });
        }


    }
}