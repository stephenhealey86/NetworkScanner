using NetworkScanClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkScanner
{
    public class FormPageViewModel : BaseViewModel
    {
        #region Public Variables
        public NetworkRangeModel NetworkRange { get; set; }
        public bool ScanNetworkButtonBusy { get; set; }
        public InformationModel Information { get; set; } = new InformationModel();
        public double FontSizeScaledForWindowWidth
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                OnPropertyChanged(nameof(FontSizeScaledForWindowWidth));
            }
        }
        public NetworkScannerSettingsModel AppSetings { get; set; }
        #endregion
        #region Private Variables
        private ISettingsService _settingsService;
        private NetworkPing scanner;
        private double fontSize = 18;
        #endregion
        #region Constructor
        public FormPageViewModel()
        {
            NetworkRange = new NetworkRangeModel();
            scanner = new NetworkPing();
            _settingsService = IoC.Get<ISettingsService>();
            AppSetings = _settingsService.GetSettings();
            SetIpRangeBasedOnActiveInterfaceAdapter();
            ClearListOfActiveNetworkIpAddresses();
            scanner.ScanNetworkFoundAsyncDelegate += ScanIpAddressAsync;
            scanner.ScanNetworkCurrentIpAddressDelegate += UpdateInformationWithCurrentIpAddress;
            Title = "Form";
            SetCommands();
        }
        #endregion
        #region Commands
        public ICommand ScanNetworkCommand { get; set; }
        #endregion
        #region CommandActions
        private async Task ScanNetworkCommandAction()
        {
            if (ScanNetworkButtonBusy)
            {
                scanner.CancelAllScanning();
            } else
            {
                ClearListOfActiveNetworkIpAddresses();
                ScanNetworkButtonBusy = true;
                OnPropertyChanged(nameof(ScanNetworkButtonBusy));
                await scanner.ScanNetwork(NetworkRange.StartIpAddress, NetworkRange.EndIpAddress, NetworkRange.Subnet);
                ClearInformation();
                ScanNetworkButtonBusy = false;
                OnPropertyChanged(nameof(ScanNetworkButtonBusy));
            }
            
        }

        private void CancelScanNetworkCommandAction()
        {
            scanner.CancelAllScanning();
        }
        #endregion
        #region Helpers
        private void SetCommands()
        {
            ScanNetworkCommand = new RelayCommand(async () => await ScanNetworkCommandAction());
        }

        private async Task ScanIpAddressAsync(string ipAddress)
        {
            var result = await scanner.ScanAddressMaxTime(ipAddress, AppSetings.PingTimeout, AppSetings.NumberOfPings);
            AddIpAddressToList($"IP Address:\t{ipAddress}\tAverage time:\t{result.Item1}\tMax time:\t{result.Item2}ms");
        }
        private void AddIpAddressToList(string ipAddress)
        {
            NetworkRange.ListOfActiveNetworkIpAddresses.Add(ipAddress);
        }
        private void UpdateInformationWithCurrentIpAddress(string ipAddress)
        {
            Information.Message = $"Information: Current IP address {ipAddress}";
            Information.Active = true;
            OnPropertyChanged(nameof(Information));
        }
        private void ClearInformation()
        {
            Information.Message = null;
            Information.Active = false;
            OnPropertyChanged(nameof(Information));
        }
        private void ClearListOfActiveNetworkIpAddresses()
        {
            NetworkRange.ListOfActiveNetworkIpAddresses.Clear();
        }
        private void SetIpRangeBasedOnActiveInterfaceAdapter()
        {
            NetworkRange.StartIpAddress = scanner.GetFirstIpAddressInNetwork();
            NetworkRange.EndIpAddress = scanner.GetLastIpAddressInNetwork();
            NetworkRange.Subnet = scanner.GetSubnetAddressOfNetwork();
        }
        #endregion
    }
}