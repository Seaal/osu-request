using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OsuRequest.AuthServer.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
