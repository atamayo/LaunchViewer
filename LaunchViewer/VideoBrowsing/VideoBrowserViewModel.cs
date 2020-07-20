using GalaSoft.MvvmLight;
using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;

namespace LaunchViewer.VideoBrowsing
{
    public class VideoBrowserViewModel : ViewModelBase, IVideoBrowserViewModel
    {
        private DeviceInformationDisplay _selectedDevice;

        public IPortableStorageService PortableStorageService { get; }

        public ObservableCollection<DeviceInformationDisplay> ResultCollection => PortableStorageService.ResultCollection;

        public ObservableCollection<Clip> SentryEventItemsViewSource { get; }
        private readonly CoreDispatcher _dispatcher;

        public VideoBrowserViewModel(CoreDispatcher coreDispatcher, IPortableStorageService portableStorageService)
        {
            _dispatcher = coreDispatcher;
            PortableStorageService = portableStorageService;
            PortableStorageService.Start();
            SentryEventItemsViewSource = new ObservableCollection<Clip>();

        }

        public DeviceInformationDisplay SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    RaisePropertyChanged(nameof(SelectedDevice));

                    _ = LoadDeviceVideos(_selectedDevice);
                }
            }
        }

 

        private async Task LoadDeviceVideos(DeviceInformationDisplay selectedDevice)
        {
            SentryEventItemsViewSource.Clear();

            if (null != selectedDevice)
            {
                //await _dispatcher.RunAsync(CoreDispatcherPriority.Low, async () =>
                //{

               

                var folders2 = await selectedDevice.StorageFolder.GetFolderAsync("TeslaCam\\SentryClips");

                var files = await folders2.GetItemsAsync();

                foreach (var file in files)
                {
                    
                    

                    SentryEventItemsViewSource.Add(new Clip(file.Name));
                  
                }

                //});
                //var folders = await GetFiles(selectedDevice.StorageFolder);

                //foreach (var folder in folders)
                //{
                //    Debug.WriteLine(folder);
                //}
            }
        }

        private async Task<IList<string>> GetFiles(StorageFolder folder)
        {
            StorageFolder fold = folder;

            var folders = new List<string>();

            var items = await fold.GetItemsAsync();

            foreach (var item in items)
            {
                if (item.GetType() == typeof(StorageFolder))
                {
                    folders.Add(item.Path.ToString());
                    folders.AddRange(await GetFiles(item as StorageFolder));
                }
            }

            //& listView.ItemsSource = files;

            return folders;
        }
    }
}
