using GalaSoft.MvvmLight;
using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.ClipBrowsing
{
    public class ClipBrowserViewModel : ViewModelBase, IClipBrowserViewModel
    {
        private DeviceInformationDisplay _selectedDevice;

        public IPortableStorageService PortableStorageService { get; }

        public ObservableCollection<DeviceInformationDisplay> ResultCollection => PortableStorageService.ResultCollection;

        public ObservableCollection<Clip> SentryEventItemsViewSource { get; }
        

        public ClipBrowserViewModel(IPortableStorageService portableStorageService)
        {
            PortableStorageService = portableStorageService;
            PortableStorageService.Start();
            SentryEventItemsViewSource = new ObservableCollection<Clip>();

        }

        public DeviceInformationDisplay SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                if (_selectedDevice != value)
                {
                    _selectedDevice = value;
                    RaisePropertyChanged(nameof(SelectedDevice));

                    _ = LoadDeviceVideos(_selectedDevice);
                }
            }
        }

 

        private async Task LoadDeviceVideos(DeviceInformationDisplay selectedDevice)
        {
            SentryEventItemsViewSource.Clear();

            if (null != selectedDevice)
            {

                var folders2 = await selectedDevice.StorageFolder.GetFolderAsync("TeslaCam\\SentryClips");

                var folders = await folders2.GetFoldersAsync();

                foreach (var folder in folders)
                {
                    var thumbSource = folder.Path + @"\thumb.png";
                    var jsonPath = folder.Path + @"\event.json";

                    StorageFile sfi =  await StorageFile.GetFileFromPathAsync(thumbSource);

                    StorageFile json = await StorageFile.GetFileFromPathAsync(jsonPath);

                    // Uri uri = new Uri(thumbSource);
                    //  BitmapImage thumbImage = new BitmapImage(uri);
                    // var thumbImage = new BitmapImage(new Uri("ms-appx:///Assets/windows-sdk.png"));

                    var thumbImage = await  FromStorageFile(sfi);
                    var jonText = await StringFromStorageFile(json);

                    var clipEvent = new ClipEvent(jonText);

                    SentryEventItemsViewSource.Add(new Clip(thumbImage, clipEvent.Time, clipEvent.City));
                    // var files = await folder.GetFilesAsync();

                    
                }
                
            }
        }

        public static async Task<string> StringFromStorageFile(StorageFile sf)
        {
            return await Windows.Storage.FileIO.ReadTextAsync(sf);
        }

        public static async Task<BitmapImage> FromStorageFile(StorageFile sf)
        {
            using (var randomAccessStream = await sf.OpenAsync(FileAccessMode.Read))
            {
                var result = new BitmapImage();
                await result.SetSourceAsync(randomAccessStream);
                return result;
            }
        }

        private async Task<IList<string>> GetFiles(StorageFolder folder)
        {
            StorageFolder fold = folder;

            var folders = new List<string>();

            var items = await fold.GetItemsAsync();

            foreach (var item in items)
            {
                if (item.GetType() == typeof(StorageFolder))
                {
                    folders.Add(item.Path.ToString());
                    folders.AddRange(await GetFiles(item as StorageFolder));
                }
            }

            //& listView.ItemsSource = files;

            return folders;
        }
    }
}
