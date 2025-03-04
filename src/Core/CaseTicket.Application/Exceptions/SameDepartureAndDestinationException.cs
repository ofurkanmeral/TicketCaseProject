using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Exceptions
{
    public class SameDepartureAndDestinationException:Exception
    {
        public SameDepartureAndDestinationException():this("Kalkış noktası ve Varış noktası Aynı Olamaz") { }

        public SameDepartureAndDestinationException(string message):base(message) { }

        public SameDepartureAndDestinationException(Exception ex):this(ex.Message) { }
    }
}
