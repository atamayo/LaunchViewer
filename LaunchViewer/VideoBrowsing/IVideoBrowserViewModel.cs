using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.ViewModel
{
    public interface IVideoBrowserViewModel
    {
        ObservableCollection<DeviceInformationDisplay> ResultCollection { get; }
    }
}
