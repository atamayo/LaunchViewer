using Autofac;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
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

           // ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<IPortableStorageService, DesignPortableStorageService>();
                builder.RegisterType<DesignPortableStorageService>().As<IPortableStorageService>();
            }
            else
            {
                builder.RegisterType<PortableStorageService>().As<IPortableStorageService>().SingleInstance();
                // SimpleIoc.Default.Register<IPortableStorageService, PortableStorageService>();
            }

            builder.Register(d => Dispatcher);
            builder.RegisterType<DeviceInformationDisplayFactory>().As<IDeviceInformationDisplayFactory>();
            //SimpleIoc.Default.Register(() => Dispatcher);
            //SimpleIoc.Default.Register<IDeviceInformationDisplayFactory, DeviceInformationDisplayFactory>();

            builder.RegisterType<VideoBrowserViewModel>().As<IVideoBrowserViewModel>();
            builder.RegisterType<MainViewModel>();
            Container = builder.Build();

            //SimpleIoc.Default.Register<IVideoBrowserViewModel, VideoBrowserViewModel>();
            //SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return Container.Resolve<MainViewModel>();
            }
            //get
            //{
            //    return ServiceLocator.Current.GetInstance<MainViewModel>();
            //}
        }

        public static void Cleanup()
        {
            Container.Dispose();
        }
    }
}
