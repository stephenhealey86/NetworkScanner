using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkScanner
{
    public class BaseUIComponent<VM> : UserControl where VM : BaseViewModel, new()
    {
        private VM mViewModel;

        public VM ViewModel
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
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseUIComponent()
        {
            NewViewModel();
        }
        #endregion

        public void NewViewModel()
        {
            ViewModel = new VM();
        }
    }
}
