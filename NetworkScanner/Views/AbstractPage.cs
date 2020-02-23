using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetworkScanner
{
    public abstract class AbstractPage : Page
    {
        #region Private Variables
        /// <summary>
        /// The view model associated with this page
        /// </summary>
        private BaseViewModel mViewModel;
        #endregion

        #region Public Variables
        /// <summary>
        /// The name of the child class page
        /// </summary>
        public string MyPageType => this.GetType().ToString();
        /// <summary>
        /// The view model associated with this page
        /// </summary>
        public BaseViewModel ViewModel
        {
            get { return mViewModel; }

            set
            {
                // If nothing has changed
                if (mViewModel == value)
                    return;
                // Update value
                mViewModel = value;
                // Set the data context
                this.DataContext = mViewModel;
            }
        }
        #endregion

        #region Abstract Methods
        public abstract void NewViewModel();
        #endregion

    }
}
