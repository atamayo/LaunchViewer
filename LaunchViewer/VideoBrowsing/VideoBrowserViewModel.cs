using GalaSoft.MvvmLight;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            }
        }
    }
}
