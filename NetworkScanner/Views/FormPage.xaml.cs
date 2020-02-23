using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkScanner
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class FormPage : BasePage<FormPageViewModel>
    {
        private bool hasFinishedLoading = false;

        public FormPage()
        {
            InitializeComponent();
            hasFinishedLoading = true;
        }
        

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (hasFinishedLoading && e.Property.Name == "ActualWidth")
            {
                var fontSize = 12;
                var screenWidth = (double)e.NewValue;
                if (screenWidth > 800)
                {
                    fontSize = 16;
                }
                if (screenWidth > 1000)
                {
                    fontSize = 18;
                }
                if (screenWidth > 1200)
                {
                    fontSize = 20;
                }
                ((FormPageViewModel)this.ViewModel).FontSizeScaledForWindowWidth = fontSize;
            }
        }
    }
}
