using GalaSoft.MvvmLight;
using Windows.Devices.Enumeration;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.Services.USB
{
    public class DeviceInformationDisplay
    {
        public DeviceInformationDisplay(string id, StorageFolder storageFolder)
        {
            Id = id;
            StorageFolder = storageFolder;
        }

        public string Id { get; private set; }
        public StorageFolder StorageFolder { get; private set; }
        public string Label => StorageFolder.DisplayName;
        public string LogicPath => StorageFolder.Path;
    }
}
