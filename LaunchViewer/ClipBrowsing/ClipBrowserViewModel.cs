using GalaSoft.MvvmLight;
using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace LaunchViewer.ClipBrowsing
{
    public class ClipBrowserViewModel : ViewModelBase, IClipBrowserViewModel
    {
        private DeviceInformationDisplay _selectedDevice;
        private readonly IClipsFolderReader _clipsFolderReader;
        private readonly IPortableStorageService _portableStorageService;

        public ObservableCollection<DeviceInformationDisplay> ResultCollection => _portableStorageService.ResultCollection;
        public ObservableCollection<Clip> SentryEventItemsViewSource => _clipsFolderReader.SentryEventItemsViewSource;


        public ClipBrowserViewModel(IPortableStorageService portableStorageService, IClipsFolderReader clipsFolderReader)
        {
            _portableStorageService = portableStorageService;
            _clipsFolderReader = clipsFolderReader;
            _portableStorageService.Start();
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

                     LoadDeviceVideos(_selectedDevice);
                }
            }
        }

        private async void LoadDeviceVideos(DeviceInformationDisplay selectedDevice)
        {
            await _clipsFolderReader.GetClipsAsync(ClipType.Sentry, selectedDevice);
        }
  
    }
}
