using LaunchViewer.ViewModel;
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
        public IVideoBrowserViewModel VideoBrowserViewModel { get; }

        public MainViewModel(IVideoBrowserViewModel videoBrowserViewModel)
        {
            MSG = $"Sentry: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
            VideoBrowserViewModel = videoBrowserViewModel;
        }
    }
}
