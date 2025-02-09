﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using SMSAPP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMSAPP.Controllers
{
    public class MMSController : Controller
    {

        HttpResponseMessage response_API = null;
        string responseString_API = "";
        private IConfiguration _configuration;

        public MMSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("SendoutboundMMS")]
        public async Task<ActionResult> SendoutboundMMS(mmsapp_connect model)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.smsapp.pk/api/v3/mms/send");

                string taggedBase64String;
                using (var memoryStream = new MemoryStream())
                {
                    await model.mms_file.CopyToAsync(memoryStream);
                    string ContentType  = model.mms_file.ContentType;
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);
                    taggedBase64String = $"data:{ContentType};base64, {base64String}";
                }

                DTOmmsapp_connect obj = new DTOmmsapp_connect();
                obj.recipient = model.recipient;
                obj.sender_id = model.sender_id;
                obj.message = model.message;
                obj.mms_file = taggedBase64String;


                request.Headers.Add("Accept", "application/json");
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
                    responseString_API = await response_API.Content.ReadAsStringAsync();
                    return StatusCode((int)response_API.StatusCode, new Response
                    {

                        Message = "Unsuccessful! Please try again. " + response_API.RequestMessage
                    });
                }

            }

        }



        [HttpGet]
        [Route("ViewMMS")]
        public async Task<IActionResult> ViewMMS(string uid)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format("https://connect.smsapp.pk/api/v3/mms/{0}", uid));

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


        [HttpGet]
        [Route("ViewAllMMSMessages")]
        public async Task<IActionResult> ViewAllMMSMessages()
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,"https://connect.smsapp.pk/api/v3/mms");

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


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}

