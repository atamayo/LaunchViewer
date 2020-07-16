using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.Services.USB
{
    class DeviceInformationDisplayFactory : IDeviceInformationDisplayFactory
    {
        public async Task<DeviceInformationDisplay> Create(DeviceInformation deviceInfo)
        {
            var usb = Windows.Devices.Portable.StorageDevice.FromId(deviceInfo.Id);

            // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
            var folder = await StorageFolder.GetFolderFromPathAsync(usb.Path);
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

            return new DeviceInformationDisplay(deviceInfo.Id, usb.DisplayName, usb.Path);
        }
        
    }
}
