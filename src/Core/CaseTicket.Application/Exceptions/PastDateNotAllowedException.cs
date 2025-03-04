using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Exceptions
{
    public class PastDateNotAllowedException:Exception
    {
        public PastDateNotAllowedException() : this("Kalkış noktası ve Varış noktası Aynı Olamaz") { }

        public PastDateNotAllowedException(string message) : base(message) { }

        public PastDateNotAllowedException(Exception ex) : this(ex.Message) { }
    }
}
