using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.Services.USB
{
    class DesignPortableStorageService : IPortableStorageService
    {
        public ObservableCollection<DeviceInformationDisplay> ResultCollection => new ObservableCollection<DeviceInformationDisplay>();

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
