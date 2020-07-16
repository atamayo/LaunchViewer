using CommonServiceLocator;
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
    {/// <summary>
     /// Initializes a new instance of the ViewModelLocator class.
     /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => Dispatcher);
            SimpleIoc.Default.Register<IDeviceInformationDisplayFactory, DeviceInformationDisplayFactory>();
            SimpleIoc.Default.Register<IPortableStorageService, PortableStorageService>();
            SimpleIoc.Default.Register<IVideoBrowserViewModel, VideoBrowserViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            
        }
    }
}
