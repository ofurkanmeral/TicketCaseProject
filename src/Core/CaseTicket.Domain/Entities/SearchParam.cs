using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Domain.Entities
{
    public class SearchParam
    {
        public string Origin { get; set; }
        public string OriginName { get; set; }

        public string Destination { get; set; }
        public string DestinationName { get; set; }

        public DateTime TravelDate { get; set; }
    }
}
