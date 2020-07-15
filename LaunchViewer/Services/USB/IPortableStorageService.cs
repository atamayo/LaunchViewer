using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.Services.USB
{
    public interface IPortableStorageService  
    {
        void Start();
        void Stop();
        ObservableCollection<DeviceInformationDisplay> ResultCollection { get; }
    }
}
