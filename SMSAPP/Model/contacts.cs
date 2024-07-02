using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSAPP.Model
{
    public class contact
    {

        public string group_id { get; set; }
        public Int64 phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }

    public class UpdateContacts
    {

        public string group_id { get; set; }
        public string uid { get; set; }
        public Int64 phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }
    public class QueryParameters
    {
        public string group_id { get; set; }
        public string uid { get; set; }

    }
}
