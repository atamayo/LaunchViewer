using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

namespace LaunchViewer.ClipBrowsing
{
    public class ClipEvent
    {
        private const string timestampKey = "timestamp";
        private const string cityKey = "city";
        private const string est_latKey = "est_lat";
        private const string est_lonKey = "est_lon";
        private const string reasonKey = "reason";  

        public ClipEvent(string jsonString)
        {
            JsonObject jsonObject = JsonObject.Parse(jsonString);

            IJsonValue timestampJsonValue = jsonObject.GetNamedValue(timestampKey);
            Timestamp = DateTime.Parse(timestampJsonValue.GetString());

            IJsonValue cityJsonValue = jsonObject.GetNamedValue(cityKey);
            City = cityJsonValue.GetString();

            IJsonValue estLatKeyJsonValue = jsonObject.GetNamedValue(est_latKey);
            var latitude = double.Parse(estLatKeyJsonValue.GetString());
            IJsonValue estLonKeyJsonValue = jsonObject.GetNamedValue(est_lonKey);
            var longitude = double.Parse(estLonKeyJsonValue.GetString());
            Geoposition = new BasicGeoposition { Latitude = latitude , Longitude= longitude } ;


            IJsonValue reasonKeyJsonValue = jsonObject.GetNamedValue(reasonKey);
            var reason = reasonKeyJsonValue.GetString();
            Reason = reason;
        }

        public DateTime Timestamp { get; private set; }

        public string City { get; private set; }

        public BasicGeoposition Geoposition { get; private set; }

        public string Reason  { get; private set; }

    }
}


