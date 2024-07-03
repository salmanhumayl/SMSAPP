using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SMSAPP.Model;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SMSAPP.Controllers
{
    public class ContactGroupController : ControllerBase
    {
        HttpResponseMessage response_API = null;
        string responseString_API = "";
        private IConfiguration _configuration;


        public ContactGroupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<ActionResult> CreateGroup(group model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ("https://connect.smsapp.pk/api/v3/contacts"));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }


        [HttpPost]
        [Route("ViewGroup")]
        public async Task<ActionResult> ViewGroup(string group_id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/show/", group_id));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(group_id);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseString_API = await response_API.Content.ReadAsStringAsync(); //then Deserilize 
                    return Ok(responseString_API);
                }
                else
                {

                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }

        [HttpPost]
        [Route("UpdateGroup")]
        public async Task<ActionResult> UpdateGroup(UpdateGroup model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}", model.group_id));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    return Ok();
                }
                else
                {

                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }

        [HttpPost]
        [Route("DeleteGroup")]
        public async Task<ActionResult> DeleteGroup(string group_id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}", group_id));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(group_id);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }
            }

        }


        [HttpGet]
        [Route("ViewAllGroups")]
        public async Task<IActionResult> ViewAllGroups()
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, " https://connect.smsapp.pk/api/v3/contacts");
                request1.Headers.Add("Accept", "application/json");
                request1.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                response_API = await httpClient.SendAsync(request1, HttpCompletionOption.ResponseHeadersRead);
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
