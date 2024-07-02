using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSAPP.Model
{
    public class smsapp_connect
    {
        public string recipient { get; set; }
        public string sender_id { get; set; }
        public string message { get; set; }
    }

    public class messages
    {
        public Int64 contact { get; set; }
        public string message { get; set; }
        public string type { get; set; }
    }
}
