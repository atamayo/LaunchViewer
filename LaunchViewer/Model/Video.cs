namespace LaunchViewer.Model
{
    public class Video
    {
        public Video(string path)
        {
            Path = path;
        }
        public string Path { get; private set; }
    }
}
