using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Domain.Entities
{
    public class Browser
    {
        public string name { get; set; }
        public string version { get; set; }
    }

    public class Connection
    {
        [JsonProperty("ip-address")]
        public string ipaddress { get; set; }
        public string port { get; set; }
    }

    public class getSessionRequest
    {
        public int type { get; set; }
        public Connection connection { get; set; }
        public Browser browser { get; set; }
    }


}
