using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.VideoBrowsing
{
    public interface IVideoBrowserViewModel
    {
        ObservableCollection<DeviceInformationDisplay> ResultCollection { get; }
        ObservableCollection<Clip> SentryEventItemsViewSource { get; }
    }
}
