using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class DesignTimeData
    {
        public NetworkRangeModel NetworkRange { get; set; }

        public DesignTimeData()
        {
            NetworkRange = new NetworkRangeModel()
            {
                ListOfActiveNetworkIpAddresses = new System.Collections.ObjectModel.ObservableCollection<NetworkScannerPingResultModel>()
                {
                    new NetworkScannerPingResultModel()
                    {
                        IpAddress = "192.168.1.10",
                        AverageTime = "10",
                        MaxTime = "20"
                    },
                    new NetworkScannerPingResultModel()
                    {
                        IpAddress = "192.168.1.20",
                        AverageTime = "10",
                        MaxTime = "20"
                    },
                }
            };
        }
    }
}
