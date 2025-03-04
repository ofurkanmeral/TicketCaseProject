using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Domain.Entities
{
    public class getbusjourneysData
    {
        [JsonProperty("origin-id")]
        public int originid { get; set; }

        [JsonProperty("destination-id")]
        public int destinationid { get; set; }

        [JsonProperty("departure-date")]
        public string departuredate { get; set; }
    }

    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }

        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class getbusjourneys
    {
        [JsonProperty("device-session")]
        public DeviceSession devicesession { get; set; }
        public string date { get; set; }
        public string language { get; set; }
        [JsonProperty("data")]
        public getbusjourneysData data { get; set; }
    }


}
