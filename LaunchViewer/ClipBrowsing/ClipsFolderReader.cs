using LaunchViewer.Model;
using LaunchViewer.Services.USB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace LaunchViewer.ClipBrowsing
{
    class ClipsFolderReader : IClipsFolderReader
    {
        public ObservableCollection<Clip> SentryEventItemsViewSource { get; private set; }

        public ClipsFolderReader()
        {
            SentryEventItemsViewSource = new ObservableCollection<Clip>();
        }

        public async Task GetClipsAsync(ClipType clipType, DeviceInformationDisplay deviceInfoDisplay)
        {
            SentryEventItemsViewSource.Clear();

            if (null == deviceInfoDisplay) return;

            var sentryClipsFolder = await deviceInfoDisplay.StorageFolder.GetFolderAsync("TeslaCam\\SentryClips");

            List<string> fileTypeFilter = new List<string>();
            fileTypeFilter.Add(".jpg");
            fileTypeFilter.Add(".png");
            fileTypeFilter.Add(".bmp");
            fileTypeFilter.Add(".gif");

            var query = sentryClipsFolder.CreateFolderQueryWithOptions(new Windows.Storage.Search.QueryOptions(Windows.Storage.Search.CommonFileQuery.OrderByName, fileTypeFilter));

            var folders = await query.GetFoldersAsync();
          

            foreach (var folder in folders)
            {
                var thumbSource = folder.Path + @"\thumb.png";
                var jsonPath = folder.Path + @"\event.json";

                StorageFile sfi = await StorageFile.GetFileFromPathAsync(thumbSource);

                StorageFile json = await StorageFile.GetFileFromPathAsync(jsonPath);

                var thumbImage = await FromStorageFile(sfi);
                var jonText = await StringFromStorageFile(json);

                var clipEvent = new ClipEvent(jonText);

                var files = await folder.GetFilesAsync();

                var videoPaths = new List<string>();

                foreach (var file in files)
                {
                    if (file.FileType != ".mp4") continue;

                    videoPaths.Add(file.Path);
                }

                var videos = videoPaths.Select(path => new Video(path)).ToList().AsReadOnly();


                SentryEventItemsViewSource.Add(new Clip(thumbImage, clipEvent, ClipType.Sentry, videos));

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
    }
}
