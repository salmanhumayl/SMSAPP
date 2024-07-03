using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMSAPP.Controllers
{
    public class ProfileController : ControllerBase
    {
        HttpResponseMessage response_API = null;
        string responseString_API = "";
        private IConfiguration _configuration;

        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("ViewSMSUnit")]
        public async Task<IActionResult> ViewSMSUnit()
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://connect.smsapp.pk/api/v3/balance");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);

                
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseString_API = await response_API.Content.ReadAsStringAsync();
                    return Ok(responseString_API);
                }


            }
            return StatusCode((int)response_API.StatusCode, new Response
            {

                Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
            });
        }


        [HttpGet]
        [Route("ViewProfile")]
        public async Task<IActionResult> ViewProfile()
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://connect.smsapp.pk/api/v3/me");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);


                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseString_API = await response_API.Content.ReadAsStringAsync();
                    return Ok(responseString_API);
                }


            }
            return StatusCode((int)response_API.StatusCode, new Response
            {

                Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
            });
        }
    }
}
