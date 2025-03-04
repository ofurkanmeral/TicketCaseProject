using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Exceptions
{
    public class ErrorHandler<T> where T : Exception
    {
        public static void Throw(string message)
        {
            throw (T)Activator.CreateInstance(typeof(T), message);
        }
    }
}
