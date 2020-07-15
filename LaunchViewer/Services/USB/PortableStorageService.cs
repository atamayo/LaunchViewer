using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Enumeration;
using Windows.Devices.Usb;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;

namespace LaunchViewer.Services.USB
{
    public class PortableStorageService : IPortableStorageService
    {
        private DeviceWatcher _deviceWatcher;
        private CoreDispatcher _dispatcher;
        public ObservableCollection<DeviceInformationDisplay> ResultCollection { get; private set; }
        
        public delegate void DeviceChangedHandler(DeviceWatcher deviceWatcher, string id);
        public event DeviceChangedHandler DeviceChanged;
        public PortableStorageService(CoreDispatcher coreDispatcher)
        {
            _dispatcher = coreDispatcher;
        }
        public void Start()
        {
            ResultCollection = new ObservableCollection<DeviceInformationDisplay>();

            // _deviceWatcher = DeviceInformation.CreateWatcher(DeviceClass.PortableStorageDevice);
            _deviceWatcher = DeviceInformation.CreateWatcher(DeviceInformation.GetAqsFilterFromDeviceClass(DeviceClass.PortableStorageDevice),new[] { "System.Devices.DeviceInstanceId" });
            _deviceWatcher.Added += Watcher_DeviceAdded;
            _deviceWatcher.Updated += Watcher_DeviceUpdated;
            _deviceWatcher.Removed += Watcher_DeviceRemoved;
            _deviceWatcher.EnumerationCompleted += Watcher_EnumerationCompleted;
            _deviceWatcher.Stopped += Watcher_Stopped;

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
           

            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                if (IsWatcherStarted(sender))
                {
                    ResultCollection.Add(new DeviceInformationDisplay(deviceInfo));
                    RaiseDeviceChanged(sender, deviceInfo.Id);
                }
            });
        }

        private async void Watcher_DeviceUpdated(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                if (IsWatcherStarted(sender))
                {
                    foreach (DeviceInformationDisplay deviceInfoDisp in ResultCollection)
                    {
                        if (deviceInfoDisp.Id == deviceInfoUpdate.Id)
                        {
                            deviceInfoDisp.Update(deviceInfoUpdate);
                            RaiseDeviceChanged(sender, deviceInfoUpdate.Id);
                            break;
                        }
                    }
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

                    RaiseDeviceChanged(sender, deviceInfoUpdate.Id);
                }
            });
        }

        private async void Watcher_EnumerationCompleted(DeviceWatcher sender, object obj)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                RaiseDeviceChanged(sender, string.Empty);
            });
        }

        private async void Watcher_Stopped(DeviceWatcher sender, object obj)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                RaiseDeviceChanged(sender, string.Empty);
            });
        }

        private void RaiseDeviceChanged(DeviceWatcher sender, string id)
        {
            DeviceChanged?.Invoke(sender, id);
        }

        static bool IsWatcherStarted(DeviceWatcher watcher)
        {
            return (watcher.Status == DeviceWatcherStatus.Started) ||
                (watcher.Status == DeviceWatcherStatus.EnumerationCompleted);
        }
        
    }
}
