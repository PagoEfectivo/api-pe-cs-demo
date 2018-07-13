using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagoEfectivo.Api.Demo.Models.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PagoEfectivo.Api.Demo.Controllers
{
    public class CipsController : Controller
    {
        // GET: /<controller>/

        public IActionResult Index()
        {
            ViewData["Message"] = "Bienvenidos a ApiDemo";
            return View();
        }

        [Route("Cips/GenerarCip")]
        public IActionResult GenerarCip()
        {
            ViewData["Message"] = "Bienvenidos a la Generación de Cip.";
            return View();
        }

        [Route("Cips/ConsultarCip")]
        public IActionResult ConsultarCip(CipViewModel model)
        {
            ViewData["Message"] = "Bienvenidos a Consultar Cip.";

            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var responseAuthz = AuthenticatePostAsync(model.Authenticate);

                CipViewModel cip = new CipViewModel();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(responseAuthz.url);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuthz.Data.Token);

                    string stringData = JsonConvert.SerializeObject(cip);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("v1/cips/search", contentData).Result;
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                    var responseAuth = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content.ReadAsStringAsync().Result);
                }

                return View();
            }
        }

        [Route("Cips/ActualizarCip")]
        public IActionResult ActualizarCip()
        {
            ViewData["Message"] = "Bienvenidos Actualizar Cip.";
            return View();
        }

        [Route("Cips/EliminarCip")]
        public IActionResult EliminarCip()
        {
            ViewData["Message"] = "Bienvenidos a Eliminar Cip.";
            return View();
        }

        public dynamic AuthenticatePostAsync(AuthenticateViewModel authenticateViewModel)
        {
            var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .Build();

            var _url = configBuilder["Url"];

            var date = Convert.ToDateTime(DateTime.Now);
            var DateNow = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, new TimeSpan(-5, 0, 0));

            Authenticate authorization = new Authenticate()
            {
                accessKey = authenticateViewModel.AccessKey,
                idService = authenticateViewModel.IdService,
                dateRequest = DateNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };

            var authRequest = authorization.idService.ToString() + "." + authorization.accessKey + "." + authenticateViewModel.SecretKey + "." + authorization.dateRequest;

            authorization.hashString = Hash.HashString(authRequest);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                string stringData = JsonConvert.SerializeObject(authorization);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("v1/authorizations", contentData).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                var responseAuth = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content.ReadAsStringAsync().Result);
                responseAuth.url = _url;
                return responseAuth;
                //return response.Content.ReadAsStringAsync().Result;
            }

        }


    }
}
