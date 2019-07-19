using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MLTrainingController.Models;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;

namespace MLTrainingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLTrainingController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public MLTrainingController()
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
        ///        "training_type": 1
        ///     }
        ///
        ///  For training type there are three choices 
                /// 1) dataset is very small less than 100 products choose training_type = 1
                /// 2) dataset is small less than 500 products choose training_type = 2
                /// 3) dataset is not small more than 500 products choose training_type = 3
        ///
        /// </remarks>
        // POST: api/gateway
        [HttpPost]
        public async Task<string> Train(TrainingSet trainingSet)
        {
           
            var content = JsonConvert.SerializeObject(trainingSet);

            var response = await client.PostAsync("http://localhost:5010/Train", new StringContent(content, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}