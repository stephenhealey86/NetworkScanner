using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace NetworkScanner
{
    public class SettingsUserControlViewModel : BaseViewModel
    {
        #region Public Variables
        public NetworkScannerSettingsModel Settings { get; set; }
        public delegate void Del();
        public Del CloseDialog;
        public int MinNumberOfPings { get; set; } = 1;
        public int MinPingTimeout { get; set; } = 50;
        #endregion

        #region Private Variables
        private ISettingsService _settingsService;
        #endregion

        #region Constructors
        public SettingsUserControlViewModel()
        {
            SetCommands();
            base.Title = "Settings";
            _settingsService = IoC.Get<ISettingsService>();
            Settings = _settingsService.GetSettings();
        }
        #endregion

        #region Commands
        public ICommand CloseDialogCommand { get; set; }
        public ICommand SaveSettingsCommand { get; set; }
        #endregion

        #region CommandActions
        private void CloseDialogCommandAction()
        {
            CloseDialog?.Invoke();
        }
        private void SaveSettingsCommandAction()
        {
            Settings.NumberOfPings = Settings.NumberOfPings > 0 ? Settings.NumberOfPings : 1;
            Settings.PingTimeout = Settings.PingTimeout >= 20 ? Settings.PingTimeout : 20;
            _settingsService.UdateSettings(Settings);
            CloseDialog?.Invoke();
        }
        #endregion

        #region Helpers
        private void SetCommands()
        {
            CloseDialogCommand = new RelayCommand(() => CloseDialogCommandAction());
            SaveSettingsCommand = new RelayCommand(() => SaveSettingsCommandAction());
        }
        #endregion
    }
}
