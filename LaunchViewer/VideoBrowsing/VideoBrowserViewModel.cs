using GalaSoft.MvvmLight;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LaunchViewer.ViewModel
{
    public class VideoBrowserViewModel : ViewModelBase, IVideoBrowserViewModel
    {
        private DeviceInformationDisplay _selectedDevice;

        public IPortableStorageService PortableStorageService { get; }

        public ObservableCollection<DeviceInformationDisplay> ResultCollection => PortableStorageService.ResultCollection;

        public VideoBrowserViewModel(IPortableStorageService portableStorageService)
        {
            PortableStorageService = portableStorageService;

            PortableStorageService.Start();
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

            var folders = await GetFiles(selectedDevice.StorageFolder);

            

            foreach (var folder in folders)
            {
                Debug.WriteLine(folder);
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
