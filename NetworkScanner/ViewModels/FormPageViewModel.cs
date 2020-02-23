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
        #endregion
        #region Private Variables
        private NetworkPing scanner;
        private double fontSize = 18;
        #endregion
        #region Constructor
        public FormPageViewModel()
        {
            NetworkRange = new NetworkRangeModel();
            scanner = new NetworkPing();
            ClearListOfActiveNetworkIpAddresses();
            scanner.ScanNetworkFoundAsyncDelegate += ScanIpAddress;
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
            ClearListOfActiveNetworkIpAddresses();
            ScanNetworkButtonBusy = true;
            OnPropertyChanged(nameof(ScanNetworkButtonBusy));
            await scanner.ScanNetwork(NetworkRange.StartIpAddress, NetworkRange.EndIpAddress, NetworkRange.Subnet);
            ScanNetworkButtonBusy = false;
            OnPropertyChanged(nameof(ScanNetworkButtonBusy));
        }
        #endregion
        #region Helpers
        private void SetCommands()
        {
            ScanNetworkCommand = new RelayCommand(async () => await ScanNetworkCommandAction());
        }
        private async Task ScanIpAddress(string ipAddress)
        {
            var result = await scanner.ScanAddress(ipAddress);
            AddIpAddressToList($"IP Address:\t{ipAddress}\t\tAverage time:\t{result}ms");
        }
        private void AddIpAddressToList(string ipAddress)
        {
            NetworkRange.ListOfActiveNetworkIpAddresses.Add(ipAddress);
        }
        private void ClearListOfActiveNetworkIpAddresses()
        {
            NetworkRange.ListOfActiveNetworkIpAddresses.Clear();
        }
        #endregion
    }
}