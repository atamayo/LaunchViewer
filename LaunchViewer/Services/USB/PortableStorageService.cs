using System;
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using Windows.UI.Core;

namespace LaunchViewer.Services.USB
{
    public class PortableStorageService : IPortableStorageService
    {
        private DeviceWatcher _deviceWatcher;
        private CoreDispatcher _dispatcher;
        private readonly IDeviceInformationDisplayFactory _deviceInformationDisplayFactory;
        public ObservableCollection<DeviceInformationDisplay> ResultCollection { get; private set; }
        
        public PortableStorageService(CoreDispatcher coreDispatcher, IDeviceInformationDisplayFactory deviceInformationDisplayFactory)
        {
            _dispatcher = coreDispatcher;
            _deviceInformationDisplayFactory = deviceInformationDisplayFactory;
        }
        public void Start()
        {
            ResultCollection = new ObservableCollection<DeviceInformationDisplay>();

             _deviceWatcher = DeviceInformation.CreateWatcher(DeviceClass.PortableStorageDevice);
            _deviceWatcher.Added += Watcher_DeviceAdded;
            _deviceWatcher.Removed += Watcher_DeviceRemoved;
            _deviceWatcher.Start();
        }

        public void Stop()
        {
            if (IsWatcherStarted(_deviceWatcher))
            {
                _deviceWatcher.Stop();
            }
        }

        private async void Watcher_DeviceAdded(DeviceWatcher sender, DeviceInformation deviceInfo)
        {
           
            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, async () =>    
            {
                if (IsWatcherStarted(sender))   
                {
                    var deviceInfoDisplay = await _deviceInformationDisplayFactory.Create(deviceInfo);
                    ResultCollection.Add(deviceInfoDisplay);
                }
            });
        }

        private async void Watcher_DeviceRemoved(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                if (IsWatcherStarted(sender))
                {
                    foreach (DeviceInformationDisplay deviceInfoDisp in ResultCollection)
                    {
                        if (deviceInfoDisp.Id == deviceInfoUpdate.Id)
                        {
                            ResultCollection.Remove(deviceInfoDisp);
                            break;
                        }
                    }
                }
            });
        }

        static bool IsWatcherStarted(DeviceWatcher watcher)
        {
            return (watcher.Status == DeviceWatcherStatus.Started) ||
                (watcher.Status == DeviceWatcherStatus.EnumerationCompleted);
        }
    }
}