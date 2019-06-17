using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PredictController.Models;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace PredictController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public PredictController()
        {}

        /// <summary>
        ///   Send products titles for categories prediction.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Predict
        ///     {
        ///        "products": ["Pepsi 1 Litre","Pringles","Nutella Jar"]
        ///     }
        ///
        /// </remarks>
        // POST: api/Predict
        [HttpPost]
        public async Task<string> Predict(PredictionSet predictionSet)
        {

            var content = JsonConvert.SerializeObject(predictionSet);

            var response = await client.PostAsync("http://localhost:5000/Predict", new StringContent(content, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}
