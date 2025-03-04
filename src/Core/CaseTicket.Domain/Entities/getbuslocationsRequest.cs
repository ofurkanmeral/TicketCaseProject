using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Domain.Entities
{
    
    public class getbuslocationsDeviceSession
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }

        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class getbuslocationsRequest
    {
        public object data { get; set; }

        [JsonProperty("device-session")]
        public getbuslocationsDeviceSession devicesession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
    }

}
