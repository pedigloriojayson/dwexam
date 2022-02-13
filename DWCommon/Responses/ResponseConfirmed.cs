using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWCommon.Responses
{
    public class ResponseConfirmed
    {
        public string observation_date { get; set; }

        public ConfirmedData[] countries { get; set; }
        
    }

    public class ConfirmedData
    {
        public string country { get; set; }

        public int confirmed { get; set; }

        public int deaths { get; set; }

        public int recovered { get; set; }
    }
}
