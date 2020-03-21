using NetworkScanClassLibrary;
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
        public SettingsUserControlViewModel SettingsUserControlViewModel { get; set; }

        public DesignTimeData()
        {
            IoC.Setup();
            SettingsUserControlViewModel = new SettingsUserControlViewModel();

            NetworkRange =  new NetworkRangeModel()
            {
                ListOfActiveNetworkIpAddresses = new System.Collections.ObjectModel.ObservableCollection<ScanResponse>()
                {
                    new ScanResponse()
                    {
                        IpAddress = "192.168.1.10",
                        AverageResponse = "10",
                        MaxResponse = "20",
                        Status = ScanResponseStatus.ok
                    },
                    new ScanResponse()
                    {
                        IpAddress = "192.168.1.20",
                        AverageResponse = "10",
                        MaxResponse = "20",
                        Status = ScanResponseStatus.ok
                    },
                }
            };
        }
    }
}
