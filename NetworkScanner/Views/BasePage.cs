using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkScanner
{
    public class BasePage<VM> : AbstractPage where VM : BaseViewModel, new()
    {
        #region Private Properties
        
        #endregion
        #region Public Properties
        
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            NewViewModel();
        }
        #endregion

        public override void NewViewModel()
        {
            base.ViewModel = new VM();
        }
    }
}
