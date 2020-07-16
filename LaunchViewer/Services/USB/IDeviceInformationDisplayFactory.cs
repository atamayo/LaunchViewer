using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace LaunchViewer.Services.USB
{
    public interface IDeviceInformationDisplayFactory
    {
        Task<DeviceInformationDisplay> Create(DeviceInformation deviceInformation);
    }
}
