using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RulesUploading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesUploadingController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public RulesUploadingController()
        {}

        /// <summary>
        ///   Send products titles for rules matching.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Upload
        ///     {
        ///       File: Rules
        ///     }
        ///
        /// </remarks>
        // POST: api/Upload
        [HttpPost]
        public async Task<string> PostFile(IFormFile file)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);

            ByteArrayContent bytes = new ByteArrayContent(data);


            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(bytes, "Rules", file.FileName);
      
            
            var response = await client.PostAsync("http://localhost:5020/Upload",multiContent);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}
