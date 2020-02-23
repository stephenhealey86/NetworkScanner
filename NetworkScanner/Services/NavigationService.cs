using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NetworkScanner
{
    public class NavigationService : INavigationService
    {
        #region Private Variables
        private object currentPage;
        private ILoggingService _logger;
        private List<AbstractPage> Pages = new List<AbstractPage>()
        {
            new FormPage()
        };
        #endregion

        #region Constructors
        public NavigationService()
        {
            _logger = IoC.Get<ILoggingService>();
        }
        #endregion

        #region Methods
        public void Navigate(object nextPage)
        {
            try
            {
                var page = nextPage as AbstractPage;
                // Set current page
                currentPage = page;

                // Animation
                var ta = new DoubleAnimation();
                ta.Duration = TimeSpan.FromSeconds(1.5);
                ta.DecelerationRatio = 0.7;
                ta.To = 1.0;
                ta.From = 0.0;
                page.BeginAnimation(Page.OpacityProperty, ta);
                return;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevels.Error, e.Message);
            }
            
        }

        /// <summary>
        /// Returns the current navigated page
        /// </summary>
        /// <returns></returns>
        public object GetCurrentPage()
        {
            return currentPage;
        }

        /// <summary>
        /// Returns the singleton page of type T
        /// </summary>
        /// <typeparam name="T">Type of page to return</typeparam>
        /// <returns></returns>
        public T GetSingletonPage<T>() where T : AbstractPage
        {
            foreach (var page in Pages)
            {
                if (typeof(T) == page.GetType())
                {
                    return page as T;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns new page of type T
        /// </summary>
        /// <typeparam name="T">The type of page to return</typeparam>
        /// <returns></returns>
        public T GetNewPage<T>() where T : AbstractPage
        {
            foreach (var page in Pages)
            {
                if (typeof(T) == page.GetType())
                {
                    page.NewViewModel();
                    return page as T;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the viewmodel of type T
        /// </summary>
        /// <typeparam name="T">Type of viewe model to return</typeparam>
        /// <returns></returns>
        public T GetViewModel<T>() where T : BaseViewModel
        {
            foreach (var page in Pages)
            {
                if (typeof(T) == page.ViewModel.GetType())
                {
                    return page.ViewModel as T;
                }
            }
            return null;
        }
        /// <summary>
        /// Returns the list of pages
        /// </summary>
        /// <returns>All the system pages</returns>
        public List<AbstractPage> GetAllPages()
        {
            return Pages;
        }
        #endregion
    }
}
