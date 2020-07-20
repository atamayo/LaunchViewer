using Autofac;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using LaunchViewer.ClipBrowsing;
using LaunchViewer.MainView;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace LaunchViewer.ViewModel
{
    public class ViewModelLocator : Page
    {
        private static IContainer Container { get; set; }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>

        public ViewModelLocator()
        {

            var builder = new ContainerBuilder();


            if (ViewModelBase.IsInDesignModeStatic)
            {
                builder.RegisterType<DesignPortableStorageService>().As<IPortableStorageService>();
            }
            else
            {
                builder.RegisterType<PortableStorageService>().As<IPortableStorageService>().SingleInstance();
            }

            builder.Register(d => Dispatcher);
            builder.RegisterType<DeviceInformationDisplayFactory>().As<IDeviceInformationDisplayFactory>();

            builder.RegisterType<ClipBrowserViewModel>().As<IClipBrowserViewModel>();
            builder.RegisterType<MainViewModel>();
            Container = builder.Build();

        }

        public MainViewModel Main
        {
            get
            {
                return Container.Resolve<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            Container.Dispose();
        }
    }
}
