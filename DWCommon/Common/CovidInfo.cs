using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWCommon.Common
{
    public class CovidInfo
    {
        public int Key { get; set; }
        public string Date { get; set; }
        public string Country { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
    }
}
