using System.Collections.Generic;

namespace NetworkScanner
{
    public delegate void SettingsUpdatedDelegate();

    public interface ISettingsService
    {
        NetworkScannerSettingsModel GetSettings();
        void UdateSettings(NetworkScannerSettingsModel settingsModel);
        event SettingsUpdatedDelegate SettingsUpdated;
    }
}