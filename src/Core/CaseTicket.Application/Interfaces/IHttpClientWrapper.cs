using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Interfaces
{
    public interface IHttpClientWrapper
    {
        void AddBasicAuthentication();
        Task<string> GetStringAsyncWrapper(string url);
        Task<string> PostAsyncWrapper(string url, string jsonContent);
    }
}
