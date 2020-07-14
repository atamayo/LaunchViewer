using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.ViewModel
{
    public class MainViewModel
    {
        public string MSG { get; set; }
        public MainViewModel()
        {
            MSG = $"Sentry: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
        }
    }
}
