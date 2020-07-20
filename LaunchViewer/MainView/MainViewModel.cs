using LaunchViewer.ClipBrowsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchViewer.MainView
{
    public class MainViewModel
    {
        public string MSG { get; set; }
        public IClipBrowserViewModel ClipBrowserViewModel { get; }

        public MainViewModel(IClipBrowserViewModel clipBrowserViewModel)
        {
            MSG = $"Sentry: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
            ClipBrowserViewModel = clipBrowserViewModel;
        }
    }
}
