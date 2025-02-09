﻿using Microsoft.AspNetCore.Mvc;
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
    public class SMSController : ControllerBase
    {
       
        HttpResponseMessage response_API = null;
        string responseString_API = "";
        private IConfiguration _configuration;

        public SMSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpPost]
        [Route("SendoutboundSMS")]
        public async Task<ActionResult> SendoutboundSMS(smsapp_connect model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.smsapp.pk/api/v3/sms/send");

                request.Headers.Add("Accept", "application/json");
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
                    responseString_API = await response_API.Content.ReadAsStringAsync();
                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }


        [HttpPost]
        [Route("SendBulkMessages")]
        public async Task<ActionResult> SendBulkMessages([FromBody] List<messages> messages)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.smsapp.pk/api/v3/sms/send-bulk-messages");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(messages);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response_API = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response_API.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    responseString_API = await response_API.Content.ReadAsStringAsync();
                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }



        [HttpGet]
        [Route("ViewSMS")]
        public async Task<IActionResult> ViewSMS(string uid)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://connect.smsapp.pk/api/v3/sms/{0}", uid));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["SMSAPPAuthorization:Token"]);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(uid);
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
