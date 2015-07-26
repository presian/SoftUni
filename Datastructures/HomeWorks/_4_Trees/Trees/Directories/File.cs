namespace Directories
{
    class File
    {
        private string name;
        private long size;
        private string path;

        public File(string name, long size, string path)
        {
            this.Name = name;
            this.Size = size;
            this.Path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        public string Name { get; set; }

        public long Size { get; set; }
    }
}
