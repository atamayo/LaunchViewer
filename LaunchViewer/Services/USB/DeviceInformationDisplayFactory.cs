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
            DeviceInformationDisplay deviceInfoDisplay = null;

            var usbPath = Windows.Devices.Portable.StorageDevice.FromId(deviceInfo.Id).Path;
            
            if (!string.IsNullOrEmpty(usbPath))
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(usbPath);

                if (null != folder)
                {
                    // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
                    StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                    deviceInfoDisplay = new DeviceInformationDisplay(deviceInfo.Id, folder);
                }
            }

            return deviceInfoDisplay;
        }
        
    }
}
