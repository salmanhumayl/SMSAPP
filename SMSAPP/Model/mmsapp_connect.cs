using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSAPP.Model
{
    public class mmsapp_connect
    {
        public string recipient { get; set; }
        public string sender_id { get; set; }
        public string message { get; set; }

        public IFormFile mms_file { get; set; }
    }

    public class DTOmmsapp_connect
    {
        public string recipient { get; set; }
        public string sender_id { get; set; }
        public string message { get; set; }

        public string mms_file { get; set; }
    }
}
