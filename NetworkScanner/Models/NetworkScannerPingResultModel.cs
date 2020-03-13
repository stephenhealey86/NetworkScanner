using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class NetworkScannerPingResultModel
    {
        public string IpAddress { get; set; }
        public string AverageTime { get; set; }
        public string MaxTime { get; set; }
    }
}
