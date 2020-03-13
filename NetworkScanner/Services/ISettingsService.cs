using System.Collections.Generic;

namespace NetworkScanner
{
    public interface ISettingsService
    {
        NetworkScannerSettingsModel GetSettings();
        void UdateSettings(NetworkScannerSettingsModel settingsModel);
    }
}