using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

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
            Time = DateTime.Parse(timestampJsonValue.GetString());

            IJsonValue cityJsonValue = jsonObject.GetNamedValue(cityKey);
            City = cityJsonValue.GetString();
        }

        public DateTime Time { get; private set; }

        public string City { get; private set; }

    }
}


