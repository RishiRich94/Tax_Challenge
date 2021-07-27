using AvalaraChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvalaraChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateTaxController : ControllerBase
    {
        // POST api/<StateTaxController>
        [HttpPost]
        public IActionResult Post(decimal money, string countyname)
        {
            TaxCalculator tc = new TaxCalculator(money,countyname);
            var errors = tc.ErrorCheck();
            if (errors != "")
                return BadRequest(errors);
            else
            {
                var taxRh = tc.GetTax();
                if (taxRh.IsError == true)
                {
                    return NotFound(taxRh.returnString);
                }
                else
                {
                    return Ok(taxRh.returnString);
                }
            }
        }

    }

}
