using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class SettingsService : ISettingsService
    {
        public event SettingsUpdatedDelegate SettingsUpdated;

        public NetworkScannerSettingsModel GetSettings()
        {
            var settings = Properties.Settings.Default.AppSetting;
            var json = JsonConvert.DeserializeObject<NetworkScannerSettingsModel>(settings);
            if (json != null)
            {
                return json;
            }
            return new NetworkScannerSettingsModel()
            {
                NumberOfPings = 8,
                PingTimeout = 500
            };
        }

        public void UdateSettings(NetworkScannerSettingsModel settingsModel)
        {
            var json = JsonConvert.SerializeObject(settingsModel);
            Properties.Settings.Default.AppSetting = json;
            Properties.Settings.Default.Save();
            SettingsUpdated?.Invoke();
        }
    }
}
