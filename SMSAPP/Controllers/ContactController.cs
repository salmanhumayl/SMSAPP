using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMSAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace SMSAPP.Controllers
{
    public class ContactController : ControllerBase
    {
     
        HttpResponseMessage response_API = null;
        string responseString_API = "";
        private IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("GetContact")]
        public async Task<IActionResult> GetContact()
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Get, "https://connect.smsapp.pk/api/v3/contacts");
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

        [HttpPost]
        [Route("CreateContact")]
        public async Task<ActionResult> CreateContact(contact model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/store", model.group_id));

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
                    return NotFound();

                }

            }
        }
        [HttpPost]
        [Route("ViewContact")]
        public async Task<ActionResult> ViewContact(string group_id, string uid)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/search/{1}", group_id, uid));

                QueryParameters obj = new QueryParameters();
                obj.group_id = group_id;
                obj.uid = uid;

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);


                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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
                   // return StatusCode((int)response_API.StatusCode);
                }

            }

        }


        [HttpPost]
        [Route("UpdateContact")]
        public async Task<ActionResult> UpdateContact(UpdateContacts model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/update/{1}", model.group_id, model.uid));

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
        [Route("DeleteContact")]
        public async Task<ActionResult> DeleteContact(string group_id, string uid)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/delete/{1}", group_id, uid));

                QueryParameters obj = new QueryParameters();
                obj.group_id = group_id;
                obj.uid = uid;

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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
        [Route("ViewAllContactsGroup")]
        public async Task<IActionResult> ViewAllContactsGroup(string group_id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, String.Format("https://connect.smsapp.pk/api/v3/contacts/{0}/all", group_id));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);

                  var json = Newtonsoft.Json.JsonConvert.SerializeObject(group_id);
                  request.Content = new StringContent(json, Encoding.UTF8, "application/json");

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
