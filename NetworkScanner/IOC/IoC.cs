using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScanner
{
    /// <summary>
    /// IOC container for application
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// The kernel for IOC container
        /// </summary>
        public static IKernel Kernel { get; set; } = new StandardKernel();

        /// <summary>
        /// Shorthand for getting service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Sets up IOC container and binds all services, must be called as soon as app opens
        /// </summary>
        public static bool Setup()
        {
            try
            {
                // Bind all view models
                BindViewModels();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Binds all singleton viewmodels
        /// </summary>
        private static void BindViewModels()
        {
            // Must be in called order
            Kernel.Bind<ISettingsService>().ToConstant(new SettingsService());
            Kernel.Bind<ILoggingService>().ToConstant(new LoggingService());
            Kernel.Bind<INavigationService>().ToConstant(new NavigationService());
        }
    }
}
