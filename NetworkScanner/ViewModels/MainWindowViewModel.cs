using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Reflection;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace NetworkScanner
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Public Variables
        public Visibility windowIsMaximised { get { return App.Current.MainWindow.WindowState == WindowState.Maximized ? Visibility.Collapsed : Visibility.Visible; } }
        public object CurrentPage => _navigationService.GetCurrentPage();
        public List<AbstractPage> NavigationPages => _navigationService.GetAllPages();
        #endregion
        #region Private Variables
        private DialogOpenedEventArgs dialogOpenedEventArgs;
        private SettingsUserControl SettingsUserControl;
        private INavigationService _navigationService;
        private ILoggingService _logger;
        private string Version
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var Major = assembly.GetName().Version.Major;
                var Minor = assembly.GetName().Version.Minor;
                var Build = assembly.GetName().Version.Build;
                return $"V{Major}.{Minor}.{Build}";
            }
        }
        #endregion
        #region Constructors
        public MainWindowViewModel()
        {
            Title = $"Network Scanner - {Version}";
            SetCommands();
            _navigationService = IoC.Get<INavigationService>();
            _navigationService.Navigate(NavigationPages.FirstOrDefault(p => p.ViewModel.Title == "Form"));
            _logger = IoC.Get<ILoggingService>();
            _logger.Log(LogLevels.Info, "Application started");
        }
        #endregion
        #region Commands
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }
        #endregion
        #region Helpers
        private void SetCommands()
        {
            // Caption Buttons
            MinimizeWindowCommand = new RelayCommand(() => App.Current.MainWindow.WindowState = WindowState.Minimized);
            MaximizeWindowCommand = new RelayCommand(() => App.Current.MainWindow.WindowState ^= WindowState.Maximized);
            CloseWindowCommand = new RelayCommand(() => { App.Current.MainWindow.Close(); });
            OpenSettingsCommand = new RelayCommand(async () => await OpenSettingsCommandAction());
        }

        public void WindowStateChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(windowIsMaximised));
        }

        public void CloseDialog()
        {
            dialogOpenedEventArgs?.Session.Close();
        }
        public async Task OpenDialogAsync()
        {
            try
            {
                var content = new SettingsUserControl();
                content.ViewModel.CloseDialog += CloseDialog;
                await DialogHost.Show(content, delegate (object sender, DialogOpenedEventArgs args)
                {
                    dialogOpenedEventArgs = args;
                });
            }
            catch (Exception e)
            {
                _logger.Log(LogLevels.Error, e.Message);
                _logger.Log(LogLevels.Error, e.StackTrace);
            }
        }
        #endregion
        #region CommandActions
        private async Task OpenSettingsCommandAction()
        {
            await OpenDialogAsync();
        }
        #endregion
    }
}