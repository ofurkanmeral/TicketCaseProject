using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Infrastructure.Interfaces
{
    public interface IRedisCacheService
    {
        T GetAsync<T>(string key);
        bool SetAsync<T>(string key, T value);
        object RemoveData(string key);
    }
}
