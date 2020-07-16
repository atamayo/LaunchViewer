using GalaSoft.MvvmLight;
using Windows.Devices.Enumeration;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.Services.USB
{
    public class DeviceInformationDisplay
    {
        public DeviceInformationDisplay(string id, string label, string logicPath)
        {
            Id = id;
            Label = label;
            LogicPath = logicPath;
        }
        
        public string Id { get; private set; }
        public string Label { get; private set; }
        public string LogicPath { get; private set; }
    }
}
