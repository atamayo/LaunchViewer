using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LaunchViewer.ClipBrowsing
{
    public interface IClipsFolderReader
    {
        Task GetClipsAsync(ClipType clipType, DeviceInformationDisplay deviceInfoDisplay);

        ObservableCollection<Clip> SentryEventItemsViewSource { get; }
    }
}
