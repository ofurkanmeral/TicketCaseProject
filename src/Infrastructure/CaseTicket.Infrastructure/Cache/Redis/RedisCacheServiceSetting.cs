using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Infrastructure.Cache.Redis
{
    public class RedisCacheServiceSetting
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public double ExpireTime { get; set; }
    }
}
