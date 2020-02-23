using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class NetworkRangeModel
    {
        public string StartIpAddress { get; set; } = "192.168.1.1";
        public string EndIpAddress { get; set; } = "192.168.1.50";
        public string Subnet { get; set; } = "255.255.255.0";
        public ObservableCollection<string> ListOfActiveNetworkIpAddresses { get; set; }
        public NetworkRangeModel()
        {
            ListOfActiveNetworkIpAddresses = new ObservableCollection<string>();
        }
    }
}
