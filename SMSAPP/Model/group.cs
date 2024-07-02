using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSAPP.Model
{
    public class group
    {
       public string name { get; set; }
    }

    public class UpdateGroup
    {
        public string group_id { get; set; }
        public string name { get; set; }
    }
}
