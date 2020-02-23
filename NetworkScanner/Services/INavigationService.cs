using System.Collections.Generic;

namespace NetworkScanner
{
    public interface INavigationService
    {
        List<AbstractPage> GetAllPages();
        object GetCurrentPage();
        T GetNewPage<T>() where T : AbstractPage;
        T GetSingletonPage<T>() where T : AbstractPage;
        T GetViewModel<T>() where T : BaseViewModel;
        void Navigate(object nextPage);
    }
}