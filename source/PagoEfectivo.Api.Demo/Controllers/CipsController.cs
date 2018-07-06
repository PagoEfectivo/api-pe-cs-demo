using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ConsultarCip()
        {
            ViewData["Message"] = "Bienvenidos a Consultar Cip.";
            return View();
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

    }
}
