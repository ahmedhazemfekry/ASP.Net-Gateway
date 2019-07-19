using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesMatchingController.Models;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace RulesMatching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesMatchingController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public RulesMatchingController()
        {}

        /// <summary>
        ///   Send products titles for rules matching.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Match
        ///     {
        ///        "products": ["Pepsi 1 Litre","Pringles","Nutella Jar"]
        ///     }
        ///
        /// </remarks>
        // POST: api/Match
        [HttpPost]
        public async Task<string> Predict(FactSet factset)
        {

            var content = JsonConvert.SerializeObject(factset);

            var response = await client.PostAsync("http://localhost:5020/Match", new StringContent(content, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}
