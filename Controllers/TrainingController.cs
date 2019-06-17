using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingController.Models;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace TrainingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public TrainingController()
        {}

     

        /// <summary>
        ///   Send products titles and categories for training.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /gateway
        ///     {
        ///        "products": ["Pepsi 1 Litre","Pringles","Nutella Jar"],
        ///        "classes": ["Soft Drinks", "Snacks", "Chocolates"]
        ///     }
        ///
        /// </remarks>
        // POST: api/gateway
        [HttpPost]
        public async Task<string> Train(TrainingSet trainingSet)
        {
           
            var content = JsonConvert.SerializeObject(trainingSet);

            var response = await client.PostAsync("http://localhost:5000/Train", new StringContent(content, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}