using LaunchViewer.ClipBrowsing;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.Model
{
    public class Clip
    {
        private ClipEvent _event;

        public Clip(BitmapImage thumb, ClipEvent clipEvent, ClipType clipType, IReadOnlyCollection<Video> videos)
        {
            Thumb = thumb;
            _event = clipEvent;
            Type = clipType;
            Videos = videos;
        }

        public string Name { get; private set; }
        public DateTime Timestamp => _event.Timestamp;
        public string City => _event.City;
        public BasicGeoposition Geoposition => _event.Geoposition;
        public BitmapImage Thumb { get; private set; }
        public ClipType Type { get; private set; }
        public IReadOnlyCollection<Video> Videos { get; private  set; }
    }
}
