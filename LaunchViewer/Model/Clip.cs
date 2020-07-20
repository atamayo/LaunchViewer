using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.Model
{
    public class Clip
    {
        public Clip(BitmapImage thumb, DateTime timesstamp, string city)
        {
            Thumb = thumb;
            Timestamp = timesstamp;
            City = city;
        }
        public Clip(string name, DateTime timesstamp, string city, BasicGeoposition geoposition, BitmapImage thumb, ClipType clipType, IReadOnlyCollection<Video> videos)
        {
            Name = name;
            Timestamp = timesstamp;
            City = city;
            Geoposition = geoposition;
            Thumb = thumb;
            Type = clipType;
            Videos = videos;
        }

        public string Name { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string City { get; private set; }
        public BasicGeoposition Geoposition { get; private set; }
        public BitmapImage Thumb { get; private set; }
        public ClipType Type { get; private set; }
        public IReadOnlyCollection<Video> Videos { get; private  set; }
    }
}
